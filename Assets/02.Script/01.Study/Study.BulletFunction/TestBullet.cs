using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public enum BulletType { Normal, Fast, Big } // �Ѿ� ���� ������
    public BulletType bulletType; // �Ѿ� ����
    public float speed = 5f; // �⺻ �Ѿ� �ӵ�
    public Sprite normalBulletSprite; // �Ϲ� �Ѿ� ��������Ʈ
    public Sprite fastBulletSprite; // ���� �Ѿ� ��������Ʈ
    public Sprite bigBulletSprite; // ū �Ѿ� ��������Ʈ
    private Vector2 targetPosition; // �÷��̾��� ��ġ

    void Start()
    {
        // �Ѿ� ������ ���� �ӵ��� ��������Ʈ ����
        switch (bulletType)
        {
            case BulletType.Normal:
                speed = 5f;
                GetComponent<SpriteRenderer>().sprite = normalBulletSprite;
                break;
            case BulletType.Fast:
                speed = 10f;
                GetComponent<SpriteRenderer>().sprite = fastBulletSprite;
                break;
            case BulletType.Big:
                speed = 3f;
                GetComponent<SpriteRenderer>().sprite = bigBulletSprite;
                break;
        }

        // �Ѿ��� �÷��̾ ���� �̵��� ������ ����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            targetPosition = player.transform.position;
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = moveDirection * speed;
        }
    }

    // �Ѿ��� �ٸ� ������Ʈ�� �浹���� �� ȣ��
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾ �Ѿ˿� ������ �������� �Դ´�
            other.GetComponent<HealthManager>().TakeDamage(1); // HealthManager�� TakeDamage �޼��带 ȣ��
            Destroy(gameObject); // �Ѿ��� �ı�
        }
        else if (other.CompareTag("Wall"))
        {
            // ���̳� �ٸ� ��ֹ��� �ε����� �Ѿ��� �ı�
            Destroy(gameObject);
        }
    }
}