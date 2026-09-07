// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/System/EventManager.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Supplements the other excerpts for this source file.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 4-4
using Unity.Cinemachine;

// Original lines 13-19
    // 가상 카메라 슬롯
    [Header("카메라 이동용 가상 카메라")]
    public CinemachineCamera vcam;
    // 카메라 이동용 임시 GameObject
    private GameObject tempCameraTarget;



// Original lines 28-31

        // 자동으로 가상 카메라 찾기 (vcam이 할당되지 않은 경우)
        if (vcam == null)
            vcam = FindAnyObjectByType<CinemachineCamera>();

// Original lines 48-49
        if (GameManager.Instance != null)
            GameManager.Instance.SetEventState(true);

// Original lines 66-80
                // 1. [수정] 플레이어(interaction)가 있을 때만 시선 고정 명령 (엔딩에선 무시)
                    if (interaction != null)
                    {
                        interaction.SetOverrideScanObject(data.eventTalkObject); 
                    }

                    // 2. 대화 시작
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.talkIndex = 0;
                        GameManager.Instance.Action(data.eventTalkObject);
                    }

                    // 3. 대화 끝날 때까지 대기
                    while (GameManager.Instance != null && GameManager.Instance.isAction)

// Original lines 82-87

                    // 4. [수정] 플레이어가 있을 때만 시선 고정 해제
                    if (interaction != null)
                    {
                        interaction.ClearOverrideScanObject(); 
                    }

// Original lines 95-100
                    if (GameManager.Instance != null)
                        GameManager.Instance.SetEventState(false);

                    var checker = PlayerCore.Instance != null
                        ? PlayerCore.Instance.GetComponent<PlayerPortalChecker>()
                        : null;

// Original lines 102-102
                        checker.SetPendingSpawn(data.targetPosition, data.facingDirection);

// Original lines 108-108
                            GameManager.Instance?.SetEventState(false);

// Original lines 113-114
                        // FadeManager 없을 때: 명명된 로컬 함수로 구독 해제 보장
                        void OnFallbackLoaded(Scene s, LoadSceneMode m)

// Original lines 116-119
                            SceneManager.sceneLoaded -= OnFallbackLoaded;
                            GameManager.Instance?.SetEventState(false);
                        }
                        SceneManager.sceneLoaded += OnFallbackLoaded;

// Original lines 124-124
                case EventType.MoveCamera:

// Original lines 126-133

                    if (vcam == null)
                    {
                        Debug.LogError("vcam이 할당되지 않았습니다!");
                        break;
                    }

                    // 원래 Follow Target 저장

// Original lines 136-186

                    // 임시 타겟 생성 및 위치 설정
                    if (tempCameraTarget == null)
                    {
                        tempCameraTarget = new GameObject("TempCameraTarget");
                    }

                    // 현재 카메라 위치에서 시작
                    Vector3 currentCamPos = vcam.transform.position;
                    tempCameraTarget.transform.position = new Vector3(currentCamPos.x, currentCamPos.y, 0);

                    // Follow Target을 임시 타겟으로 변경
                    vcam.Follow = tempCameraTarget.transform;

                    // 가상 카메라 이동
                    Vector3 startPos = tempCameraTarget.transform.position;
                    Vector3 targetPos = new Vector3(
                        data.cameraTargetPosition.x,
                        data.cameraTargetPosition.y,
                        0  // Z는 0으로 (Follow Offset의 Z가 -10이므로)
                    );

                    float t = 0f;
                    float dist = Vector3.Distance(startPos, targetPos);

                    while (t < 1f)
                    {
                        t += Time.deltaTime * (data.cameraMoveSpeed / Mathf.Max(dist, 0.001f));
                        tempCameraTarget.transform.position = Vector3.Lerp(startPos, targetPos, t);
                        yield return null;
                    }

                    // 2) 대기
                    yield return new WaitForSeconds(data.cameraWaitTime);

                    // 3) 플레이어 위치로 복귀
                    if (data.returnToPlayer)
                    {
                        startPos = tempCameraTarget.transform.position;
                        Vector3 playerPos = PlayerCore.Instance.transform.position;
                        targetPos = new Vector3(playerPos.x, playerPos.y, 0);

                        t = 0f;
                        dist = Vector3.Distance(startPos, targetPos);

                        while (t < 1f)
                        {
                            t += Time.deltaTime * (data.cameraMoveSpeed / Mathf.Max(dist, 0.001f));
                            tempCameraTarget.transform.position = Vector3.Lerp(startPos, targetPos, t);
                            yield return null;
                        }

// Original lines 188-188
                    }

// Original lines 190-191

                    break;

// Original lines 203-203
            yield return null;

// Original lines 205-215
        if (GameManager.Instance != null)
            GameManager.Instance.SetEventState(false);
    }

    void OnDestroy()
    {
        // 임시 타겟 정리
        if (tempCameraTarget != null)
        {
            Destroy(tempCameraTarget);
        }

#endif
