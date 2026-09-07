// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/System/GameManager.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Supplements the other excerpts for this source file.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 37-39
    [Header("엔딩 씬 전용 설정")]
    public bool forceNewInstance = false;


// Original lines 42-49
        // ✅ 1. 만약 이미 게임매니저가 있는데, 내가 "강제 생성(forceNewInstance)" 옵션을 켰다면?
        if (Instance != null && forceNewInstance)
        {
            Destroy(Instance.gameObject); // 기존의 낡은 매니저를 파괴!
            Instance = null; // 자리를 비움
        }

        // ✅ 2. 기존 싱글톤 로직 (이제 자리가 비었거나, 원래 없었으면 내가 주인이 됨)

// Original lines 55-55
        

// Original lines 57-59
        // 엔딩 씬에서는 굳이 DontDestroy할 필요 없지만, 안전을 위해 둠
        // (어차피 크레딧 끝나면 타이틀로 가니까요)
        if (!forceNewInstance) DontDestroyOnLoad(gameObject); 

// Original lines 64-64
        if (scanObj == null) return;

// Original lines 66-73
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();

        if (objData == null)
        {
            Debug.LogError("[Action] 대상에게 ObjectData 컴포넌트가 없습니다: " + scanObj.name);
            return;
        }

// Original lines 75-77
        Talk(objData.id, objData.isNpc);
        if (talkPanel != null) talkPanel.SetActive(true);
        isAction = true;

// Original lines 82-84
        // 매니저 없으면 찾기
        if (talkManager == null) talkManager = FindFirstObjectByType<TalkManager>();


// Original lines 93-93
            if(talkPanel != null) talkPanel.SetActive(false);

// Original lines 96-97
        
        ProcessEffect(talkData.effects);

// Original lines 127-129
            if (popupImg != null && talkManager != null)
                popupImg.sprite = talkManager.GetPopupImage(talkData.popupIndex);
            if (popupImage != null) popupImage.SetActive(true);

// Original lines 133-133
            if (popupImage != null) popupImage.SetActive(false);

// Original lines 155-155
            if (portraitImg != null)

// Original lines 227-227
            if (popupImage != null) popupImage.SetActive(false);

// Original lines 241-241


#endif
