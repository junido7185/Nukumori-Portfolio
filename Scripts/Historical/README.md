# 초기 구현과 별도 브랜치 실험

이 폴더는 현재 `Jun` 통합본에 없는 과거 기여를 보존합니다. 현재 게임의 실행 코드나 완성 기능으로 표시하지 않습니다.

| 자료 | 기준 커밋 | 내용 |
| --- | --- | --- |
| [PlayerMovement.SprintStamina.Excerpt.cs](PlayerMovement.SprintStamina.Excerpt.cs) | `2ef79da` (2025.07.16) | `3308272`의 Shift 달리기와 후속 스태미나 감소·회복 기여 구간. 원본 로컬 `main` 브랜치에서 확인 |
| [CutsceneManager.Prototype.Excerpt.cs](CutsceneManager.Prototype.Excerpt.cs) | `801005d` (2025.08.15) | `origin/nukumori_newtest` 브랜치의 카메라 우선순위 전환 실험 |

Git blame으로 본인 기여가 확인되는 줄만 원본 순서대로 발췌하고 `#if false`로 감쌌습니다. 원본 줄 번호와 커밋은 각 파일 머리말과 [소스 목록](../../Docs/source-manifest.json)에 기록했습니다.

컷신 프로토타입에는 `Lens.With { ... }`처럼 그대로는 컴파일할 수 없는 표현이 남아 있습니다. `blendCurve`가 실제 보간에 사용되지 않고, `BlendTo`는 카메라 우선순위 변경 뒤 대기하는 형태입니다. 이를 완성된 카메라 블렌딩 기능으로 설명하지 않습니다. 자료의 목적은 과거 실험의 기여 확인이며 원본 코드를 임의로 완성 코드처럼 고치지 않았습니다.

현재 통합본의 카메라 진행 구현은 [EventManager 발췌](../Flow/EventManager.Excerpt.cs)를 참고하세요.
