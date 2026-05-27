#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Donkey Car model training script.
Supports donkeycar pipeline and standalone Keras training.

Usage:
    train.py [--tubs=tubs] (--model=<model>)
    [--type=(linear|inferred|tensorrt_linear|tflite_linear)]
    [--comment=<comment>]

Options:
    -h --help              Show this screen.
"""

import io
import sys

# Windows cp949 환경에서 한글/특수문자 출력 깨짐 방지
sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8', errors='replace')
sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding='utf-8', errors='replace')

from docopt import docopt
import json
import os
import glob
import time

# ──────────────────────────────────────────────────────────────────────────────
# 1. donkeycar 파이프라인으로 학습
# ──────────────────────────────────────────────────────────────────────────────

def build_donkeycar_config(tubs, model, model_type):
    """donkeycar config 객체를 직접 구성합니다 (config.py 파일 불필요)."""
    import donkeycar as dk

    cfg = dk.load_config(config_path=None)          # 빈 기본 config 로드

    # 기본 학습 파라미터 덮어쓰기
    cfg.BATCH_SIZE       = 64
    cfg.TRAIN_TEST_SPLIT = 0.8
    cfg.MAX_EPOCHS       = 100
    cfg.EARLY_STOP_PATIENCE = 5
    cfg.MIN_DELTA        = 0.0005
    cfg.PRINT_MODEL_SUMMARY = True
    cfg.MODEL_CATEGORICAL_MAX_THROTTLE_RANGE = 0.5
    cfg.SEQUENCE_LENGTH  = 3
    cfg.VERBOSE_TRAIN    = True

    # 입력 크기 설정 (donkeycar 표준)
    cfg.IMAGE_W   = 160
    cfg.IMAGE_H   = 120
    cfg.IMAGE_DEPTH = 3
    cfg.TARGET_W  = 160
    cfg.TARGET_H  = 120
    cfg.TARGET_D  = 3

    # 데이터 경로
    cfg.DATA_PATH = tubs

    return cfg


def train_with_donkeycar(tubs, model, model_type, comment):
    """donkeycar 공식 파이프라인으로 학습합니다."""
    try:
        import donkeycar as dk
        from donkeycar.pipeline.training import train

        cfg = build_donkeycar_config(tubs, model, model_type)
        print("[donkeycar] Config 구성 완료 - 학습 시작...")
        train(cfg, tubs, model, model_type, comment)
        return True

    except ImportError as e:
        print(f"[donkeycar] 설치되지 않음 - 독립 Keras 모드로 전환: {e}")
        return False
    except Exception as e:
        print(f"[donkeycar] 학습 오류 - 독립 Keras 모드로 전환: {e}", file=sys.stderr)
        return False


# ──────────────────────────────────────────────────────────────────────────────
# 2. 독립 Keras 학습 (donkeycar 없이도 동작)
# ──────────────────────────────────────────────────────────────────────────────

# Tub 레코드에서 이미지 경로·각도·스로틀을 읽어옵니다.
def load_tub_records(tub_path):
    records = []

    # 1) catalog 방식 (donkeycar >= 4.x)
    catalog_files = sorted(glob.glob(os.path.join(tub_path, "catalog_*.catalog")))
    if catalog_files:
        manifest_path = os.path.join(tub_path, "manifest.json")
        try:
            with open(manifest_path, "r") as f:
                manifest = json.load(f)
            inputs  = manifest.get("inputs", [])
            img_key = next((k for k in inputs if "image" in k.lower()), "cam/image_array")
        except Exception:
            img_key = "cam/image_array"

        for cat_file in catalog_files:
            with open(cat_file, "r") as f:
                for line in f:
                    line = line.strip()
                    if not line:
                        continue
                    try:
                        rec = json.loads(line)
                        img_rel = rec.get(img_key, "")
                        img_path = os.path.join(tub_path, "images", img_rel) if img_rel else ""
                        angle    = float(rec.get("user/angle",    rec.get("angle",    0.0)))
                        throttle = float(rec.get("user/throttle", rec.get("throttle", 0.0)))
                        if img_path and os.path.exists(img_path):
                            records.append((img_path, angle, throttle))
                    except Exception:
                        continue
        if records:
            return records

    # 2) record_NNNN.json 방식 (donkeycar <= 3.x)
    rec_files = sorted(glob.glob(os.path.join(tub_path, "record_*.json")))
    for rec_file in rec_files:
        try:
            with open(rec_file, "r") as f:
                rec = json.load(f)
            img_rel  = rec.get("cam/image_array", "")
            img_path = os.path.join(tub_path, img_rel) if img_rel else ""
            angle    = float(rec.get("user/angle",    rec.get("angle",    0.0)))
            throttle = float(rec.get("user/throttle", rec.get("throttle", 0.0)))
            if img_path and os.path.exists(img_path):
                records.append((img_path, angle, throttle))
        except Exception:
            continue

    return records


def build_linear_model(img_h=120, img_w=160, img_d=3):
    """Linear 모델 (donkeycar linear 모델과 동일한 구조)."""
    from tensorflow import keras
    from tensorflow.keras import layers

    inp = keras.Input(shape=(img_h, img_w, img_d), name="img_in")
    x = layers.Conv2D(24, (5, 5), strides=(2, 2), activation="relu")(inp)
    x = layers.Conv2D(32, (5, 5), strides=(2, 2), activation="relu")(x)
    x = layers.Conv2D(64, (5, 5), strides=(2, 2), activation="relu")(x)
    x = layers.Conv2D(64, (3, 3), strides=(1, 1), activation="relu")(x)
    x = layers.Conv2D(64, (3, 3), strides=(1, 1), activation="relu")(x)
    x = layers.Flatten()(x)
    x = layers.Dense(100, activation="relu")(x)
    x = layers.Dropout(0.1)(x)
    x = layers.Dense(50,  activation="relu")(x)
    x = layers.Dropout(0.1)(x)

    angle    = layers.Dense(1, activation="linear", name="angle")(x)
    throttle = layers.Dense(1, activation="linear", name="throttle")(x)

    model = keras.Model(inputs=inp, outputs=[angle, throttle])
    model.compile(
        optimizer="adam",
        loss={"angle": "mse", "throttle": "mse"},
        loss_weights={"angle": 0.9, "throttle": 0.01},
        metrics={"angle": "mae", "throttle": "mae"},
    )
    return model


class EpochLogger:
    """에포크별 진행 상황을 C# 파서가 읽을 수 있는 형식으로 출력합니다."""
    def __init__(self, total_epochs):
        self.total = total_epochs

    def on_epoch_end(self, epoch, logs=None):
        logs = logs or {}
        # angle 손실만 대표 loss로 사용 (val split 없을 수도 있음)
        loss     = logs.get("loss",     logs.get("angle_loss",     0))
        val_loss = logs.get("val_loss", logs.get("val_angle_loss", None))

        ep = epoch + 1
        msg = f"Epoch {ep}/{self.total} - loss: {loss:.4f}"
        if val_loss is not None:
            msg += f" - val_loss: {val_loss:.4f}"
        print(msg, flush=True)


