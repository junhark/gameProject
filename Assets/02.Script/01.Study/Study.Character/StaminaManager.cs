using UnityEngine;
using UnityEngine.UI;

public class StaminaManager
{
    private float maxStamina;
    private float currentStamina;
    private float drainRate;
    private float regenRate;
    private float regenDelay;
    private float regenTimer;
    private Slider staminaBar;

    // 생성자: 최대 스태미나, 소모 속도, 회복 속도, 회복 대기 시간, 스태미나 바를 초기화
    public StaminaManager(float maxStamina, float drainRate, float regenRate, float regenDelay, Slider staminaBar)
    {
        this.maxStamina = maxStamina;
        this.currentStamina = maxStamina;
        this.drainRate = drainRate;
        this.regenRate = regenRate;
        this.regenDelay = regenDelay;
        this.staminaBar = staminaBar;
        this.regenTimer = 0f;
    }

    // 스태미나를 소모하는 함수
    public void DrainStamina(float deltaTime)
    {
        currentStamina -= drainRate * deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        regenTimer = 0f;
        UpdateStaminaBar();
    }

    // 스태미나를 회복하는 함수
    public void RegenStamina(float deltaTime)
    {
        if (currentStamina < maxStamina)
        {
            regenTimer += deltaTime;
            if (regenTimer >= regenDelay)
            {
                currentStamina += regenRate * deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }
        }
        UpdateStaminaBar();
    }

    // 스태미나 바를 업데이트하는 함수
    private void UpdateStaminaBar()
    {
        staminaBar.value = currentStamina;
    }
}