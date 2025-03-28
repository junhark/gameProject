using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public enum BulletType { Normal, Fast, Big } // 총알 유형 열거형
    public BulletType bulletType; // 총알 유형
    public float speed = 5f; // 기본 총알 속도
    public Sprite normalBulletSprite; // 일반 총알 스프라이트
    public Sprite fastBulletSprite; // 빠른 총알 스프라이트
    public Sprite bigBulletSprite; // 큰 총알 스프라이트
    private Vector2 targetPosition; // 플레이어의 위치

    void Start()
    {
        // 총알 유형에 따라 속도와 스프라이트 설정
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
            other.GetComponent<HealthManager>().TakeDamage(1); // HealthManager의 TakeDamage 메서드를 호출
            Destroy(gameObject); // 총알을 파괴
        }
        else if (other.CompareTag("Wall"))
        {
            // 벽이나 다른 장애물에 부딪히면 총알을 파괴
            Destroy(gameObject);
        }
    }
}