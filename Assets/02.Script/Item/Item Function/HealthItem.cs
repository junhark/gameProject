using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                // 현재 체력이 최대 체력보다 적을 때만 회복
                if (player.currentHealth < player.maxHealth)
                {
                    player.TakeDamage(-healAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}