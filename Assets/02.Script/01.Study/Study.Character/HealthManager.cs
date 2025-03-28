using UnityEngine;
using UnityEngine.UI;

public class HealthManager
{
    private float maxHealth;
    private float currentHealth;
    private Image[] hearts;
    private Sprite fullHeart;
    private Sprite emptyHeart;

    // 생성자: 최대 체력과 하트 UI, 하트 스프라이트를 초기화
    public HealthManager(float maxHealth, Image[] hearts, Sprite fullHeart, Sprite emptyHeart)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.hearts = hearts;
        this.fullHeart = fullHeart;
        this.emptyHeart = emptyHeart;
    }

    // 피해를 입었을 때 호출되는 함수
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHearts();
    }

    // 체력이 회복되었을 때 호출되는 함수
    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHearts();
    }

    // 체력에 맞게 하트를 업데이트하는 함수
    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }
}