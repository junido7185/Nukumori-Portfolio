// PORTFOLIO EXCERPT: selected contributions by dev_slime.
// Source: Assets/Scripts/System/FlagManager.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Review only; omitted team code and dependencies are not included.
// Blocks retain their original scope; this is not a standalone Unity component.
#if false

// Original lines 72-93
            // scene:씬이름 처리 로직
            // 주의: 아래의 Split('=') 보다 윗줄에 있어야 작동
            if (kv.StartsWith("scene:"))
            {
                var sceneParts = kv.Split(':'); // 변수명 겹침 방지를 위해 sceneParts로 변경 권장
                if (sceneParts.Length == 2)
                {
                    string targetScene = sceneParts[1].Trim();
                    Debug.Log($"[FlagManager] 씬 전환 명령 감지: {targetScene}");

                    // FadeManager가 있으면 페이드 효과와 함께 이동
                    if (FadeManager.Instance != null)
                    {
                        FadeManager.Instance.ChangeScene(targetScene);
                    }
                    else
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
                    }
                }
                continue; // 씬 이동 명령 처리했으니 다음 명령으로 넘어감
            }

#endif
