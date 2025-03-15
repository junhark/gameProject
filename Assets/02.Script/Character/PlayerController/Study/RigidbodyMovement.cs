using UnityEngine;

// Rigidbody�� ����ϴ� �̵� Ŭ����
public class RigidbodyMovement : IMovement
{
    private Rigidbody rb;

    // Rigidbody ��ü �ʱ�ȭ
    public RigidbodyMovement(Rigidbody rigidbody)
    {
        rb = rigidbody;
    }

    // �̵� ó��
    public void Move(Vector3 direction, float speed)
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}