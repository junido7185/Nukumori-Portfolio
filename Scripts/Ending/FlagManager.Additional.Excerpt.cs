// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/System/FlagManager.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Supplements the other excerpts for this source file.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 46-47
        if (string.IsNullOrWhiteSpace(effects)) return;
        if (!gameObject.activeInHierarchy)

// Original lines 49-50
            Debug.LogWarning("[FlagManager] 씬 언로드 중이라 효과 적용 스킵");
            return;

// Original lines 52-54

        var pairs = effects.Split(';');
        foreach (var p in pairs)

// Original lines 56-71
            var kv = p.Trim();
            if (string.IsNullOrEmpty(kv)) continue;

            // 1. 타이머 ON
            if (kv.Equals("timer.on", System.StringComparison.OrdinalIgnoreCase))
            {
                if (Timer.Instance != null) Timer.Instance.TimerOn();
                continue;
            }
            // 2. 타이머 OFF
            if (kv.Equals("timer.off", System.StringComparison.OrdinalIgnoreCase))
            {
                if (Timer.Instance != null) Timer.Instance.TimerOff();
                continue;
            }


// Original lines 95-97
            // 3. 기존 set / clear 처리 (등호 = 가 들어가는 명령어들)
            var parts = kv.Split('=');
            if (parts.Length != 2) continue;

// Original lines 99-100
            string cmd = parts[0].Trim().ToLower();
            string[] keys = parts[1].Split(',');

// Original lines 102-109
            foreach (var raw in keys)
            {
                string key = raw.Trim();
                if (string.IsNullOrEmpty(key)) continue;

                if (cmd == "set") Set(key);
                else if (cmd == "clear") Clear(key);
            }

// Original lines 112-113
        OnFlagsApplied?.Invoke();
    }

#endif
