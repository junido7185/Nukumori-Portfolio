// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/UI/ChoicePanelUI.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Supplements the other excerpts for this source file.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 26-47
    // ▼ [추가됨] Act 5 같은 상황에서 매니저 연결 없이도 스스로 키 입력을 받도록 함
    void Update()
    {
        if (!IsOpen) return;

        // 위쪽 이동 (W 또는 윗화살표)
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelection(-1);
        }
        // 아래쪽 이동 (S 또는 아래화살표)
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelection(1);
        }
        // 선택 확정 (E, 엔터, 스페이스)
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            Confirm();
        }
    }


// Original lines 116-116
}

#endif
