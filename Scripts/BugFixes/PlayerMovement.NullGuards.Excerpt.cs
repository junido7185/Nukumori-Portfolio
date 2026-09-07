// PORTFOLIO EXCERPT: selected contributions by dev_slime.
// Source: Assets/Scripts/Player/PlayerMovement.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Review only; omitted team code and dependencies are not included.
// Blocks retain their original scope; this is not a standalone Unity component.
#if false

// Original lines 42-43
        // [추가] 타일맵이 없으면(엔딩 씬 등) 아예 업데이트 로직 중지 (안전장치)
        if (tileMap == null) return;

// Original lines 105-106
        // [핵심 수정] 타일맵이 없으면 '막힌 것'으로 간주하여 이동 방지 & 에러 차단
        if (tileMap == null) return true;

// Original lines 125-126
        // [추가] 타일맵 없으면 이벤트 이동도 중지
        if (tileMap == null) return;

// Original lines 156-157
        // [핵심 수정] 타일맵이 없으면 트리거 검사도 하지 않음
        if (tileMap == null) return;

#endif
