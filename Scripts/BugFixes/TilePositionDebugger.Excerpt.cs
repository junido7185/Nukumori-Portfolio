// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/Map/TilePositionDebugger.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Supplements the other excerpts for this source file.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 7-14

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }


// Original lines 17-18
        if (targetTilemap == null) return;
        if (cam == null) cam = Camera.main;

// Original lines 21-21
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

// Original lines 23-23
            Debug.Log($"클릭한 타일 좌표: {cellPos}");

#endif
