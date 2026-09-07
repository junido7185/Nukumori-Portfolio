# 코드 읽기 안내

이 저장소는 개인 기여를 검토하기 위한 코드 모음입니다. 전체 게임 빌드에 필요한 파일은 포함하지 않았습니다.

1. `Flow/TitleManager.cs`에서 타이틀 버튼과 첫 씬 진입을 확인합니다.
2. `PlayerInteraction/PlayerInteraction.Excerpt.cs`에서 상자 탐지와 프롬프트 표시를 확인한 뒤 `ChestController.cs`, `ChestUIManager.cs`, `ChestSlotUI.cs`에서 아이템 이동 흐름을 읽습니다.
3. `Ending/FlagManager.SceneCommand.Excerpt.cs`에서 분기 결과의 씬 전환을, `EndingSceneSetup.cs`에서 씬 이동 후 UI 재연결을 확인합니다.
4. `SimpleFadeIn.cs`, `EndingInput.cs`, `SoundManager.cs`에서 연출·입력·오디오 연결을 읽습니다.
5. `BugFixes/`와 [기여 내역](contribution.md)에서 문제 상황과 수정 근거를 확인합니다.

## 원본과 발췌본

일반 `.cs` 파일은 기준 커밋의 원본 바이트를 그대로 복사했습니다. `*.Excerpt.cs`는 공동 작업 파일 중 Git blame에서 개인 기여로 확인한 부분입니다. 원래 메서드 내부의 일부 블록도 포함하므로 `#if false`로 감싸 코드 검토 용도로 제공하며, 실행용 컴포넌트로 사용할 수 없습니다. 추가된 헤더·전처리 지시문은 포트폴리오 편집 요소입니다.

## 공개하지 않은 의존성

| 시스템 | 원본 게임에서 필요한 연결 |
| --- | --- |
| 상자·인벤토리 | 씬의 UI 슬롯·프리팹, ItemData/ChestData 인스턴스, 스프라이트, Chest 레이어 |
| 게임 진입 | Build Settings에 등록된 `Act 1` 씬과 시작 버튼 이벤트 |
| 엔딩 | GameManager, TalkManager, FlagManager, FadeManager 등 팀 공통 시스템, 대사·조건 CSV, 엔딩 UI |
| 사운드 | Inspector에 지정한 AudioSource·AudioClip과 효과 명령의 이름 매칭 |
| 테스트 | 원본 Unity Editor의 엔딩 분기 데이터와 매니저 연결 |

## 검증 범위

포트폴리오 구성 과정에서 원본 파일 해시, 발췌 줄의 작성자, Markdown 내부 링크, 공개 파일 목록을 확인했습니다. 이 코드 모음 자체의 독립 실행·컴파일 테스트는 대상이 아닙니다. 기존 `EndingTester`는 조건을 바꾸는 수동 재현 도구이며 자동 테스트 성공 결과를 의미하지 않습니다.
