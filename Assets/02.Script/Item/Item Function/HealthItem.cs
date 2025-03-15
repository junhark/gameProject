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
                // ���� ü���� �ִ� ü�º��� ���� ���� ȸ��
                if (player.currentHealth < player.maxHealth)
                {
                    player.TakeDamage(-healAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}