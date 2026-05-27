# SimpleDonkeyManager - 다른 데스크톱 배포 가이드

## 📋 요구사항

다른 데스크톱에서 SimpleDonkeyManager를 실행하려면 다음만 필요합니다:

### 필수 사항
1. **.NET 10 Runtime** (또는 .NET 10 SDK)
   - 다운로드: https://dotnet.microsoft.com/download/dotnet/10.0

2. **Python 3.11+ (Donkeycar 설치됨)**
   - 다운로드: https://www.python.org/downloads/
   - Donkeycar 설치 방법 아래 참조

### 선택 사항 (성능 향상)
3. **로컬 가상환경** (donkey_env)
   - 없어도 작동하지만, 있으면 로컬 가상환경의 Donkeycar 사용

---

## 🚀 빠른 시작 (권장)

### 1단계: .NET 10 설치
```bash
# Windows에서 .NET 10 Runtime 설치
# https://dotnet.microsoft.com/download/dotnet/10.0
```

### 2단계: Python + Donkeycar 설치
```bash
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
