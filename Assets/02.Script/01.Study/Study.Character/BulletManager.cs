using UnityEngine;

public class BulletManager
{
    private GameObject[] bulletPrefabs;
    private float[] cooldowns;
    private float[] lastFireTime;
    private float bulletSpeed;

    // 생성자: 총알 프리팹, 쿨다운 시간, 총알 속도를 초기화
    public BulletManager(GameObject[] bulletPrefabs, float[] cooldowns, float bulletSpeed)
    {
        this.bulletPrefabs = bulletPrefabs;
        this.cooldowns = cooldowns;
        this.bulletSpeed = bulletSpeed;
        this.lastFireTime = new float[bulletPrefabs.Length];
    }

    // 총알이 발사 가능한지 여부를 확인하는 함수
    public bool CanFire(int bulletIndex)
    {
        return Time.time >= lastFireTime[bulletIndex] + cooldowns[bulletIndex];
    }

    // 총알을 발사하는 함수
    public void FireBullet(int bulletIndex, Vector3 position, Vector3 direction)
    {
        if (CanFire(bulletIndex))
        {
            GameObject bullet = Object.Instantiate(bulletPrefabs[bulletIndex], position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
            lastFireTime[bulletIndex] = Time.time;
        }
    }
}