// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/System/EventData.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Supplements the other excerpts for this source file.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 4-10
public enum EventType
{
    Move,
    Wait,
    Talk,
    FacingDirection,
    SceneTransition,

// Original lines 13-13
}

// Original lines 34-34
    public Vector2Int facingDirection;

// Original lines 39-39


// Original lines 41-45
    [Header("카메라 이동 설정")]
    public Vector3 cameraTargetPosition;  // 목표 월드 좌표
    public float cameraMoveSpeed = 5f;    // 이동 속도
    public float cameraWaitTime = 1f;     // 목표 지점 대기 시간
    public bool returnToPlayer = true;    // 플레이어 복귀 여부

#endif
