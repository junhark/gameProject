using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public float laserSpeed = 5f;
    public int laserDamage = 2;
    public string requiredBulletTag; // 특수 탄환 태그 (R, G, B)

    private Vector2 direction;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;
        }
    }

    void Update()
    {
        transform.Translate(direction * laserSpeed * Time.deltaTime);

        // 레이저가 화면 밖에 나가면 제거
        if (IsOffScreen())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOffScreen()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(laserDamage);
            }

            Destroy(gameObject);
        }
        else if (collision.CompareTag(requiredBulletTag)) // 필요한 특수 탄환 태그에 따라 파괴
        {
            // 특정 특수 총알에 닿으면 레이저 사라짐
            Destroy(gameObject);
            Destroy(collision.gameObject); // 특수 총알도 파괴
        }
    }
}