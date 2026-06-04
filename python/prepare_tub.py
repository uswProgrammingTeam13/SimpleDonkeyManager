#!/usr/bin/env python3
"""
prepare_tub.py - donkeycar v3 형식 데이터를 v2(tub) 형식으로 변환합니다.

donkey 폴더처럼 record_N.json + meta.json + 루트 이미지로 구성된 구형(v3)
데이터를, 설치된 donkeycar 5.x 가 요구하는 tub v2 형식
(manifest.json + catalog_N.catalog + catalog_N.catalog_manifest + images/)
으로 변환합니다.

변환은 손으로 바이트 포맷을 만드는 대신 donkeycar 의 Tub API 를 그대로
사용하므로, manifest/catalog 직렬화 규칙(line_lengths 등)이 라이브러리와
항상 일치합니다.

또한 학습(train.py)에 필요한 config.py 가 없으면 donkeycar 기본 템플릿
(cfg_complete.py)을 복사하여 생성합니다.

이미 변환된 폴더(manifest.json 존재)는 건너뜁니다.

Usage:
    prepare_tub.py --tub <path> [--config-dir <dir>]

train.py 는 수정하지 않으며, 이 스크립트는 학습 전 준비 단계로만 사용됩니다.
"""

import os
import sys
import json
import glob
import shutil
import argparse


def log(msg):
    """stdout 으로 즉시 출력 (호출 측 학습 로그에 표시되도록 flush)."""
    print(msg, flush=True)


def is_already_v2(tub_path):
    """이미 tub v2 형식으로 변환되었는지 확인."""
    return os.path.exists(os.path.join(tub_path, 'manifest.json'))


def sync_deleted_indexes(tub_path):
    """images 폴더에 실제 파일이 없는 catalog 레코드를 학습에서 제외하도록
    manifest.json 의 deleted_indexes 를 재계산합니다.

    데이터 필터링으로 이미지가 filtered 폴더로 이동(백업)되었지만 manifest 에
    반영되지 않은 경우, 학습 시 donkeycar 가 없는 이미지를 읽으려다
    FileNotFoundError 로 중단됩니다. 학습 전에 정합성을 맞춰 이를 방지합니다.

    [중요] donkeycar 의 ManifestIterator 는 deleted_indexes 를 catalog 의
    '_index' 값이 아니라 catalog 를 순서대로 읽은 '순번(position, 0부터)' 으로
    비교합니다(datastore_v2.py ManifestIterator.__next__). 따라서 여기서도
    반드시 순번 기준으로 계산해야 학습이 올바르게 해당 레코드를 건너뜁니다.
    """
    try:
        manifest_path = os.path.join(tub_path, 'manifest.json')
        if not os.path.exists(manifest_path):
            return

        images_dir = os.path.join(tub_path, 'images')
        if not os.path.isdir(images_dir):
            return

        # manifest.json 읽기 (5번째 줄 = catalog metadata)
        with open(manifest_path, 'r', encoding='utf-8') as fp:
            lines = [ln.rstrip('\n') for ln in fp.readlines()]
        if len(lines) < 5:
            return

        try:
            meta = json.loads(lines[4])
        except Exception:
            return
        if not isinstance(meta, dict):
            return

        catalog_paths = meta.get('paths') or []
        prev_deleted = set(meta.get('deleted_indexes') or [])

        # catalog 를 ManifestIterator 와 동일한 순서로 읽으며 순번(position)을 매기고,
        # 실제 이미지가 없는 레코드의 순번을 deleted 집합으로 수집합니다.
        position = 0
        deleted_positions = set()
        missing_count = 0
        for cat_name in catalog_paths:
            cat_path = os.path.join(tub_path, cat_name)
            if not os.path.exists(cat_path):
                continue
            with open(cat_path, 'r', encoding='utf-8') as cf:
                for line in cf:
                    if not line.strip():
                        # ManifestIterator 는 빈 줄을 카탈로그 끝으로 보고 다음
                        # 카탈로그로 넘어가므로 순번을 증가시키지 않습니다.
                        continue
                    try:
                        rec = json.loads(line)
                    except Exception:
                        position += 1
                        continue

                    img = rec.get('cam/image_array')
                    if img:
                        img_name = os.path.basename(img)
                        if not os.path.exists(os.path.join(images_dir, img_name)):
                            deleted_positions.add(position)
                            missing_count += 1
                    position += 1

        # 실제 파일 존재 여부로 전체 재계산한 결과를 기록합니다.
        merged = sorted(deleted_positions)
        if merged == sorted(prev_deleted):
            log("[준비] 데이터 정합성 확인 완료 (변경 없음).")
            return

        meta['deleted_indexes'] = merged
        lines[4] = json.dumps(meta)

        with open(manifest_path, 'w', encoding='utf-8') as fp:
            fp.write('\n'.join(lines))
            fp.write('\n')

        log(f"[준비] 누락 이미지 {missing_count}건을 학습에서 제외하도록 "
            f"deleted_indexes 를 재계산했습니다 (총 {len(merged)}건).")
    except Exception as ex:
        log(f"[경고] deleted_indexes 동기화 실패(무시하고 진행): {ex}")


