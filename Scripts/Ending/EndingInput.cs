using UnityEngine;

public class EndingInput : MonoBehaviour
{
    void Update()
    {
        // 1. 매니저가 없거나 대화 중이 아니면 작동 X
        if (GameManager.Instance == null || !GameManager.Instance.isAction) return;

        // 2. 입력 감지 (E키, 스페이스바, 엔터, 마우스 클릭)
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            GameManager gm = GameManager.Instance;

            // [핵심 수정] 책갈피(talkIndex)를 1칸 뒤로 넘깁니다.
            gm.talkIndex++; 

            // 그 다음, 넘긴 페이지의 내용을 읽으라고 시킵니다.
            gm.Talk(gm.currentTalkId, false);
        }
    }
}