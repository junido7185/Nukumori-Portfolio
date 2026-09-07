using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleFadeIn : MonoBehaviour
{
    [Header("설정")]
    public float duration = 3.0f; // 3초 동안 천천히 밝아짐 (원하는 만큼 숫자 늘리세요)
    
    [Header("연결해주세요")]
    public Image fadeOverlay; // FadeImage 연결
    public GameObject autoStartObject; // Hierarchy의 'AutoStart' 오브젝트 연결

    void Start()
    {
        // 0. 안전장치
        if (fadeOverlay == null) return;

        // 1. 시작하자마자 대화 트리거는 꺼둡니다. (페이드 끝나고 켜려고)
        if (autoStartObject != null) 
        {
            autoStartObject.SetActive(false);
        }

        // 2. 화면을 검은색으로 덮고 시작
        Color c = fadeOverlay.color;
        c.a = 1f; 
        fadeOverlay.color = c;
        fadeOverlay.gameObject.SetActive(true);
        fadeOverlay.raycastTarget = true; // 터치 막기

        // 3. 페이드 연출 시작
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        float timer = 0f;

        while (timer < duration)
        {
            if (fadeOverlay == null) yield break;

            timer += Time.deltaTime;
            // 1(검정) -> 0(투명)으로 천천히 변함
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);
            
            Color c = fadeOverlay.color;
            c.a = alpha;
            fadeOverlay.color = c;

            yield return null;
        }

        // 4. 페이드 끝! 마무리
        if (fadeOverlay != null)
        {
            Color finalColor = fadeOverlay.color;
            finalColor.a = 0f;
            fadeOverlay.color = finalColor;
            fadeOverlay.raycastTarget = false; // 이제 클릭 가능하게 뚫어줌
        }

        // 5. [핵심] 이제 대화를 시작하세요! (바통 터치)
        if (autoStartObject != null)
        {
            Debug.Log("페이드 끝! 대화 시작!");
            autoStartObject.SetActive(true); // 꺼뒀던 트리거를 켜서 대화 발동
        }
    }
}