def train_with_keras(tubs, model_path, model_type, comment):
    """독립 Keras 학습 - donkeycar 없이도 실제 모델(.h5)을 생성합니다."""
    try:
        import numpy as np
        from PIL import Image as PILImage
        import tensorflow as tf
    except ImportError as e:
        print(f"ERROR: 필수 패키지 없음 ({e}). tensorflow, numpy, Pillow 를 설치하세요.", file=sys.stderr)
        return False

    IMG_H, IMG_W = 120, 160

    # ── 데이터 로드 ───────────────────────────────────────────
    print(f"[Keras] 데이터 로드 중: {tubs}")
    records = load_tub_records(tubs)

    if not records:
        print(f"ERROR: 학습 레코드를 찾을 수 없습니다: {tubs}", file=sys.stderr)
        return False

    print(f"[Keras] 레코드 수: {len(records)}")

    images   = []
    angles   = []
    throttles = []

    for i, (img_path, angle, throttle) in enumerate(records):
        try:
            img = PILImage.open(img_path).convert("RGB").resize((IMG_W, IMG_H))
            images.append(np.array(img, dtype=np.float32) / 255.0)
            angles.append(angle)
            throttles.append(throttle)
        except Exception as ex:
            print(f"  이미지 로드 실패 ({img_path}): {ex}", file=sys.stderr)
            continue

        if (i + 1) % 200 == 0:
            print(f"  {i + 1}/{len(records)} 이미지 로드 완료...")

    X = np.array(images,    dtype=np.float32)
    Y_angle    = np.array(angles,    dtype=np.float32)
    Y_throttle = np.array(throttles, dtype=np.float32)

    print(f"[Keras] 데이터셋 크기: {X.shape}")

    # ── 모델 구성 ─────────────────────────────────────────────
    print("[Keras] 모델 구성 중...")
    model = build_linear_model(IMG_H, IMG_W, 3)
    model.summary()

    # ── 학습 파라미터 ─────────────────────────────────────────
    EPOCHS     = 100
    BATCH_SIZE = 64
    VAL_SPLIT  = 0.2

    from tensorflow.keras.callbacks import (
        EarlyStopping, ModelCheckpoint, ReduceLROnPlateau, Callback
    )

    class _EpochLogger(Callback):
        def __init__(self, total):
            super().__init__()
            self.total = total
        def on_epoch_end(self, epoch, logs=None):
            logs = logs or {}
            loss     = logs.get("loss",     logs.get("angle_loss",     0))
            val_loss = logs.get("val_loss", logs.get("val_angle_loss", None))
            ep  = epoch + 1
            msg = f"Epoch {ep}/{self.total} - loss: {loss:.4f}"
            if val_loss is not None:
                msg += f" - val_loss: {val_loss:.4f}"
            print(msg, flush=True)

    callbacks = [
        EarlyStopping(monitor="val_loss", patience=15, min_delta=0.0001, restore_best_weights=True),
        ReduceLROnPlateau(monitor="val_loss", factor=0.5, patience=7, min_lr=1e-7),
        _EpochLogger(EPOCHS),
    ]

    # 모델 저장 폴더 생성
    model_dir = os.path.dirname(model_path)
    if model_dir:
        os.makedirs(model_dir, exist_ok=True)

    # 중간 체크포인트도 저장
    ckpt_path = model_path.replace(".h5", "_best.h5")
    callbacks.append(
        ModelCheckpoint(filepath=ckpt_path, monitor="val_loss",
                        save_best_only=True, verbose=0)
    )

    # ── 학습 실행 ─────────────────────────────────────────────
    print(f"[Keras] 학습 시작  (epochs={EPOCHS}, batch={BATCH_SIZE}, val_split={VAL_SPLIT})")
    t0 = time.time()

    history = model.fit(
        X,
        {"angle": Y_angle, "throttle": Y_throttle},
        epochs=EPOCHS,
        batch_size=BATCH_SIZE,
        validation_split=VAL_SPLIT,
        callbacks=callbacks,
        verbose=0,          # 진행은 _EpochLogger 로 출력
        shuffle=True,
    )

    elapsed = time.time() - t0
    print(f"[Keras] 학습 완료  ({elapsed:.1f}초)")

    # ── 모델 저장 ─────────────────────────────────────────────
    model.save(model_path)
    print(f"[Keras] 모델 저장 완료: {model_path}")

    # 메타데이터 사이드카 저장
    meta = {
        "type":    model_type,
        "records": len(records),
        "comment": comment,
        "created": time.strftime("%Y-%m-%d %H:%M:%S"),
        "mode":    "keras",
        "epochs_run": len(history.history.get("loss", [])),
        "final_loss": float(history.history["loss"][-1]) if history.history.get("loss") else None,
    }
    meta_path = model_path.replace(".h5", "_meta.json")
    with open(meta_path, "w") as f:
        json.dump(meta, f, indent=2)
    print(f"[Keras] 메타데이터 저장 완료: {meta_path}")

    return True


# ──────────────────────────────────────────────────────────────────────────────
# 진입점
# ──────────────────────────────────────────────────────────────────────────────

def main():
    args = docopt(__doc__)
    tubs       = args.get("--tubs",    ".")
    model      = args.get("--model")
    model_type = args.get("--type",    "linear") or "linear"
    comment    = args.get("--comment", "") or ""

    if not model:
        print("ERROR: --model 파라미터가 필요합니다.", file=sys.stderr)
        sys.exit(1)

    print("=" * 60)
    print("[Training Parameters]")
    print(f"  Data folder : {tubs}")
    print(f"  Model path  : {model}")
    print(f"  Model type  : {model_type}")
    print(f"  Comment     : {comment}")
    print("=" * 60)

    # 1순위: donkeycar 공식 파이프라인
    if train_with_donkeycar(tubs, model, model_type, comment):
        sys.exit(0)

    # 2순위: 독립 Keras 학습
    print("[Training] donkeycar 파이프라인 불가 - 독립 Keras 학습으로 전환합니다.")
    if train_with_keras(tubs, model, model_type, comment):
        sys.exit(0)

    print("ERROR: 모든 학습 방법이 실패했습니다.", file=sys.stderr)
    sys.exit(1)


if __name__ == "__main__":
    main()

