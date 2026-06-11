#!/usr/bin/env python3
"""
학습된 모델을 사용해 프레임별 추론(AI 예측)을 수행하고
실제값과 비교한 결과를 JSON 으로 출력하는 검증 스크립트입니다.

입력 JSON (--input) 형식:
{
    "frames": [
        { "frame": 152, "image": "C:/.../images/152_cam_image_array_.jpg",
          "actual_angle": -0.35, "actual_throttle": 0.42 },
        ...
    ]
}

출력 JSON (--output) 형식:
{
    "model_type": "linear",
    "count": 500,
    "results": [
        { "frame": 152, "image": "...",
          "actual_angle": -0.35, "pred_angle": -0.31,
          "actual_throttle": 0.42, "pred_throttle": 0.39,
          "angle_error": 0.04, "throttle_error": 0.03 },
        ...
    ],
    "summary": {
        "count": 500,
        "avg_angle_error": 0.047,
        "max_angle_error": 0.312,
        "avg_throttle_error": 0.025,
        "max_throttle_error": 0.110,
        "verdict": "양호"
    }
}

Usage:
    validate_model.py --model=<model> --input=<input.json> --output=<output.json>
    [--type=(linear|inferred|tensorrt_linear|tflite_linear)]
"""

import os
import sys
import json
import argparse


def log(msg):
    # C# 쪽에서 stdout 을 그대로 로그에 표시하므로 진행 상황을 출력합니다.
    print(msg, flush=True)


def _is_ascii(text):
    try:
        text.encode("ascii")
        return True
    except (UnicodeEncodeError, AttributeError):
        return False


def _ensure_ascii_path(path, suffix):
    """
    TensorFlow/Keras 는 Windows 에서 비ASCII(예: 한글) 경로의 모델 파일을
    로드하지 못합니다. 경로에 비ASCII 문자가 있으면 ASCII 임시 경로로 복사한
    뒤 그 경로를 반환합니다. (호출 측에서 사용 후 삭제)
    """
    if _is_ascii(os.path.abspath(path)):
        return path, None

    import tempfile
    import shutil

    fd, tmp_path = tempfile.mkstemp(suffix=suffix)
    os.close(fd)
    shutil.copy2(path, tmp_path)
    log(f"[검증] 비ASCII 경로 감지 -> 임시 경로로 복사: {tmp_path}")
    return tmp_path, tmp_path


