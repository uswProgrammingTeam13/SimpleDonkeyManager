# SimpleDonkeyManager — 환경 구축 및 배포 가이드

> **다른 PC에서 WinForms 앱만으로 실제 DonkeyCar 모델 학습이 가능하려면**
> 아래 단계에 따라 환경을 구축하세요.

---

## 🚀 빠른 시작 (자동화 스크립트 사용) - 권장

**가장 간단하고 안전한 방법입니다.**

### 1단계: PowerShell 관리자 권한 확인

Windows 검색 → `powershell` → **우클릭 "관리자로 실행"**

### 2단계: 자동 설치 스크립트 실행

```powershell
# 프로젝트 폴더로 이동
cd C:\Users\사용자명\source\repos\SimpleDonkeyManager

# 자동 설치 시작 (5~10분 소요)
powershell.exe -ExecutionPolicy Bypass -File setup-environment.ps1
```

### 스크립트가 자동으로 처리하는 작업

✅ `.NET 10` 버전 확인  
✅ `Python 3.10+` 설치 여부 확인  
✅ `donkey_env` 가상환경 생성 (없으면)  
✅ `pip` 최신 버전으로 업그레이드  
✅ 필수 패키지 자동 설치:
   - **donkeycar** (학습 파이프라인)
   - **tensorflow** (딥러닝 프레임워크)
   - **numpy** (수치 계산)
   - **Pillow** (이미지 처리)
   - **docopt** (명령줄 인자)

✅ 모든 설치 결과 확인 및 요약 출력

### 3단계: 앱 실행

```powershell
# Visual Studio에서 프로젝트 열기
start SimpleDonkeyManager.sln

# 또는 직접 실행
dotnet run
```

**완료! 🎉** 이제 모든 환경이 준비되었습니다.

---

## 목차

