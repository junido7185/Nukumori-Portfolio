// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/Player/PlayerInteraction.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Supplements the other excerpts for this source file.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 25-25


// Original lines 36-36


// Original lines 51-51


// Original lines 77-77


// Original lines 80-81

        // TriggerController로 수정

// Original lines 93-93
        

// Original lines 95-95


// Original lines 98-98
        if (gameManager != null && (gameManager.isAction || gameManager.isEventPlaying))

// Original lines 103-106
        CheckForNearbyChest();
        ScanForObjects();
        UpdateInteractionPrompts();
    }

// Original lines 136-141


    // 일반 오브젝트 탐지 (기존 시스템)
    private void ScanForObjects()
    {
        // 이동 방향 업데이트

// Original lines 143-147
        if (dir != Vector2.zero)
            lastMoveDir = dir;

        // Ray로 오브젝트 탐지
        Debug.DrawRay(transform.position, lastMoveDir.normalized * 0.9f, Color.green);

// Original lines 157-157


// Original lines 162-162


// Original lines 166-167
    }


// Original lines 196-204

    // 기즈모로 상호작용 범위 표시
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
        // Ray도 시각화
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, lastMoveDir.normalized * 0.9f);

#endif
