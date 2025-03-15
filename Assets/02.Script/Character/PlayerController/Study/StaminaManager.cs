using UnityEngine;

public class StaminaManager
{
    // 최대 스태미나
    private float maxStamina;

    // 현재 스태미나
    private float currentStamina;

    // 스태미나 소모 속도
    private float drainRate;

    // 스태미나 회복 속도
    private float regenRate;

    // 회복 시작 지연 시간
    private float regenDelay;

    // 회복 대기 타이머
    private float regenTimer;

    // 현재 스태미나를 반환하는 프로퍼티
    public float CurrentStamina => currentStamina;

    // 생성자: 최대 스태미나, 소모 속도, 회복 속도, 회복 대기 시간 초기화
    public StaminaManager(float maxStamina, float drainRate, float regenRate, float regenDelay)
    {
        this.maxStamina = maxStamina; // 최대 스태미나 설정
        this.currentStamina = maxStamina; // 현재 스태미나를 최대값으로 설정
        this.drainRate = drainRate; // 스태미나 소모 속도 설정
        this.regenRate = regenRate; // 스태미나 회복 속도 설정
        this.regenDelay = regenDelay; // 회복 대기 시간 설정
        this.regenTimer = 0f; // 회복 대기 타이머 초기화
    }

    // 스태미나를 소모하는 함수
    public void DrainStamina(float deltaTime)
    {
        // 소모된 스태미나 계산
        currentStamina -= drainRate * deltaTime;

        // 스태미나는 0과 최대 스태미나 사이로 제한
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        // 스태미나 소모 시 회복 대기 타이머를 초기화
        regenTimer = 0f;
    }

    // 스태미나를 회복하는 함수
    public void RegenStamina(float deltaTime)
    {
        // 현재 스태미나가 최대값보다 작으면 회복을 진행
        if (currentStamina < maxStamina)
        {
            // 회복 대기 타이머 갱신
            regenTimer += deltaTime;

            // 회복 대기 시간이 지나면 스태미나를 회복
            if (regenTimer >= regenDelay)
            {
                // 회복 속도에 맞게 스태미나 회복
                currentStamina += regenRate * deltaTime;

                // 스태미나는 0과 최대 스태미나 사이로 제한
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }
        }
    }
}