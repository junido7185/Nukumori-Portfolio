#if UNITY_EDITOR
using UnityEngine;

public class EndingTester : MonoBehaviour
{
    // 테스트할 대화 시작 ID (Act 5 엔딩 분기점)
    public int startDialogueId = 5000;

    void Start()
    {
        if (FlagManager.Instance == null)
        {
            Debug.Log(">>> [테스트용] FlagManager 임시 생성됨");
            GameObject go = new GameObject("FlagManager_Temp");
            go.AddComponent<FlagManager>();
        }

        if (TalkManager.Instance == null)
        {
            Debug.Log(">>> [테스트용] TalkManager 임시 생성됨 (초상화는 안 나옵니다)");
            GameObject go = new GameObject("TalkManager_Temp");
            go.AddComponent<TalkManager>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log(">>> [F1] 배드 엔딩 루트 설정 (flag.normalsua 삭제)");
            FlagManager.Instance.Clear("flag.normalsua");
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log(">>> [F2] 노말/진 엔딩 루트 설정 (flag.normalsua 지급)");
            FlagManager.Instance.Set("flag.normalsua");
            FlagManager.Instance.Clear("flag.diary1");
            FlagManager.Instance.Clear("flag.diary2");
            FlagManager.Instance.Clear("flag.diary3");
            FlagManager.Instance.Clear("flag.diary4");
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Debug.Log(">>> [F3] 히든 엔딩 루트 설정 (일기장 풀세트)");
            FlagManager.Instance.Set("flag.normalsua");
            FlagManager.Instance.Set("flag.diary1");
            FlagManager.Instance.Set("flag.diary2");
            FlagManager.Instance.Set("flag.diary3");
            FlagManager.Instance.Set("flag.diary4");
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            Debug.Log($">>> [F5] 대화 강제 시작 (ID: {startDialogueId})");
            GameManager.Instance.Talk(startDialogueId, false);
        }
    }
}
#endif
