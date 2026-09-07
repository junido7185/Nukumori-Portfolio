// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/Player/PlayerMovement.cs
// Revision: 2ef79da923c95da00bea00d333393db453be5a66
// Historical prototype; not part of the current integrated project.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 10-10
    //값 변수

// Original lines 12-12
    public float sprintSpeed = 10f; //스프린트 속도

// Original lines 14-18
    public float maxStamina = 3f;       // 최대 스태미나
    public float staminaDrain = 1f;     // 초당 감소량
    public float staminaRegen = 0.5f;   // 초당 회복량

    //참조 변수

// Original lines 21-21
    public PortalManager portalManager;

// Original lines 23-23
    //상태 변수

// Original lines 25-29
    private bool isSprinting = false;  // 스프린트 상태
    private float currentSpeed;        // 현재 속도
    private float currentStamina;      // 현재 스태미나

    //벡터

// Original lines 35-39

    void Start()
    {
        currentStamina = maxStamina;
    }

// Original lines 52-71
        
        // 스프린트 조건 체크 (스태미나 0보다 커야 함)
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            isSprinting = true;
            currentStamina -= staminaDrain * Time.deltaTime;

            if (currentStamina < 0)
                currentStamina = 0f;
        }
        else
        {
            isSprinting = false;
            currentStamina += staminaRegen * Time.deltaTime;

            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }

        currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

// Original lines 80-84
            //방향 벡터가 바뀔 때만 방향 저장
            if (input != Vector2.zero)
            {
                lastMoveDir = input;
            }

// Original lines 90-91
                if (!IsBlocked(targetPos))
                    StartCoroutine(MoveTo(targetPos));

// Original lines 104-105
            // 현재 속도(스프린트 또는 일반 속도)를 사용
            transform.position = Vector3.MoveTowards(transform.position, target, currentSpeed * Time.deltaTime);

#endif
