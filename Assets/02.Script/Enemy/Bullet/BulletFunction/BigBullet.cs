using UnityEngine;

public class BigBullet : MonoBehaviour
{
    public float speed = 5f; // 총알 속도
    private Vector2 targetPosition; // 플레이어의 위치

    void Start()
    {
        // 총알이 플레이어를 향해 이동할 방향을 설정
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            targetPosition = player.transform.position;
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = moveDirection * speed;
        }
    }

    // 총알이 다른 오브젝트와 충돌했을 때 호출
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 총알에 맞으면 데미지를 입는다
            other.GetComponent<PlayerMovement>().TakeDamage(1);
            Destroy(gameObject); // 총알을 파괴
        }
        else if (other.CompareTag("Wall"))
        {
            // 벽이나 다른 장애물에 부딪히면 총알을 파괴
            Destroy(gameObject);
        }
    }
}