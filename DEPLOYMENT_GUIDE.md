# SimpleDonkeyManager — 환경 구축 및 배포 가이드

> **다른 PC에서 WinForms 앱만으로 실제 DonkeyCar 모델 학습이 가능하려면**
> 아래 단계에 따라 환경을 구축하세요.

---

## 목차

1. [시스템 요구사항](#1-시스템-요구사항)
2. [.NET 10 설치](#2-net-10-설치)
3. [Python 3.11 설치](#3-python-311-설치)
4. [학습 환경 구축 (가상환경 + 패키지)](#4-학습-환경-구축)
5. [환경 동작 확인](#5-환경-동작-확인)
6. [앱 배포 패키지 구성](#6-앱-배포-패키지-구성)
7. [학습 실행 흐름](#7-학습-실행-흐름)
8. [트러블슈팅](#8-트러블슈팅)
9. [빠른 점검 체크리스트](#9-빠른-점검-체크리스트)

---

## 1. 시스템 요구사항

| 항목 | 최소 | 권장 |
|------|------|------|
| OS | Windows 10 64비트 | Windows 11 64비트 |
| RAM | 8 GB | 16 GB 이상 |
| 저장 공간 | 10 GB 이상 여유 | 20 GB 이상 |
| GPU | 없어도 됨 (CPU 학습) | NVIDIA GPU + CUDA (학습 속도 향상) |
| .NET | 10 Runtime | 10 SDK |
| Python | 3.10 | 3.11 |

---

## 2. .NET 10 설치

앱 실행에 필요합니다.

```powershell
# 버전 확인 (이미 설치된 경우)
dotnet --version
```

설치되지 않았다면:
👉 https://dotnet.microsoft.com/download/dotnet/10.0
→ **".NET Runtime 10.x"** 다운로드 후 설치 (SDK도 가능)

---

## 3. Python 3.11 설치

학습 스크립트(`python/train.py`) 실행에 필요합니다.

### 설치

👉 https://www.python.org/downloads/release/python-3119/
→ **Windows installer (64-bit)** 다운로드

> ⚠️ 설치 화면에서 **"Add Python 3.11 to PATH"** 반드시 체크하세요!

### 설치 확인

```powershell
python --version
# Python 3.11.x
```

---

## 4. 학습 환경 구축

### 방법 A — 로컬 가상환경 (권장)

프로젝트 루트 폴더에 `donkey_env` 가상환경을 만드는 방법입니다.
앱이 자동으로 이 환경을 우선적으로 사용합니다.

```powershell
# 프로젝트 폴더로 이동
cd C:\path\to\SimpleDonkeyManager

# 가상환경 생성
python -m venv donkey_env

# 가상환경 활성화
.\donkey_env\Scripts\Activate.ps1

# pip 업그레이드
python -m pip install --upgrade pip
```

#### 4-A-1. donkeycar 설치

```powershell
# donkeycar 설치 (학습 파이프라인 핵심)
pip install donkeycar

# 설치 확인
python -c "import donkeycar; print('donkeycar', donkeycar.__version__)"
```

#### 4-A-2. TensorFlow 설치 (donkeycar가 없거나 fallback 학습용)

donkeycar 설치 시 TensorFlow가 자동으로 포함되는 경우가 많지만,
없을 경우 수동 설치합니다.

```powershell
# CPU 버전 (범용)
pip install tensorflow

# 설치 확인
python -c "import tensorflow as tf; print('tensorflow', tf.__version__)"
```

> GPU가 있는 경우 [GPU 가속 설정](#gpu-가속-선택-사항) 참조

#### 4-A-3. 나머지 필수 패키지 설치

```powershell
pip install numpy Pillow docopt
```

#### 4-A-4. 전체 필수 패키지 한 번에 설치

```powershell
pip install donkeycar tensorflow numpy Pillow docopt
```

---

### 방법 B — 시스템 전역 Python 사용

가상환경 없이 시스템 Python에 직접 설치하는 방법입니다.

```powershell
pip install donkeycar tensorflow numpy Pillow docopt
```

앱은 `donkey_env`가 없으면 자동으로 시스템 `python` 명령을 사용합니다.

---

### GPU 가속 (선택 사항)

NVIDIA GPU가 있는 경우 학습 속도를 크게 향상시킬 수 있습니다.

#### 사전 준비
1. [NVIDIA 드라이버](https://www.nvidia.com/Download/index.aspx) 최신 버전 설치
2. [CUDA Toolkit 11.8 또는 12.x](https://developer.nvidia.com/cuda-downloads) 설치
3. [cuDNN](https://developer.nvidia.com/cudnn) 설치 (CUDA 버전에 맞는 것)

#### GPU 버전 TensorFlow 설치

```powershell
# CUDA 12.x 기준
pip install tensorflow[and-cuda]

# GPU 인식 확인
python -c "import tensorflow as tf; print('GPU:', tf.config.list_physical_devices('GPU'))"
```

> GPU 목록에 장치가 출력되면 성공입니다.

---

## 5. 환경 동작 확인

학습을 실행하기 전에 아래를 확인하세요.

### train.py 직접 실행 테스트

```powershell
# 가상환경 활성화 후
.\donkey_env\Scripts\Activate.ps1

# 도움말 출력 (패키지 오류 없이 출력되면 환경 정상)
python python\train.py --help
```

### 학습 시험 실행

```powershell
python python\train.py `
    --tubs "C:\path\to\your\tub_data" `
    --model "C:\path\to\output\model.h5" `
    --type linear
```

정상 출력 예시:
```
============================================================
[Training Parameters]
  Data folder : C:\...\tub_data
  Model path  : C:\...\model.h5
  Model type  : linear
============================================================
[Keras] 데이터 로드 중: C:\...\tub_data
[Keras] 레코드 수: 1537
[Keras] 모델 구성 중...
[Keras] 학습 시작  (epochs=100, batch=64, val_split=0.2)
Epoch 1/100 - loss: 0.2341 - val_loss: 0.2108
Epoch 2/100 - loss: 0.1876 - val_loss: 0.1754
...
[Keras] 학습 완료  (342.5초)
[Keras] 모델 저장 완료: C:\...\model.h5
```

---

## 6. 앱 배포 패키지 구성

### 포함 필수

```
SimpleDonkeyManager/
├── bin/Release/net10/           ← 컴파일된 WinForms 앱
│   ├── SimpleDonkeyManager.exe
│   ├── SimpleDonkeyManager.dll
│   └── *.dll  (의존성)
└── python/
    └── train.py                 ← 학습 스크립트 (필수!)
```

### 포함 불필요 (배포 시 제외)

```
❌ donkey_env/        — 대상 PC에서 직접 구축 (위 4단계 참조)
❌ bin/Debug/         — 디버그 빌드
❌ .git/              — 버전 관리
❌ .vs/               — Visual Studio 캐시
❌ test_data/         — 테스트 데이터
```

### 자체 포함(Self-Contained) 배포 빌드

```powershell
dotnet publish -c Release -o publish --self-contained -r win-x64
```

→ `publish/` 폴더에 .NET Runtime이 포함된 단독 실행 패키지가 생성됩니다.
이 경우 대상 PC에 .NET 설치가 불필요합니다.

---

## 7. 학습 실행 흐름

앱에서 학습 버튼을 누르면 내부적으로 다음 순서로 동작합니다.

```
[WinForms 앱]
    │
    ├─ Python 실행 파일 탐색
    │   ├─ (1순위) donkey_env\Scripts\python.exe   ← 로컬 가상환경
    │   └─ (2순위) python                           ← 시스템 전역 Python
    │
    └─ python\train.py 실행
        │
        ├─ (1순위) donkeycar 공식 파이프라인으로 학습
        │       donkeycar.pipeline.training.train()
        │
        └─ (2순위) 독립 Keras 학습 (donkeycar 없을 때 자동 전환)
                TensorFlow Keras CNN 모델 학습
                → 실제 .h5 모델 파일 생성
                → _meta.json 메타데이터 사이드카 생성
```

### 생성되는 파일

| 파일 | 설명 |
|------|------|
| `model_YYYYMMDD_HHmmss.h5` | 실제 학습된 Keras 모델 |
| `model_YYYYMMDD_HHmmss_best.h5` | 검증 손실 기준 최고 체크포인트 |
| `model_YYYYMMDD_HHmmss_meta.json` | 학습 메타데이터 (에포크 수, 손실값 등) |

---

## 8. 트러블슈팅

### ❌ "No module named 'tensorflow'"

TensorFlow가 설치되지 않았습니다.

```powershell
.\donkey_env\Scripts\Activate.ps1
pip install tensorflow
```

---

### ❌ "No module named 'donkeycar'"

donkeycar가 설치되지 않았습니다.
독립 Keras 학습으로 자동 전환되지만, donkeycar 파이프라인을 원한다면:

```powershell
pip install donkeycar
```

---

### ❌ "학습 레코드를 찾을 수 없습니다"

데이터 폴더 구조가 올바르지 않습니다.
지원하는 DonkeyCar Tub 형식:

```
tub_data/              ← 앱에서 선택하는 폴더 (donkeycar 4.x 형식)
├── catalog_0.catalog
├── manifest.json
└── images/
    ├── 0_cam_image_array_.jpg
    └── ...

또는

tub_data/              ← donkeycar 3.x 형식
├── record_000001.json
├── record_000002.json
└── 0_cam-image_array_.jpg
```

---

### ❌ "python not found" / Python을 찾을 수 없음

환경 변수에 Python 경로를 추가하세요:

1. Windows 검색 → "환경 변수 편집" → 시스템 환경 변수 → Path
2. 추가:
   - `C:\Users\{사용자명}\AppData\Local\Programs\Python\Python311`
   - `C:\Users\{사용자명}\AppData\Local\Programs\Python\Python311\Scripts`
3. PowerShell 재시작

---

### ❌ 학습이 너무 오래 걸림

- 앱의 학습 타임아웃은 **3시간**입니다.
- CPU 학습 시 데이터 1,000장 기준 약 10~40분 소요됩니다.
- 학습 로그 창에서 `Epoch N/100`이 출력되고 있으면 정상 진행 중입니다.
- GPU 환경이 있다면 [GPU 가속 설정](#gpu-가속-선택-사항)을 참고하세요.

---

### ❌ 모델 파일이 "# Mock Donkey Car Model" 로 시작함

이전 버전의 `train.py`가 사용 중인 경우입니다.
최신 `python/train.py`로 교체하세요 (이 저장소의 파일을 사용).

---

### ❌ PowerShell 스크립트 실행 정책 오류

```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

---

## 9. 빠른 점검 체크리스트

다른 PC에서 배포 전 다음을 확인하세요.

- [ ] `dotnet --version` → `10.x.x` 출력
- [ ] `python --version` → `3.10.x` 또는 `3.11.x` 출력
- [ ] `python -c "import tensorflow"` → 오류 없음
- [ ] `python -c "import donkeycar"` → 오류 없음 *(선택)*
- [ ] `python -c "import numpy, PIL, docopt"` → 오류 없음
- [ ] `python python\train.py --help` → 사용법 출력
- [ ] SimpleDonkeyManager.exe 실행 → 앱 정상 구동
- [ ] 학습 탭에서 데이터 폴더 선택 → 레코드 수 표시
- [ ] 학습 시작 → 로그 창에 `Epoch 1/100` 출력 확인
- [ ] 학습 완료 → `.h5` 파일 생성 확인 (텍스트 파일이 아닌 바이너리)

---

**버전**: 2026-05-28
**상태**: ✅ 실제 Keras 학습 지원 (donkeycar 또는 독립 TensorFlow)

<!-- ============================================================
     아래는 이전 버전 내용입니다 (참고용으로 보존)
     ============================================================ -->
# Python 3.11 설치 후
pip install donkeycar
```

### 3단계: SimpleDonkeyManager 실행
```bash
# 컴파일된 exe 실행 또는
dotnet SimpleDonkeyManager.dll
```

**끝!** 이게 전부입니다.

---

## 📦 배포 패키지 구성

### 배포해야 할 파일들
```
SimpleDonkeyManager/
├── bin/Release/net10/           ← 컴파일된 파일들
│   ├── SimpleDonkeyManager.exe
│   ├── SimpleDonkeyManager.dll
│   └── ... (의존성 DLL들)
├── python/
│   └── train.py                 ← 필수!
├── config.py                    ← 필수!
└── README.md                    ← 사용 설명서
```

### 배포하지 않아도 되는 파일들
```
❌ donkey_env/                   (로컬 가상환경 - 선택사항)
❌ test_data/                    (테스트 데이터)
❌ test_models/                  (테스트 모델)
❌ bin/Debug/                    (디버그 빌드)
❌ .git/                         (버전 관리)
❌ .vs/                          (VS 캐시)
```

---

## 🔄 Python 실행 우선순위

애플리케이션은 다음 순서로 Python을 검색합니다:

1. **로컬 가상환경** (있으면 사용)
   ```
   SimpleDonkeyManager/donkey_env/Scripts/python.exe
   ```

2. **시스템 전역 Python** (없으면 이것 사용)
   ```
   python (PATH에서 검색)
   ```

### 예시

#### 시나리오 1: 시스템 Python만 있는 경우
```bash
# 필요
pip install donkeycar

# SimpleDonkeyManager 실행
SimpleDonkeyManager.exe
# → train.py 실행할 때 시스템 Python 사용
```

#### 시나리오 2: 로컬 가상환경도 있는 경우
```bash
# 프로젝트 폴더에서
python -m venv donkey_env
.\donkey_env\Scripts\activate
pip install donkeycar

# SimpleDonkeyManager 실행
SimpleDonkeyManager.exe
# → train.py 실행할 때 로컬 가상환경 Python 사용
```

---

## 🛠️ 트러블슈팅

### 문제 1: "python not found" 또는 Donkeycar를 찾을 수 없음

**원인**: Python에 Donkeycar가 설치되지 않았음

**해결책**:
```bash
pip install donkeycar
```

### 문제 2: train.py를 찾을 수 없음

**원인**: `python` 폴더가 없거나 train.py 파일이 없음

**확인 방법**:
```
SimpleDonkeyManager/
└── python/
	└── train.py     ← 이 파일이 있어야 함
```

**해결책**: 배포 패키지에 python/ 폴더와 train.py 포함

### 문제 3: config.py를 찾을 수 없음

**원인**: config.py 파일이 프로젝트 루트에 없음

**해결책**:
```
SimpleDonkeyManager/
└── config.py       ← 이 파일이 프로젝트 루트에 있어야 함
```

### 문제 4: .NET 10이 설치되지 않았음

**확인**:
```bash
dotnet --version
```

**해결책**:
```bash
# .NET 10 Runtime 또는 SDK 설치
# https://dotnet.microsoft.com/download/dotnet/10.0
```

---

## 📊 작동 확인

애플리케이션에서 "학습" 탭의 로그를 확인하면:

### ✅ 정상 작동
```
[학습] Python 스크립트 검색 시작...
[학습]   BaseDirectory: C:\...
[학습]   검색 위치: C:\...\python\train.py
[학습]   ✓ 찾음!
[학습] 시스템 전역 Python 사용: python
[학습] 실행 명령어: python "C:\...\train.py" ...
[학습] Epoch 1/10
...
```

### ⚠️ 문제 있을 때
```
[학습 경고] Python 스크립트를 찾을 수 없음: train.py
```
→ `python/train.py` 파일 확인

```
[학습] Donkeycar not installed, using mock training mode...
```
→ `pip install donkeycar` 실행

---

## 🎯 권장 배포 방식

### 방식 1: 간단한 배포 (권장)
```
1. bin/Release/net10/ 전체 폴더 복사
2. python/ 폴더 복사
3. config.py 복사
4. 대상 PC에 .NET 10, Python, Donkeycar 설치
5. exe 실행
```

### 방식 2: 자체 포함 배포
```
1. 프로젝트 루트에서:
   dotnet publish -c Release -o "publish" --self-contained

2. publish 폴더:
   - SimpleDonkeyManager.exe
   - 모든 .NET 의존성 (자동 포함)

3. python/ 폴더 복사
4. config.py 복사
5. 대상 PC에 Python, Donkeycar만 설치
6. exe 실행
```

---

## 🔧 Windows에서 Python PATH 설정

Python을 설치했는데 `python` 명령어가 작동하지 않으면:

1. **설치 시 "Add Python to PATH" 체크 (재설치)**
   - Python 설치 프로그램에서 "Add Python 3.11 to PATH" 체크

2. **수동으로 PATH 추가**
   - Windows 검색 → "환경 변수" 검색
   - "시스템 환경 변수 편집" → "환경 변수" 버튼
   - "Path" 선택 → "편집"
   - 새로 추가: `C:\Users\{username}\AppData\Local\Programs\Python\Python311`
   - 확인 후 PowerShell 재시작

---

## 📝 체크리스트

다른 PC에서 배포하기 전 확인:

- [ ] .NET 10 Runtime 설치 테스트
- [ ] `python --version` 작동 확인
- [ ] `pip install donkeycar` 성공 확인
- [ ] `python python/train.py --help` 실행 테스트
- [ ] SimpleDonkeyManager.exe 실행 테스트
- [ ] 로그에서 Python 경로가 정확하게 표시되는지 확인
- [ ] 테스트 데이터로 학습 시작 테스트

---

## 🎓 최종 요약

**한 문장으로:**
> Donkeycar가 설치된 Python 3.11+과 .NET 10만 있으면, 
> SimpleDonkeyManager.exe와 python/train.py, config.py만으로 완전히 작동합니다.

**필요한 것:**
- .NET 10
- Python 3.11+ (with `pip install donkeycar`)
- python/train.py (프로젝트에 포함)
- config.py (프로젝트에 포함)

**더 이상 필요 없는 것:**
- donkey_env (로컬 가상환경)
- 다른 의존성 라이브러리 설치

---

**버전**: 2026-05-27
**상태**: ✅ 다중 PC 배포 준비 완료
