// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/Player/PlayerMovement.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Supplements the other excerpts for this source file.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 44-44


// Original lines 46-46
        if (GameManager.Instance != null && GameManager.Instance.isEventPlaying)

// Original lines 62-68
        // GameManager의 isAction이 true면 이동 불가
        if (GameManager.Instance != null && GameManager.Instance.isAction)
        {
            // 이동 입력 무시
            return;
        }


// Original lines 100-100
        if (portalChecker != null) portalChecker.PortalChecker();

// Original lines 102-102


// Original lines 107-107


// Original lines 115-115
            LayerMask.GetMask("Wall", "Obstacle", "Object", "Chest")

// Original lines 123-124
        if (GameManager.Instance != null && !GameManager.Instance.isEventPlaying || isMoving) return;
        

// Original lines 158-158


// Original lines 170-174
        if (animator != null)
        {
            animator.SetFloat("MoveX", dir.x);
            animator.SetFloat("MoveY", dir.y);
        }

// Original lines 195-195
}

#endif
