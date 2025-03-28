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

    // ������: �ִ� ���¹̳�, �Ҹ� �ӵ�, ȸ�� �ӵ�, ȸ�� ��� �ð�, ���¹̳� �ٸ� �ʱ�ȭ
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

    // ���¹̳��� �Ҹ��ϴ� �Լ�
    public void DrainStamina(float deltaTime)
    {
        currentStamina -= drainRate * deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        regenTimer = 0f;
        UpdateStaminaBar();
    }

    // ���¹̳��� ȸ���ϴ� �Լ�
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

    // ���¹̳� �ٸ� ������Ʈ�ϴ� �Լ�
    private void UpdateStaminaBar()
    {
        staminaBar.value = currentStamina;
    }
}