def main():
    parser = argparse.ArgumentParser(description="Validate a trained donkeycar model.")
    parser.add_argument("--model", required=True, help="학습된 모델 파일 경로 (.h5)")
    parser.add_argument("--input", required=True, help="검증할 프레임 목록 JSON 경로")
    parser.add_argument("--output", required=True, help="검증 결과를 저장할 JSON 경로")
    parser.add_argument("--type", default=None, help="모델 타입 (기본값: config DEFAULT_MODEL_TYPE)")
    args = parser.parse_args()

    if not os.path.exists(args.model):
        log(f"[검증오류] 모델 파일을 찾을 수 없습니다: {args.model}")
        sys.exit(2)

    if not os.path.exists(args.input):
        log(f"[검증오류] 입력 파일을 찾을 수 없습니다: {args.input}")
        sys.exit(2)

    log("[검증] donkeycar 모듈을 불러오는 중...")
    import numpy as np
    from PIL import Image
    import donkeycar as dk
    from donkeycar.utils import get_model_by_type

    cfg = dk.load_config()
    model_type = args.type if args.type else getattr(cfg, "DEFAULT_MODEL_TYPE", "linear")

    # TensorFlow 는 Windows 에서 한글 등 비ASCII 모델 경로를 로드하지 못하므로
    # 필요한 경우 ASCII 임시 경로로 복사한 뒤 로드합니다.
    model_ext = os.path.splitext(args.model)[1] or ".h5"
    model_load_path, model_tmp_to_cleanup = _ensure_ascii_path(args.model, model_ext)

    log(f"[검증] 모델 로드: {args.model} (타입={model_type})")
    pilot = get_model_by_type(model_type, cfg)
    try:
        pilot.load(model_load_path)
    finally:
        if model_tmp_to_cleanup:
            try:
                os.remove(model_tmp_to_cleanup)
            except OSError:
                pass

    image_w = getattr(cfg, "IMAGE_W", 160)
    image_h = getattr(cfg, "IMAGE_H", 120)
    image_d = getattr(cfg, "IMAGE_DEPTH", 3)

    with open(args.input, "r", encoding="utf-8-sig") as f:
        payload = json.load(f)

    frames = payload.get("frames", [])
    total = len(frames)
    log(f"[검증] 검증 프레임 수: {total}")

    results = []
    angle_errors = []
    throttle_errors = []

    # 진행 마커는 매 프레임이 아닌 일정 간격(전체의 약 1%)으로만 출력하여
    # stdout I/O 로 인한 검증 속도 저하를 최소화합니다.
    progress_interval = max(1, total // 100)

    for i, frame in enumerate(frames):
        image_path = frame.get("image")
        if not image_path or not os.path.exists(image_path):
            continue

        # C# UI 가 진행률 상태바를 갱신할 수 있도록 일정 간격으로 진행 마커를 출력합니다.
        # (이미지 미리보기를 표시하지 않으므로 이미지 경로는 전송하지 않습니다.)
        if (i + 1) % progress_interval == 0 or (i + 1) == total:
            log(f"[PROGRESS]\t{i + 1}\t{total}")

        try:
            with open(image_path, "rb") as imgf:
                img = Image.open(imgf)
                if image_d == 1:
                    img = img.convert("L")
                else:
                    img = img.convert("RGB")
            if img.size != (image_w, image_h):
                img = img.resize((image_w, image_h))
            img_arr = np.array(img)
            if image_d == 1:
                img_arr = img_arr.reshape((image_h, image_w, 1))

            out = pilot.run(img_arr)
            pred_angle = float(out[0]) if len(out) > 0 else 0.0
            pred_throttle = float(out[1]) if len(out) > 1 else 0.0
        except Exception as ex:
            log(f"[검증경고] 프레임 {frame.get('frame')} 추론 실패: {ex}")
            continue

        actual_angle = float(frame.get("actual_angle", 0.0))
        actual_throttle = float(frame.get("actual_throttle", 0.0))
        angle_err = abs(actual_angle - pred_angle)
        throttle_err = abs(actual_throttle - pred_throttle)

        angle_errors.append(angle_err)
        throttle_errors.append(throttle_err)

        results.append({
            "frame": frame.get("frame", i),
            "image": image_path,
            "actual_angle": actual_angle,
            "pred_angle": pred_angle,
            "actual_throttle": actual_throttle,
            "pred_throttle": pred_throttle,
            "angle_error": angle_err,
            "throttle_error": throttle_err,
        })

    count = len(results)
    if count > 0:
        avg_angle = sum(angle_errors) / count
        max_angle = max(angle_errors)
        avg_throttle = sum(throttle_errors) / count
        max_throttle = max(throttle_errors)
    else:
        avg_angle = max_angle = avg_throttle = max_throttle = 0.0

    # 판정 기준: 평균 조향 오차로 간단히 평가
    if avg_angle <= 0.05:
        verdict = "양호"
    elif avg_angle <= 0.12:
        verdict = "보통"
    else:
        verdict = "미흡"

    summary = {
        "count": count,
        "avg_angle_error": avg_angle,
        "max_angle_error": max_angle,
        "avg_throttle_error": avg_throttle,
        "max_throttle_error": max_throttle,
        "verdict": verdict,
    }

    output_obj = {
        "model_type": model_type,
        "count": count,
        "results": results,
        "summary": summary,
    }

    with open(args.output, "w", encoding="utf-8") as f:
        json.dump(output_obj, f, ensure_ascii=False, indent=2)

    log(f"[검증] 완료. 결과 저장: {args.output}")
    log(f"[검증] 검증 이미지 수: {count}  평균 조향 오차: {avg_angle:.3f}  "
        f"최대 조향 오차: {max_angle:.3f}  평균 속도 오차: {avg_throttle:.3f}  검증 결과: {verdict}")


if __name__ == "__main__":
    main()
