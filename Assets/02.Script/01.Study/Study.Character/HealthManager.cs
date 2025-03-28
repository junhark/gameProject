using UnityEngine;
using UnityEngine.UI;

public class HealthManager
{
    private float maxHealth;
    private float currentHealth;
    private Image[] hearts;
    private Sprite fullHeart;
    private Sprite emptyHeart;

    // ������: �ִ� ü�°� ��Ʈ UI, ��Ʈ ��������Ʈ�� �ʱ�ȭ
    public HealthManager(float maxHealth, Image[] hearts, Sprite fullHeart, Sprite emptyHeart)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.hearts = hearts;
        this.fullHeart = fullHeart;
        this.emptyHeart = emptyHeart;
    }

    // ���ظ� �Ծ��� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHearts();
    }

    // ü���� ȸ���Ǿ��� �� ȣ��Ǵ� �Լ�
    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHearts();
    }

    // ü�¿� �°� ��Ʈ�� ������Ʈ�ϴ� �Լ�
    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }
}