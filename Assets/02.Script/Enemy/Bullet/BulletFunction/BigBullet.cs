using UnityEngine;

public class BigBullet : MonoBehaviour
{
    public float speed = 5f; // �Ѿ� �ӵ�
    private Vector2 targetPosition; // �÷��̾��� ��ġ

    void Start()
    {
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
            other.GetComponent<PlayerMovement>().TakeDamage(1);
            Destroy(gameObject); // �Ѿ��� �ı�
        }
        else if (other.CompareTag("Wall"))
        {
            // ���̳� �ٸ� ��ֹ��� �ε����� �Ѿ��� �ı�
            Destroy(gameObject);
        }
    }
}