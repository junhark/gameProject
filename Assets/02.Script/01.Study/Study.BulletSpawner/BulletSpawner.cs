using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // 기본 총알 프리팹
    public GameObject fastBulletPrefab; // 빠른 총알 프리팹
    public GameObject bigBulletPrefab; // 큰 총알 프리팹 

    public void SpawnBullet(GameObject bulletPrefab)
    {
        Vector3 spawnPosition = GetRandomEdgePosition();
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // 총알을 화면 안에서만 보이도록 설정 (Z축을 0으로)
        bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y, 0);
    }

    private Vector3 GetRandomEdgePosition()
    {
        // 화면의 경계 계산
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // 왼쪽
                return new Vector3(bottomLeft.x, Random.Range(bottomLeft.y, topRight.y), 0);
            case 1: // 오른쪽
                return new Vector3(topRight.x, Random.Range(bottomLeft.y, topRight.y), 0);
            case 2: // 아래
                return new Vector3(Random.Range(bottomLeft.x, topRight.x), bottomLeft.y, 0);
            case 3: // 위
                return new Vector3(Random.Range(bottomLeft.x, topRight.x), topRight.y, 0);
            default:
                return Vector3.zero;
        }
    }
}