using UnityEngine;
using UnityEngine.UI;

public class HealthManager
{
    // 최대 체력
    private int maxHealth;

    // 현재 체력
    private int currentHealth;

    // 체력을 나타내는 하트 이미지 배열
    private Image[] hearts;

    // 가득 찬 하트 이미지
    private Sprite fullHeart;

    // 빈 하트 이미지
    private Sprite emptyHeart;

    // 생성자: 초기 체력, 하트 이미지 배열, 가득 찬 하트 및 빈 하트 설정
    public HealthManager(int maxHealth, Image[] hearts, Sprite fullHeart, Sprite emptyHeart)
    {
        this.maxHealth = maxHealth; // 최대 체력 설정
        this.currentHealth = maxHealth; // 현재 체력을 최대 체력으로 설정
        this.hearts = hearts; // 하트 이미지 배열 설정
        this.fullHeart = fullHeart; // 가득 찬 하트 이미지 설정
        this.emptyHeart = emptyHeart; // 빈 하트 이미지 설정

        UpdateHearts(); // 체력에 맞춰 하트 이미지를 업데이트
    }

    // 피해를 입었을 때 호출되는 함수
    public void TakeDamage(int damage)
    {
        // 현재 체력에서 피해를 뺀 값을 계산하여, 0과 최대 체력 사이로 제한
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        // 체력에 맞게 하트 이미지를 업데이트
        UpdateHearts();
    }

    // 체력이 회복되었을 때 호출되는 함수
    public void Heal(int amount)
    {
        // 현재 체력에 회복량을 더한 값을 계산하여, 0과 최대 체력 사이로 제한
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        // 체력에 맞게 하트 이미지를 업데이트
        UpdateHearts();
    }

    // 체력에 맞게 하트 이미지를 업데이트하는 함수
    private void UpdateHearts()
    {
        // 하트 이미지 배열을 순회하면서 각 하트가 가득 찼는지 비었는지 결정
        for (int i = 0; i < hearts.Length; i++)
        {
            // 현재 체력이 해당 하트의 인덱스보다 많으면 가득 찬 하트, 아니면 빈 하트를 설정
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }
}