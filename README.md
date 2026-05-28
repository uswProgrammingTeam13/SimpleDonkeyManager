# SimpleDonkeyManager

**자율주행 자동차 Donkeycar의 학습 데이터를 효율적으로 관리·처리하는 윈도우 데스크톱 애플리케이션**

---

## 🎯 프로젝트 개요

SimpleDonkeyManager는 **Donkeycar 자율주행 프로젝트**에서 수집된 대량의 주행 데이터(이미지 + 메타데이터)를 체계적으로 관리하고 전처리한 후, 머신러닝 모델을 학습시키는 **통합 관리 도구**입니다.

Windows 환경에서 **별도의 커맨드라인 작업 없이** GUI를 통해 전 과정을 처리할 수 있습니다.

### 주요 특징

- ✅ **직관적 GUI**: 4단계 워크플로우로 누구나 쉽게 사용 가능
- ✅ **데이터 시각화**: 이미지 미리보기, 메타데이터 확인, 차트 분석
- ✅ **스마트 필터링**: 각도, 스로틀, 해상도 기반 불필요한 데이터 제거
- ✅ **모델 학습 통합**: Linear, Categorical, 3D CNN 모델 지원
- ✅ **성능 분석**: 학습 곡선(Loss, Accuracy) 실시간 모니터링
- ✅ **.NET 10 + Python 파이프라인**: 고성능 C# UI + 파이썬 ML 백엔드

---

## 📋 사용 워크플로우

SimpleDonkeyManager는 4단계로 구성됩니다:

```
┌─────────────────────────────────────────────────────┐
│  ① 데이터 불러오기 (Data Load)                      │
│  • 주행 데이터 폴더 선택                             │
│  • 이미지 + JSON 메타데이터 스캔                     │
│  • 폴더 통계 확인 (이미지 수, 크기, 해상도)         │
└──────────────────┬──────────────────────────────────┘
                   │
┌──────────────────▼──────────────────────────────────┐
│  ② 데이터 필터링 (Data Filter)                      │
│  • 각도, 스로틀, 해상도로 필터링                    │
│  • 필터된 데이터 미리보기                            │
│  • 필터 없이 바로 학습 가능 (선택사항)               │
└──────────────────┬──────────────────────────────────┘
                   │
┌──────────────────▼──────────────────────────────────┐
│  ③ 학습 실행 (Training)                             │
│  • 모델 유형 선택 (Linear, Categorical, 3D CNN)    │
│  • 학습 설정 구성                                   │
│  • Donkeycar 파이프라인으로 모델 학습               │
└──────────────────┬──────────────────────────────────┘
                   │
┌──────────────────▼──────────────────────────────────┐
│  ④ 결과 확인 (Result)                               │
│  • 학습 곡선 시각화 (Loss, Accuracy)                │
│  • 모델 성능 분석                                   │
│  • 학습된 모델 저장/내보내기                         │
└─────────────────────────────────────────────────────┘
```

---

## 🛠️ 시스템 요구사항

| 항목 | 최소 | 권장 |
|------|------|------|
| **OS** | Windows 10 64비트 | Windows 11 64비트 |
| **RAM** | 8 GB | 16 GB 이상 |
| **.NET** | .NET 10 Runtime | .NET 10 SDK |
| **Python** | 3.10 이상 | 3.11 |
| **저장 공간** | 10 GB 여유 | 20 GB 이상 |
| **GPU** | (선택) | NVIDIA GPU + CUDA (학습 가속) |

---

## 🚀 설치 및 실행

### 1단계: 필수 환경 설치

#### .NET 10 설치
```powershell
# 버전 확인
dotnet --version

# 설치 필요 시
# https://dotnet.microsoft.com/download/dotnet/10.0
# → ".NET Runtime 10.x" 다운로드 및 설치
```

#### Python 3.11 설치
```powershell
# 버전 확인
python --version

# 설치 필요 시
# https://www.python.org/downloads/release/python-3119/
# ⚠️ 설치 시 "Add Python 3.11 to PATH" 반드시 체크
```

### 2단계: 학습 환경 구축

프로젝트 폴더에서:

```powershell
# 가상환경 생성
python -m venv donkey_env

# 가상환경 활성화
.\donkey_env\Scripts\Activate.ps1

# pip 업그레이드
python -m pip install --upgrade pip

# Donkeycar 설치
pip install donkeycar
```

### 3단계: 애플리케이션 실행

```powershell
# 프로젝트 폴더에서
dotnet run

# 또는 빌드 후 exe 실행
dotnet build
# 생성된 exe 파일 실행
```

---

## 📁 프로젝트 구조