def read_meta(tub_path):
    """meta.json 에서 inputs/types 를 읽습니다."""
    meta_path = os.path.join(tub_path, 'meta.json')
    if not os.path.exists(meta_path):
        return None, None
    with open(meta_path, 'r', encoding='utf-8') as fp:
        meta = json.load(fp)
    return meta.get('inputs'), meta.get('types')


def find_v3_records(tub_path):
    """record_N.json 파일들을 인덱스 순으로 정렬하여 반환합니다."""
    records = []
    for f in glob.glob(os.path.join(tub_path, 'record_*.json')):
        base = os.path.basename(f)
        num = base[len('record_'):-len('.json')]
        try:
            idx = int(num)
        except ValueError:
            continue
        records.append((idx, f))
    records.sort(key=lambda x: x[0])
    return records


def convert_v3_to_v2(tub_path):
    """v3 형식 데이터를 같은 폴더 내부에 v2 tub 형식으로 변환합니다."""
    import numpy as np
    from PIL import Image
    from donkeycar.parts.tub_v2 import Tub

    inputs, types = read_meta(tub_path)
    if not inputs or not types:
        log("[오류] meta.json 에서 inputs/types 를 읽을 수 없습니다. v3 형식이 아닌 것 같습니다.")
        return False

    records = find_v3_records(tub_path)
    if not records:
        log("[오류] record_*.json 파일을 찾을 수 없습니다. 변환할 레코드가 없습니다.")
        return False

    # 이미지 입력 키 찾기 (type 이 image_array 인 입력)
    image_key = None
    for inp, typ in zip(inputs, types):
        if typ == 'image_array':
            image_key = inp
            break

    if image_key is None:
        log("[오류] image_array 타입 입력을 찾을 수 없습니다. 이미지 데이터가 없습니다.")
        return False

    log(f"[준비] tub v2 변환 시작: {tub_path}")
    log(f"[준비] inputs={inputs}")
    log(f"[준비] types={types}")
    log(f"[준비] 레코드 {len(records)}개, 이미지 키='{image_key}'")

    tub = Tub(base_path=tub_path, inputs=inputs, types=types)

    written = 0
    skipped = 0
    total = len(records)
    for i, (idx, rec_file) in enumerate(records):
        try:
            with open(rec_file, 'r', encoding='utf-8') as fp:
                rec = json.load(fp)
        except Exception as ex:
            log(f"[경고] 레코드 읽기 실패 {os.path.basename(rec_file)}: {ex}")
            skipped += 1
            continue

        img_name = rec.get(image_key)
        if not img_name:
            skipped += 1
            continue

        img_path = os.path.join(tub_path, img_name)
        if not os.path.exists(img_path):
            log(f"[경고] 이미지 없음, 건너뜀: {img_name}")
            skipped += 1
            continue

        try:
            with Image.open(img_path) as im:
                arr = np.asarray(im.convert('RGB'))
        except Exception as ex:
            log(f"[경고] 이미지 로드 실패 {img_name}: {ex}")
            skipped += 1
            continue

        record = {}
        for inp, typ in zip(inputs, types):
            if typ == 'image_array':
                record[inp] = arr
            else:
                record[inp] = rec.get(inp)

        try:
            tub.write_record(record)
            written += 1
        except Exception as ex:
            log(f"[경고] 레코드 기록 실패 (index {idx}): {ex}")
            skipped += 1
            continue

        # 진행 상황 (200개마다)
        if (i + 1) % 200 == 0 or (i + 1) == total:
            log(f"[준비] 변환 진행: {i + 1}/{total} (기록 {written}, 건너뜀 {skipped})")

    tub.close()

    if written == 0:
        log("[오류] 변환된 레코드가 없습니다. 학습을 진행할 수 없습니다.")
        return False

    log(f"[준비] tub v2 변환 완료: 기록 {written}개, 건너뜀 {skipped}개")
    return True


