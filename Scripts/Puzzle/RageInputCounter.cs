using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 분노의 방 퍼즐: E키 또는 클릭을 timeWindow초 안에 requiredPresses번 누르면 클리어.
///
/// 씬 설정:
///   1. 빈 GameObject에 이 스크립트 부착
///   2. completionFlag = "flag.angry_done"
///   3. completionEventId = 분노방 클리어 EventSequence ID
///   4. triggerController = 씬의 TriggerController 연결
///   5. (선택) rageUI / rageText / targetObjects 연결
/// </summary>
public class RageInputCounter : MonoBehaviour
{
    [Header("퍼즐 설정")]
    public int requiredPresses = 20;
    public float timeWindow = 5f;
    public string completionFlag = "flag.angry_done";

    [Header("이벤트 연동")]
    public TriggerController triggerController;
    public int completionEventId = 320; // 분노방 클리어 이벤트 ID

    [Header("파괴 대상 오브젝트 (선택)")]
    public GameObject[] targetObjects; // 연타할수록 하나씩 비활성화할 오브젝트들

    [Header("UI (선택)")]
    public GameObject rageUI;       // 연타 카운트 UI 루트
    public Text rageText;           // "XX / 20" 표시 텍스트
    public Image rageFillBar;       // 채워지는 게이지 이미지 (Image Type: Filled)

    [Header("사운드 (선택)")]
    public string hitSfxName = "SFX2";   // 연타 효과음
    public string clearSfxName = "SFX10"; // 클리어 효과음

    // 내부 상태
    private int pressCount = 0;
    private float windowTimer = 0f;
    private bool isActive = false;
    private bool isCompleted = false;

    void Start()
    {
        // 이미 클리어된 방이면 스킵
        if (FlagManager.Instance != null && FlagManager.Instance.Has(completionFlag))
        {
            isCompleted = true;
            if (rageUI != null) rageUI.SetActive(false);
            return;
        }

        isActive = true;
        ResetCount();
        if (rageUI != null) rageUI.SetActive(true);
    }

    void Update()
    {
        if (!isActive || isCompleted) return;

        // E키 또는 마우스 클릭 입력 감지
        bool pressed = Input.GetKeyDown(KeyCode.E)
                    || Input.GetMouseButtonDown(0)
                    || Input.GetMouseButtonDown(1);

        if (pressed)
            RegisterPress();

        // 타이머 감소
        if (pressCount > 0)
        {
            windowTimer -= Time.deltaTime;
            if (windowTimer <= 0f)
                ResetCount();
        }

        UpdateUI();
    }

    void RegisterPress()
    {
        if (windowTimer <= 0f)
        {
            // 새 연타 시작
            pressCount = 0;
            windowTimer = timeWindow;
        }

        pressCount++;

        // 효과음
        if (SoundManager.Instance != null && !string.IsNullOrEmpty(hitSfxName))
            SoundManager.Instance.PlaySFX(hitSfxName);

        // 오브젝트 파괴 연출: 구간마다 하나씩 비활성화
        UpdateTargetObjects();

        if (pressCount >= requiredPresses)
            Complete();
    }

    void UpdateTargetObjects()
    {
        if (targetObjects == null || targetObjects.Length == 0) return;

        // 연타 진행도에 따라 오브젝트를 순서대로 비활성화
        int destroyCount = Mathf.FloorToInt(
            (float)pressCount / requiredPresses * targetObjects.Length
        );

        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i] != null)
                targetObjects[i].SetActive(i >= destroyCount);
        }
    }

    void Complete()
    {
        isCompleted = true;
        isActive = false;

        if (rageUI != null) rageUI.SetActive(false);

        // 클리어 효과음
        if (SoundManager.Instance != null && !string.IsNullOrEmpty(clearSfxName))
            SoundManager.Instance.PlaySFX(clearSfxName);

        // 플래그 세팅
        if (FlagManager.Instance != null)
            FlagManager.Instance.Set(completionFlag);

        // 클리어 이벤트 실행 (컷씬 + 대사)
        if (triggerController != null)
            triggerController.TriggerByEventId(completionEventId);
        else if (EventManager.Instance != null)
            Debug.LogWarning("[RageInputCounter] triggerController 미연결 — TriggerController를 인스펙터에 연결해주세요.");
    }

    void ResetCount()
    {
        pressCount = 0;
        windowTimer = 0f;
    }

    void UpdateUI()
    {
        if (rageText != null)
            rageText.text = $"{pressCount} / {requiredPresses}";

        if (rageFillBar != null)
            rageFillBar.fillAmount = (float)pressCount / requiredPresses;
    }
}