```
SimpleDonkeyManager/
│
├── 📄 README.md                 # 본 파일
├── 📄 DEPLOYMENT_GUIDE.md       # 상세 배포 및 트러블슈팅 가이드
├── 📄 config.py                 # Donkeycar 설정 파일
│
├── 🔷 Program.cs                # .NET 진입점
├── 🔷 MainWindow.cs             # 메인 윈도우 (UI 컨트롤러)
├── 🔷 MainWindow.Designer.cs    # UI 디자인
│
├── 📂 controls/                 # UI 컨트롤 모음
│   ├── InitialScreen.cs         # ① 초기 화면 (환영 메시지)
│   ├── DataLoadControl.cs       # ① 데이터 불러오기 탭
│   ├── DataFilterControl.cs     # ② 데이터 필터링 탭
│   ├── TrainingControl.cs       # ③ 학습 실행 탭
│   └── ResultControl.cs         # ④ 결과 확인 탭
│
├── 📂 controlutils/             # UI 유틸리티
│   ├── ImageViewer.cs           # 이미지 뷰어
│   ├── ImageList.cs             # 이미지 리스트
│   └── ImageViewerUpper.cs      # 상단 이미지 뷰어
│
├── 📂 helptexts/                # 도움말 텍스트
│   ├── InitialHelp.txt
│   ├── DataLoadHelp.txt
│   ├── DataFilterHelp.txt
│   ├── TrainingHelp.txt
│   └── ResultHelp.txt
│
├── 📂 python/                   # Python 백엔드
│   └── train.py                 # Donkeycar 학습 스크립트
│
├── 📂 donkey_env/               # Python 가상환경 (자동 생성)
│
├── 🔷 Logger.cs                 # 로깅 유틸리티
├── 🔷 HelpManager.cs            # 도움말 관리자
├── 🔷 FrameData.cs              # 프레임 데이터 모델
├── 🔷 ChartDataModel.cs         # 차트 데이터 모델
└── 🔷 ImageManager.cs           # 이미지 처리 유틸리티
```

---

## 🎮 사용 방법

### 기본 흐름

1. **데이터 불러오기**
   - "① 데이터 불러오기" 탭 클릭
   - 폴더 선택 → `record_000.jpg`, `record_000.json` 등이 있는 주행 데이터 폴더 선택
   - "불러오기" 버튼으로 이미지 + 메타데이터 로드
   - 폴더 통계 확인 (이미지 수, 해상도, 파일 크기 등)
   - 이미지 미리보기로 데이터 품질 확인

2. **데이터 필터링 (선택사항)**
   - "② 데이터 필터링" 탭 클릭
   - 필터 조건 설정:
     - **각도 범위** (steering angle): -1.0 ~ 1.0
     - **스로틀 범위** (throttle): -1.0 ~ 1.0
     - **해상도**: 최소 너비/높이
   - 필터 미리보기로 선택된 프레임 확인
   - 필터 적용 → 필터링된 데이터 저장

3. **모델 학습**
   - "③ 학습 실행" 탭 클릭
   - 데이터 폴더 선택 (필터링된 폴더 또는 원본 폴더)
   - 모델 유형 선택:
     - **Linear**: 선형 회귀 (빠르고 가벼움)
     - **Categorical**: 카테고리 분류 (균형잡힌 성능)
     - **3D CNN**: 3D 합성곱신경망 (고정확도, 느림)
   - 모델 저장 경로 지정
   - "학습 시작" 버튼 → 백그라운드에서 학습 진행
   - 로그 창에서 실시간 학습 진행률 확인

4. **결과 확인**
   - "④ 결과 확인" 탭 클릭
   - 학습된 모델 파일 선택
   - 학습 곡선 시각화:
     - **Loss 그래프**: 학습 손실 변화
     - **Accuracy 그래프**: 정확도 변화
   - 모델 성능 요약 및 통계 확인
   - 모델 내보내기 (Donkeycar 호환 형식)

### 고급 기능

- **실시간 로그 뷰**: 우측 하단 로그 창에서 프로세스 진행 상황 모니터링
- **도움말 통합**: 각 탭의 좌측에 상세한 도움말 텍스트 제공
- **이미지 확대 뷰**: 이미지 클릭 시 큰 창에서 확인 가능
- **필터 프리셋 저장**: 자주 사용하는 필터 설정 저장 및 로드

---

## 📊 지원되는 데이터 형식

### 입력 형식
Donkeycar 표준 수집 형식:

```
data_folder/
├── record_000.jpg
├── record_000.json
├── record_001.jpg
├── record_001.json
├── ...
├── catalog_0.catalog
└── manifest.json
```

**record_XXX.json 예시:**
```json
{
  "cam/image_array": "record_000.jpg",
  "user/angle": 0.15,
  "user/throttle": 0.5,
  "user/mode": "local_angle",
  "timestamp": 1630703400.123
}
```

### 출력 형식

**학습된 모델**: Keras `.h5` 형식
- Linear, Categorical: `model.h5`
- 메타데이터: `model_metadata.json`

**필터링된 데이터**: 원본과 동일한 구조로 저장

---

## 🔧 설정 및 커스터마이징

### Python 설정 (config.py)

학습 파라미터는 `python/train.py`에서 직접 구성:

