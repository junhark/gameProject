using UnityEngine;

public class BulletManager
{
    // 총알 프리팹을 저장할 배열 (각각의 총알은 다른 프리팹을 사용할 수 있음)
    private GameObject[] bulletPrefabs;

    // 각 총알의 쿨타임을 저장할 배열
    private float[] cooldowns;

    // 각 총알에 대해 마지막으로 발사한 시간을 저장할 배열
    private float[] lastFireTime;

    // 총알 프리팹 배열과 쿨타임 배열을 받아 초기화
    public BulletManager(GameObject[] bulletPrefabs, float[] cooldowns)
    {
        this.bulletPrefabs = bulletPrefabs; // 총알 프리팹 배열 초기화
        this.cooldowns = cooldowns; // 쿨타임 배열 초기화
        lastFireTime = new float[bulletPrefabs.Length]; // 각 총알에 대해 마지막 발사 시간 배열 초기화
    }

    // 총알이 발사 가능한지 여부를 확인하는 함수
    public bool CanFire(int bulletIndex)
    {
        // 현재 시간이 마지막 발사 시간 + 쿨타임보다 크거나 같으면 발사 가능
        return Time.time >= lastFireTime[bulletIndex] + cooldowns[bulletIndex];
    }

    // 총알을 발사하는 함수
    public void FireBullet(int bulletIndex, Vector3 position, Vector3 direction, float speed)
    {
        // 발사할 수 있다면
        if (CanFire(bulletIndex))
        {
            // 지정된 총알 프리팹을 사용해 총알 생성
            GameObject bullet = Object.Instantiate(bulletPrefabs[bulletIndex], position, Quaternion.identity);

            // 생성된 총알에 물리적인 속도 부여 (방향과 속도 설정)
            bullet.GetComponent<Rigidbody>().velocity = direction * speed;

            // 총알을 발사한 시간을 기록하여 다음 발사까지의 쿨타임 관리
            lastFireTime[bulletIndex] = Time.time;
        }
    }
}