// PORTFOLIO EXCERPT: selected contributions by dev_slime.
// Source: Assets/Scripts/Player/PlayerInteraction.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Review only; omitted team code and dependencies are not included.
// Blocks retain their original scope; this is not a standalone Unity component.
#if false

// Original lines 15-24
    [Header("상자 상호작용 설정")]
    public float interactionRange = 2f;
    public LayerMask chestLayer;


    [Header("UI 표시")]
    public GameObject interactionPrompt; // 일반 상호작용 프롬프트
    public GameObject chestInteractionPrompt; // 상자 상호작용 프롬프트

    private ChestController nearbyChest;

// Original lines 31-35
    void Awake()
    {
        // Awake에서 레이어마스크 초기화
        chestLayer = LayerMask.GetMask("Chest");
    }

// Original lines 108-135
    // 상자 탐지
    private void CheckForNearbyChest()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, chestLayer);
        // Debug.Log($"[CheckForNearbyChest] 감지된 콜라이더 수: {colliders.Length}");
        // foreach (Collider2D col in colliders)
        // {
        //     Debug.Log($"[CheckForNearbyChest] 감지된 오브젝트: {col.name}, 레이어: {LayerMask.LayerToName(col.gameObject.layer)}");
        // }
        
        ChestController closestChest = null;
        float closestDistance = float.MaxValue;
        foreach (Collider2D collider in colliders)
        {
            ChestController chest = collider.GetComponent<ChestController>();
            if (chest != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestChest = chest;
                }
            }
        }
        nearbyChest = closestChest;
        //Debug.Log($"[CheckForNearbyChest] nearbyChest: {(nearbyChest != null ? nearbyChest.name : "null")}");
    }

// Original lines 168-195
    // UI 프롬프트 업데이트
    private void UpdateInteractionPrompts()
    {
        // 상자가 근처에 있으면 상자 프롬프트 표시
        if (nearbyChest != null)
        {
            if (chestInteractionPrompt != null)
                chestInteractionPrompt.SetActive(true);
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
        // 상자가 없고 일반 오브젝트가 있으면 일반 프롬프트 표시
        else if (scanObject != null)
        {
            if (chestInteractionPrompt != null)
                chestInteractionPrompt.SetActive(false);
            if (interactionPrompt != null)
                interactionPrompt.SetActive(true);
        }
        // 둘 다 없으면 모든 프롬프트 숨김
        else
        {
            if (chestInteractionPrompt != null)
                chestInteractionPrompt.SetActive(false);
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
    }

#endif
