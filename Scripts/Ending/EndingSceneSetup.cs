using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingSceneSetup : MonoBehaviour
{
    [Header("지금 씬(엔딩)에 있는 것들을 연결하세요")]
    public TalkManager endingTalkManager; // [추가됨] 대화 매니저
    public GameObject endingTalkPanel;
    public Text endingTalkText;
    public Image endingPortrait; 
    public GameObject endingNamePanel; 
    public Text endingNameText; 

    IEnumerator Start()
    {
        // 1. Act 5 찌꺼기 로딩 대기
        yield return null; 

        // 2. Act 5에서 넘어온 Canvas 찾아서 파괴 (화면 가림 방지)
        var canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        foreach (var c in canvases)
        {
            if (c.transform.root != this.transform.root)
                Destroy(c.gameObject);
        }

        // 3. GameManager에게 새 식구들 소개 (UI + 매니저)
        if (GameManager.Instance != null)
        {
            // [핵심 추가] 두뇌(TalkManager) 연결!
            if (endingTalkManager != null)
            {
                GameManager.Instance.talkManager = endingTalkManager;
            }

            // 입(UI) 연결
            GameManager.Instance.talkPanel = endingTalkPanel;
            GameManager.Instance.talkText = endingTalkText;
            GameManager.Instance.portraitImg = endingPortrait;
            GameManager.Instance.namePanel = endingNamePanel;
            GameManager.Instance.nameText = endingNameText;
            
            GameManager.Instance.talkPanelAnimator = null; // 에러 방지용 끊기
            
            // 먹통 방지용 리셋
            if(endingTalkPanel != null) endingTalkPanel.SetActive(false);
            
            Debug.Log("[EndingSetup] GameManager 완전체 결합 완료! (TalkManager 포함)");
        }
    }
}