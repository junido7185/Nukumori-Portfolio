using UnityEngine;

/// <summary>
/// 일기장 조각 4개를 인벤토리에서 자동 감지해 합체 → 히든 엔딩 아이템 지급.
///
/// 씬 설정:
///   1. 빈 GameObject에 이 스크립트 부착 (DontDestroyOnLoad 오브젝트에 붙여도 됨)
///   2. diaryItemIDs = [ 101, 102, 103, 104 ]  (각 일기장 조각 ItemData의 itemID)
///   3. combinedDiaryItemId = 105              (완성 일기 ItemData의 itemID)
///   4. completionFlag = "flag.diary_complete"
/// </summary>
public class DiaryFragmentCombiner : MonoBehaviour
{
    [Header("일기장 조각 아이템 ID (ItemData.itemID와 일치시킬 것)")]
    public int[] diaryItemIDs = { 101, 102, 103, 104 };

    [Header("완성된 일기 아이템 ID")]
    public int combinedDiaryItemId = 105;

    [Header("완성 플래그")]
    public string completionFlag = "flag.diary_complete";

    [Header("완성 효과음 (선택)")]
    public string combineSfxName = "SFX10";

    void Update()
    {
        // 이미 완성됐으면 검사 안 함
        if (FlagManager.Instance != null && FlagManager.Instance.Has(completionFlag))
            return;

        if (InventoryManager.Instance == null) return;

        if (AllFragmentsCollected())
            Combine();
    }

    bool AllFragmentsCollected()
    {
        foreach (int id in diaryItemIDs)
        {
            if (InventoryManager.Instance.GetItemTotalCount(id) <= 0)
                return false;
        }
        return true;
    }

    void Combine()
    {
        // 조각 제거
        foreach (int id in diaryItemIDs)
            InventoryManager.Instance.RemoveItem(id, 1);

        // 완성 일기 추가
        InventoryManager.Instance.AddItem(combinedDiaryItemId, 1);

        // 효과음
        if (SoundManager.Instance != null && !string.IsNullOrEmpty(combineSfxName))
            SoundManager.Instance.PlaySFX(combineSfxName);

        // 플래그 세팅 → 히든 엔딩 조건 해제
        if (FlagManager.Instance != null)
            FlagManager.Instance.Set(completionFlag);

        Debug.Log("[DiaryFragmentCombiner] 일기장 완성! flag.diary_complete 세팅됨");
    }
}