def ensure_config(config_dir):
    """config.py 가 없으면 donkeycar 기본 템플릿으로 생성합니다."""
    cfg_path = os.path.join(config_dir, 'config.py')
    if os.path.exists(cfg_path):
        log(f"[준비] config.py 가 이미 존재합니다: {cfg_path}")
        return True

    try:
        import donkeycar
        template = os.path.join(os.path.dirname(donkeycar.__file__),
                                'templates', 'cfg_complete.py')
        if not os.path.exists(template):
            log(f"[오류] donkeycar 설정 템플릿을 찾을 수 없습니다: {template}")
            return False

        shutil.copyfile(template, cfg_path)
        # 헤드리스 서브프로세스 환경에 맞춰 일부 옵션을 덮어씁니다.
        with open(cfg_path, 'a', encoding='utf-8') as f:
            f.write('\n')
            f.write('# ----- prepare_tub.py 자동 추가 설정 -----\n')
            f.write('# 학습 종료 시 matplotlib 창이 떠서 프로세스가 멈추지 않도록 비활성화\n')
            f.write('SHOW_PLOT = False\n')
            f.write('# 모델 요약은 로그로 출력\n')
            f.write('PRINT_MODEL_SUMMARY = True\n')
            f.write('# 변환 실패 위험이 있는 추가 포맷 생성 비활성화 (안정적 실행)\n')
            f.write('CREATE_TF_LITE = False\n')
            f.write('CREATE_TENSOR_RT = False\n')
        log(f"[준비] config.py 생성 완료: {cfg_path}")
        return True
    except Exception as ex:
        log(f"[오류] config.py 생성 실패: {ex}")
        return False


def main():
    parser = argparse.ArgumentParser(
        description='donkeycar v3 데이터를 v2 tub 형식으로 변환하고 config.py 를 준비합니다.')
    # train.py 와 동일하게 --tubs 를 사용 (TrainingControl 호출과 일치).
    # 과거 호환을 위해 --tub 도 별칭으로 허용합니다.
    parser.add_argument('--tubs', '--tub', dest='tubs', required=True,
                        help='변환할 데이터 폴더 경로')
    parser.add_argument('--config-dir', default=None,
                        help='config.py 를 생성할 폴더 (기본: 이 스크립트가 있는 폴더)')
    args = parser.parse_args()

    tub_path = os.path.abspath(args.tubs)
    config_dir = args.config_dir or os.path.dirname(os.path.abspath(__file__))

    if not os.path.isdir(tub_path):
        log(f"[오류] 데이터 폴더가 존재하지 않습니다: {tub_path}")
        sys.exit(1)

    # 1) config.py 준비
    if not ensure_config(config_dir):
        sys.exit(2)

    # 2) tub 형식 준비
    if is_already_v2(tub_path):
        log(f"[준비] 이미 tub v2 형식입니다 (manifest.json 존재). 변환을 건너뜁니다: {tub_path}")
        # 필터링 등으로 누락된 이미지가 있으면 학습 전에 deleted_indexes 로 정합성 보정
        sync_deleted_indexes(tub_path)
        sys.exit(0)

    ok = convert_v3_to_v2(tub_path)
    if not ok:
        sys.exit(3)

    # 변환 후에도 누락 이미지 정합성 보정
    sync_deleted_indexes(tub_path)

    log("[준비] 모든 준비가 완료되었습니다. 학습을 시작할 수 있습니다.")
    sys.exit(0)


if __name__ == '__main__':
    main()
