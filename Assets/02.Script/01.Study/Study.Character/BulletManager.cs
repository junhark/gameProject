using UnityEngine;

public class BulletManager
{
    private GameObject[] bulletPrefabs;
    private float[] cooldowns;
    private float[] lastFireTime;
    private float bulletSpeed;

    // ������: �Ѿ� ������, ��ٿ� �ð�, �Ѿ� �ӵ��� �ʱ�ȭ
    public BulletManager(GameObject[] bulletPrefabs, float[] cooldowns, float bulletSpeed)
    {
        this.bulletPrefabs = bulletPrefabs;
        this.cooldowns = cooldowns;
        this.bulletSpeed = bulletSpeed;
        this.lastFireTime = new float[bulletPrefabs.Length];
    }

    // �Ѿ��� �߻� �������� ���θ� Ȯ���ϴ� �Լ�
    public bool CanFire(int bulletIndex)
    {
        return Time.time >= lastFireTime[bulletIndex] + cooldowns[bulletIndex];
    }

    // �Ѿ��� �߻��ϴ� �Լ�
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