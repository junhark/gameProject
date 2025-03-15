using UnityEngine;
using UnityEngine.UI;

public class HealthManager
{
    // �ִ� ü��
    private int maxHealth;

    // ���� ü��
    private int currentHealth;

    // ü���� ��Ÿ���� ��Ʈ �̹��� �迭
    private Image[] hearts;

    // ���� �� ��Ʈ �̹���
    private Sprite fullHeart;

    // �� ��Ʈ �̹���
    private Sprite emptyHeart;

    // ������: �ʱ� ü��, ��Ʈ �̹��� �迭, ���� �� ��Ʈ �� �� ��Ʈ ����
    public HealthManager(int maxHealth, Image[] hearts, Sprite fullHeart, Sprite emptyHeart)
    {
        this.maxHealth = maxHealth; // �ִ� ü�� ����
        this.currentHealth = maxHealth; // ���� ü���� �ִ� ü������ ����
        this.hearts = hearts; // ��Ʈ �̹��� �迭 ����
        this.fullHeart = fullHeart; // ���� �� ��Ʈ �̹��� ����
        this.emptyHeart = emptyHeart; // �� ��Ʈ �̹��� ����

        UpdateHearts(); // ü�¿� ���� ��Ʈ �̹����� ������Ʈ
    }

    // ���ظ� �Ծ��� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(int damage)
    {
        // ���� ü�¿��� ���ظ� �� ���� ����Ͽ�, 0�� �ִ� ü�� ���̷� ����
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        // ü�¿� �°� ��Ʈ �̹����� ������Ʈ
        UpdateHearts();
    }

    // ü���� ȸ���Ǿ��� �� ȣ��Ǵ� �Լ�
    public void Heal(int amount)
    {
        // ���� ü�¿� ȸ������ ���� ���� ����Ͽ�, 0�� �ִ� ü�� ���̷� ����
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        // ü�¿� �°� ��Ʈ �̹����� ������Ʈ
        UpdateHearts();
    }

    // ü�¿� �°� ��Ʈ �̹����� ������Ʈ�ϴ� �Լ�
    private void UpdateHearts()
    {
        // ��Ʈ �̹��� �迭�� ��ȸ�ϸ鼭 �� ��Ʈ�� ���� á���� ������� ����
        for (int i = 0; i < hearts.Length; i++)
        {
            // ���� ü���� �ش� ��Ʈ�� �ε������� ������ ���� �� ��Ʈ, �ƴϸ� �� ��Ʈ�� ����
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }
}