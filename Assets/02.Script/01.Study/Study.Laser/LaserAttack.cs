using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public float laserSpeed = 5f;
    public int laserDamage = 2;
    public string requiredBulletTag; // Ư�� źȯ �±� (R, G, B)

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

        // �������� ȭ�� �ۿ� ������ ����
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
        else if (collision.CompareTag(requiredBulletTag)) // �ʿ��� Ư�� źȯ �±׿� ���� �ı�
        {
            // Ư�� Ư�� �Ѿ˿� ������ ������ �����
            Destroy(gameObject);
            Destroy(collision.gameObject); // Ư�� �Ѿ˵� �ı�
        }
    }
}