```python
cfg.BATCH_SIZE = 64              # 배치 크기
cfg.TRAIN_TEST_SPLIT = 0.8       # 학습:테스트 비율
cfg.MAX_EPOCHS = 100             # 최대 에포크
cfg.EARLY_STOP_PATIENCE = 5      # 조기 중단 대기 수
cfg.IMAGE_W, cfg.IMAGE_H = 160, 120  # 입력 이미지 크기
```

### C# 설정 (MainWindow.cs)

UI 기본값 및 동작은 `MainWindow.cs`에서 커스터마이징 가능:
- 기본 필터 값
- 로그 버퍼 크기
- 이미지 미리보기 크기

---

## 🐛 트러블슈팅

### 자주 묻는 질문

**Q: "Python을 찾을 수 없습니다" 오류**  
A: Python이 PATH에 등록되지 않았습니다.
```powershell
# Python 설치 시 "Add Python to PATH" 옵션을 체크했는지 확인
python --version

# 설치되지 않았다면
# https://www.python.org/downloads/ 에서 재설치 (체크박스 체크 필수)
```

**Q: "donkeycar를 찾을 수 없습니다"**  
A: 가상환경이 활성화되지 않았거나 설치되지 않음
```powershell
# 가상환경 활성화 확인
.\donkey_env\Scripts\Activate.ps1

# 설치 확인
pip list | grep donkeycar

# 미설치 시 설치
pip install donkeycar
```

**Q: 학습이 매우 느립니다**  
A: 다음 중 선택:
- GPU 설치 및 CUDA 환경 구축 (권장)
- 모델을 "Linear"로 변경
- 필터링으로 학습 데이터 양 감소
- 배치 크기 증가 (`train.py` 참고)

**Q: 메모리 부족 오류**  
A:
- 이미지 해상도 감소 (`IMAGE_W`, `IMAGE_H` 줄이기)
- 배치 크기 감소 (`config.py`에서 `BATCH_SIZE` 줄이기)
- 에포크 수 감소

더 많은 정보는 **[DEPLOYMENT_GUIDE.md](./DEPLOYMENT_GUIDE.md)**를 참조하세요.

---

## 📝 주요 코드 구조

### MainWindow.cs
애플리케이션의 중심 컨트롤러. 5개의 탭 관리 및 상태 전환 담당.

### controls/ 폴더
각 워크플로우 단계별 독립적인 UserControl:
- **DataLoadControl**: 파일 시스템 탐색, 이미지 로드
- **DataFilterControl**: LINQ 필터링, 범위 조정
- **TrainingControl**: Python 백엔드 호출, 프로세스 관리
- **ResultControl**: 학습 결과 분석, 그래프 렌더링

### python/train.py
Donkeycar 공식 파이프라인 통합 또는 독립 Keras 학습 지원.

---

## 🤝 기여 및 개선 사항

### 알려진 제한사항
- Windows 환경에서만 테스트됨 (macOS, Linux는 미지원)
- 대용량 데이터셋(100k+ 프레임)의 경우 성능 저하 가능
- GPU 없는 환경에서 3D CNN 학습은 시간이 오래 걸림

### 개선 로드맵
- [ ] macOS, Linux 네이티브 지원
- [ ] 분산 학습 (다중 GPU)
- [ ] TensorFlow Lite 변환 및 모바일 배포
- [ ] 실시간 모델 평가 (웹캠 테스트)
- [ ] 고급 데이터 증강 (Data Augmentation)

---

## 📄 라이선스

[라이선스 정보를 여기에 추가하세요]

---

## 📧 연락처 및 지원

- **GitHub**: [SimpleDonkeyManager](https://github.com/uswProgrammingTeam13/SimpleDonkeyManager)
- **이슈 리포트**: GitHub Issues를 통해 버그 및 기능 요청
- **문서**: [DEPLOYMENT_GUIDE.md](./DEPLOYMENT_GUIDE.md) 참조

---

## 🎓 참고 자료

- [Donkeycar 공식 문서](https://docs.donkeycar.com/)
- [Keras 학습 가이드](https://keras.io/guides/)
- [Python 가상환경 (venv) 가이드](https://docs.python.org/3/library/venv.html)
- [.NET 10 공식 문서](https://learn.microsoft.com/ko-kr/dotnet/core/whats-new/dotnet-10)

---

## 🎬 스크린샷 (추가 예정)

<!-- 
[스크린샷: 초기 화면 - InitialScreen으로 프로그램 시작 모습]
[스크린샷: 데이터 불러오기 - 폴더 선택 및 이미지 미리보기]
[스크린샷: 데이터 필터링 - 각도/스로틀 범위 조정 및 프리뷰]
[스크린샷: 학습 실행 - 모델 선택 및 학습 진행 상황 로그 표시]
[스크린샷: 결과 확인 - Loss/Accuracy 그래프 시각화]
[스크린샷: 도움말 통합 - 각 탭 좌측의 상세 도움말 텍스트]
-->

---

**마지막 업데이트**: 2025년  
**버전**: 1.0.0  
**상태**: 활발히 개발 중 ✨
