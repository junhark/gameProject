using UnityEngine;

public class StaminaManager
{
    // �ִ� ���¹̳�
    private float maxStamina;

    // ���� ���¹̳�
    private float currentStamina;

    // ���¹̳� �Ҹ� �ӵ�
    private float drainRate;

    // ���¹̳� ȸ�� �ӵ�
    private float regenRate;

    // ȸ�� ���� ���� �ð�
    private float regenDelay;

    // ȸ�� ��� Ÿ�̸�
    private float regenTimer;

    // ���� ���¹̳��� ��ȯ�ϴ� ������Ƽ
    public float CurrentStamina => currentStamina;

    // ������: �ִ� ���¹̳�, �Ҹ� �ӵ�, ȸ�� �ӵ�, ȸ�� ��� �ð� �ʱ�ȭ
    public StaminaManager(float maxStamina, float drainRate, float regenRate, float regenDelay)
    {
        this.maxStamina = maxStamina; // �ִ� ���¹̳� ����
        this.currentStamina = maxStamina; // ���� ���¹̳��� �ִ밪���� ����
        this.drainRate = drainRate; // ���¹̳� �Ҹ� �ӵ� ����
        this.regenRate = regenRate; // ���¹̳� ȸ�� �ӵ� ����
        this.regenDelay = regenDelay; // ȸ�� ��� �ð� ����
        this.regenTimer = 0f; // ȸ�� ��� Ÿ�̸� �ʱ�ȭ
    }

    // ���¹̳��� �Ҹ��ϴ� �Լ�
    public void DrainStamina(float deltaTime)
    {
        // �Ҹ�� ���¹̳� ���
        currentStamina -= drainRate * deltaTime;

        // ���¹̳��� 0�� �ִ� ���¹̳� ���̷� ����
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        // ���¹̳� �Ҹ� �� ȸ�� ��� Ÿ�̸Ӹ� �ʱ�ȭ
        regenTimer = 0f;
    }

    // ���¹̳��� ȸ���ϴ� �Լ�
    public void RegenStamina(float deltaTime)
    {
        // ���� ���¹̳��� �ִ밪���� ������ ȸ���� ����
        if (currentStamina < maxStamina)
        {
            // ȸ�� ��� Ÿ�̸� ����
            regenTimer += deltaTime;

            // ȸ�� ��� �ð��� ������ ���¹̳��� ȸ��
            if (regenTimer >= regenDelay)
            {
                // ȸ�� �ӵ��� �°� ���¹̳� ȸ��
                currentStamina += regenRate * deltaTime;

                // ���¹̳��� 0�� �ִ� ���¹̳� ���̷� ����
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }
        }
    }
}