1. [빠른 자동 설치 (권장)](#빠른-시작-자동화-스크립트-사용---권장)
2. [시스템 요구사항](#시스템-요구사항)
3. [수동 설치 가이드](#수동-설치-가이드)
   - [.NET 10 설치](#net-10-설치)
   - [Python 3.11 설치](#python-311-설치)
   - [학습 환경 구축](#학습-환경-구축)
4. [환경 동작 확인](#환경-동작-확인)
5. [앱 배포 패키지 구성](#앱-배포-패키지-구성)
6. [학습 실행 흐름](#학습-실행-흐름)
7. [트러블슈팅](#트러블슈팅)
8. [빠른 참조](#빠른-참조)

---

## 시스템 요구사항

| 항목 | 최소 | 권장 |
|------|------|------|
| **OS** | Windows 10 64비트 | Windows 11 64비트 |
| **RAM** | 8 GB | 16 GB 이상 |
| **저장 공간** | 10 GB 여유 | 20 GB 이상 |
| **.NET** | 10 Runtime | 10 SDK |
| **Python** | 3.10 이상 | 3.11 |
| **GPU** | (선택) | NVIDIA GPU + CUDA (학습 가속) |

---

## 수동 설치 가이드

자동 스크립트가 작동하지 않는 경우에만 이 방법을 따르세요.

### .NET 10 설치

앱 실행에 필수입니다.

#### 버전 확인

```powershell
dotnet --version
```

- **이미 설치됨 (10.x.x 출력)**: 이 단계 스킵
- **설치 안 됨**: 아래 링크에서 설치

#### .NET 10 다운로드

👉 **https://dotnet.microsoft.com/download/dotnet/10.0**

**".NET 10 Runtime" 다운로드 → 설치**

설치 후 PowerShell 재시작:

```powershell
dotnet --version
# 예: 10.0.x
```

---

### Python 3.11 설치

학습 스크립트(`python/train.py`) 실행에 필수입니다.

#### 버전 확인

```powershell
python --version
```

- **이미 설치됨 (3.10 이상 출력)**: 이 단계 스킵
- **설치 안 됨**: 아래 링크에서 설치

#### Python 3.11 다운로드

👉 **https://www.python.org/downloads/release/python-3119/**

**"Windows installer (64-bit)" 다운로드 → 실행**

#### ⚠️ 중요: 설치 중 체크박스 확인

```
☑️ Add Python 3.11 to PATH     ← 반드시 체크!
☑️ Install pip
```

설치 후 PowerShell 재시작:

```powershell
python --version
# 예: Python 3.11.9
```

---

### 학습 환경 구축

#### 가상환경 생성

```powershell
# 프로젝트 폴더로 이동
cd C:\path\to\SimpleDonkeyManager

# 가상환경 생성
python -m venv donkey_env

# 가상환경 활성화 (PowerShell)
.\donkey_env\Scripts\Activate.ps1

# 프롬프트에 (donkey_env)가 붙으면 성공
```

#### pip 업그레이드

```powershell
python -m pip install --upgrade pip
```

#### 필수 패키지 설치 (한 번에)

> ⚠️ **버전 고정 필수**: donkeycar 5.3.0 은 특정 버전 조합에서만 정상 동작합니다.
> 최신 버전을 그냥 설치하면 학습 시 다음과 같은 오류가 발생합니다.
> - `ModuleNotFoundError: No module named 'albumentations'`
> - `AttributeError: 'Functional' object has no attribute 'input_names'` (Keras 3 비호환)
> - `numpy 2.x` 비호환 (donkeycar 는 `numpy<2.0` 요구)
>
> 아래 버전 조합을 **순서대로** 설치하세요. (또는 `resources\setup-environment.ps1` 한 번 실행으로 자동 처리)

```powershell
# 1) donkeycar 먼저 설치 (핵심 의존성 고정)
pip install donkeycar==5.3.0

# 2) numpy 1.x 고정 (TensorFlow 설치 전)
pip install "numpy==1.26.4"

# 3) TensorFlow 2.15 (Keras 2.x 포함 — donkeycar 학습 파이프라인 필수)
pip install tensorflow==2.15.1

# 4) 이미지 증강 (donkeycar.pipeline.augmentations 의존)
pip install albumentations==1.4.18 "opencv-python-headless==4.9.0.80"

# 5) 기타 유틸리티
pip install Pillow docopt h5py pyyaml

# 6) numpy 재고정 (transitive 의존성이 numpy 2.x 를 끌어올 수 있으므로 마지막에 다시 고정)
pip install "numpy==1.26.4"
```

**패키지 설명:**

| 패키지 | 고정 버전 | 설명 |
|--------|----------|------|
| **donkeycar** | 5.3.0 | 자율주행 학습 파이프라인 |
| **tensorflow** | 2.15.1 | 딥러닝 프레임워크 (Keras 2.x 포함) |
| **numpy** | 1.26.4 | 수치 계산 (donkeycar 는 `<2.0` 요구) |
| **albumentations** | 1.4.18 | 이미지 증강 (numpy 1.x 호환 마지막 버전대) |
| **opencv-python-headless** | 4.9.0.80 | albumentations 의존, numpy 1.x 호환 |
| **Pillow** | 최신 | 이미지 처리 |
| **docopt** | 최신 | 명령줄 인자 파싱 |

#### 설치 확인

```powershell
python -c "import tensorflow as tf; print('tensorflow', tf.__version__)"
python -c "import keras; print('keras', keras.__version__)"
python -c "import donkeycar; print('donkeycar', donkeycar.__version__)"
python -c "import numpy; print('numpy', numpy.__version__)"
python -c "import albumentations; print('albumentations', albumentations.__version__)"
python -c "from PIL import Image; print('Pillow OK')"
python -c "import docopt; print('docopt OK')"
# 학습 파이프라인 임포트 (가장 중요 — 오류 없으면 학습 준비 완료)
python -c "from donkeycar.pipeline.training import train; print('donkeycar training pipeline OK')"
```

---

### GPU 가속 (선택 사항)

NVIDIA GPU가 있는 경우 학습 속도를 **10배 이상** 향상시킬 수 있습니다.

#### 사전 준비

1. **NVIDIA 드라이버 최신 버전 설치**
   👉 https://www.nvidia.com/Download/index.aspx

2. **CUDA Toolkit 설치** (11.8 또는 12.x)
   👉 https://developer.nvidia.com/cuda-downloads

3. **cuDNN 설치** (CUDA 버전에 맞는 것)
   👉 https://developer.nvidia.com/cudnn

#### GPU 버전 TensorFlow 설치

```powershell
# 가상환경 활성화 상태에서
pip install tensorflow[and-cuda]

# GPU 인식 확인
python -c "import tensorflow as tf; print('GPU:', tf.config.list_physical_devices('GPU'))"
```

**정상 출력 예시:**
```
GPU: [PhysicalDevice(name='/physical_device:GPU:0', device_type='GPU')]
```

---

## 환경 동작 확인

### train.py 실행 테스트

```powershell
# 가상환경 활성화
.\donkey_env\Scripts\Activate.ps1

# 도움말 출력 (오류 없으면 정상)
python python\train.py --help
```

### 학습 시험 실행

```powershell
python python\train.py `
	--tubs "C:\path\to\your\tub_data" `
	--model "C:\path\to\output\model.h5" `
	--type linear
```

---

## 앱 배포 패키지 구성

### 필수 포함 파일

```
SimpleDonkeyManager/
├── bin/Release/net10/           ← 컴파일된 앱
│   ├── SimpleDonkeyManager.exe
│   ├── SimpleDonkeyManager.dll
│   └── *.dll (의존성)
└── python/
	└── train.py                 ← 필수!
```

### 배포 시 제외

```
❌ donkey_env/          — 대상 PC에서 직접 구축
❌ bin/Debug/           — 디버그 빌드
❌ .git/                — 버전 관리
❌ setup-environment.ps1  — 설치 완료 후 불필요
```

### 자체 포함 배포 빌드

.NET Runtime이 포함된 단독 실행 패키지:

```powershell
dotnet publish -c Release -o publish --self-contained -r win-x64
```

결과: `publish/` 폴더에 .NET Runtime 포함된 exe  
장점: 대상 PC에 .NET 설치 불필요  
단점: 파일 크기 증가 (~300MB)

---

## 학습 실행 흐름

앱에서 학습 버튼을 누르면:

```
[WinForms 앱]
	↓
[Python 실행 파일 탐색]
	├─ (1순위) donkey_env\Scripts\python.exe ← 로컬 가상환경
	└─ (2순위) python                        ← 시스템 전역 Python
	↓
[python\prepare_tub.py 실행] ← 학습 데이터 자동 준비
	├─ catalog/manifest 자동 생성 (구형 v3 → tub v2 변환)
	├─ config.py 자동 생성 (없을 경우)
	└─ 이미 변환된 폴더는 건너뜀
	↓
[python\train.py 실행]
	├─ (1순위) donkeycar 파이프라인으로 학습
	└─ (2순위) 독립 Keras 학습 (자동 전환)
	↓
[모델 파일 생성]
	├─ model_YYYYMMDD_HHmmss.h5       ← 학습된 모델
	├─ model_..._best.h5              ← 최고 체크포인트
	└─ model_..._meta.json            ← 메타데이터
```

---

## 트러블슈팅

### ❌ "python not found"

**해결:**

1. **Python 재설치** (권장)
   - https://www.python.org/downloads/
   - **"Add Python to PATH"** 체크 후 설치

2. **PATH 수동 추가**
   - Windows 검색 → "환경 변수 편집"
   - "Path" → "새로 추가":
	 - `C:\Users\{사용자명}\AppData\Local\Programs\Python\Python311`
   - PowerShell 재시작

---

### ❌ "No module named 'tensorflow'" / "No module named 'donkeycar'"

**해결:**

```powershell
# 가상환경 활성화
.\donkey_env\Scripts\Activate.ps1

# 패키지 설치
pip install tensorflow donkeycar
```

---

### ❌ "No module named 'albumentations'"

donkeycar 학습 파이프라인이 `albumentations` 에 의존하는데 설치되지 않은 경우입니다.

**해결:**

```powershell
.\donkey_env\Scripts\Activate.ps1
pip install albumentations==1.4.18 "opencv-python-headless==4.9.0.80" "numpy==1.26.4"
```

---

### ❌ "'Functional' object has no attribute 'input_names'" (Keras 3 비호환)

TensorFlow 2.16 이상은 Keras 3 를 기본 포함하는데, donkeycar 5.3.0 은 Keras 2.x API 를 사용합니다.

**해결: TensorFlow 2.15(Keras 2.x)로 고정**

```powershell
.\donkey_env\Scripts\Activate.ps1
pip uninstall -y tensorflow tensorflow-intel keras
pip install --no-cache-dir tensorflow==2.15.1
pip install "numpy==1.26.4"
```

확인:
```powershell
python -c "import keras; print(keras.__version__)"   # 2.15.x 여야 함
```

---

### ❌ "numpy 2.x incompatible" / donkeycar import 오류

donkeycar 5.3.0 은 `numpy<2.0` 을 요구합니다. albumentations/opencv 설치 시 numpy 2.x 가
함께 설치되면 충돌이 발생합니다.

**해결:**

```powershell
.\donkey_env\Scripts\Activate.ps1
pip install "numpy==1.26.4"
```

> 💡 가장 확실한 방법은 `resources\setup-environment.ps1` 을 실행하는 것입니다.
> 이 스크립트가 위의 모든 버전 충돌을 올바른 순서로 자동 해결합니다.

---

### ❌ "학습 레코드를 찾을 수 없습니다"

**데이터 폴더 구조 확인:**

**형식 1: DonkeyCar 4.x / 5.x (학습에 필요한 형식)**
```
tub_data/
├── manifest.json
├── catalog_0.catalog
├── catalog_0.catalog_manifest
└── images/
	└── 0_cam_image_array_.jpg
```

**형식 2: DonkeyCar 3.x (구형 — 자동 변환됨)**
```
tub_data/
├── meta.json
├── record_000001.json
└── 0_cam-image_array_.jpg
```

> 💡 **catalog 자동 생성**: 형식 2(구형, `record_*.json` + `meta.json`)처럼 `catalog`/`manifest.json`
> 이 없는 폴더로 학습을 시작하면, 앱이 학습 직전에 `python\prepare_tub.py` 를 자동 실행하여
> 형식 1(tub v2: `manifest.json` + `catalog` + `images/`)로 **자동 변환**합니다.
> 변환된 파일은 **원본 데이터 폴더 안에** 생성되며, 원본 이미지/레코드는 그대로 보존됩니다.
> 이미 변환된 폴더(`manifest.json` 존재)는 자동으로 건너뜁니다.
>
> 수동으로 변환하려면:
> ```powershell
> python python\prepare_tub.py --tubs "C:\path\to\donkey" --config-dir "python"
> ```

> 💡 **config.py 자동 생성**: 학습에는 `python\config.py` 가 필요합니다. 없으면
> `prepare_tub.py`(또는 `setup-environment.ps1`)가 donkeycar 기본 템플릿(`cfg_complete.py`)을
> 복사하여 자동 생성합니다.

---

### ❌ 학습이 너무 오래 걸림

**해결 방법:**

1. **GPU 설정** (가장 효과적)
   - [GPU 가속 설정](#gpu-가속-선택-사항) 참조
   - GPU 사용 시 10~100배 빠름

2. **모델 변경**
   - Linear (빠름) < Categorical < 3D CNN (느림)

3. **데이터 필터링**
   - 불필요한 프레임 제거
   - 데이터양 감소로 학습 시간 단축

4. **배치 크기 조정** (train.py에서)
   ```python
   cfg.BATCH_SIZE = 128  # 기본값 64에서 증가
   ```

---

### ❌ 메모리 부족 오류

**해결:**

```python
# python/train.py 에서 수정:
cfg.BATCH_SIZE = 32        # 감소
cfg.IMAGE_W = 120          # 감소
cfg.IMAGE_H = 80           # 감소
```

---

### ❌ PowerShell 스크립트 실행 정책 오류

**해결:**

```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

---

## 빠른 참조

### 자동 설치
```powershell
powershell.exe -ExecutionPolicy Bypass -File setup-environment.ps1
```

### 가상환경 관리
```powershell
# 활성화
.\donkey_env\Scripts\Activate.ps1

# 비활성화
deactivate
```

### 패키지 관리
```powershell
# 설치
pip install donkeycar tensorflow numpy Pillow docopt

# 확인
pip list | findstr "tensorflow donkeycar"
```

### 앱 빌드/실행
```powershell
# 빌드
dotnet build

# 실행
dotnet run

# 릴리스 빌드
dotnet publish -c Release -o publish
```

### 체크리스트
```powershell
✓ dotnet --version           # 10.x.x
✓ python --version           # 3.10+
✓ python -c "import tensorflow"  # OK
✓ python -c "import donkeycar"   # OK
✓ python python\train.py --help  # 사용법 출력
```

---

**버전**: 2025-05  
**마지막 업데이트**: setup-environment.ps1 통합  
**상태**: ✅ 자동화 설치 스크립트 포함 - 가장 빠르고 안전한 방법
