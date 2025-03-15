using UnityEngine;

// Rigidbody를 사용하는 이동 클래스
public class RigidbodyMovement : IMovement
{
    private Rigidbody rb;

    // Rigidbody 객체 초기화
    public RigidbodyMovement(Rigidbody rigidbody)
    {
        rb = rigidbody;
    }

    // 이동 처리
    public void Move(Vector3 direction, float speed)
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}