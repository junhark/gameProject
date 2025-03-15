using UnityEngine;

public class BulletManager
{
    // �Ѿ� �������� ������ �迭 (������ �Ѿ��� �ٸ� �������� ����� �� ����)
    private GameObject[] bulletPrefabs;

    // �� �Ѿ��� ��Ÿ���� ������ �迭
    private float[] cooldowns;

    // �� �Ѿ˿� ���� ���������� �߻��� �ð��� ������ �迭
    private float[] lastFireTime;

    // �Ѿ� ������ �迭�� ��Ÿ�� �迭�� �޾� �ʱ�ȭ
    public BulletManager(GameObject[] bulletPrefabs, float[] cooldowns)
    {
        this.bulletPrefabs = bulletPrefabs; // �Ѿ� ������ �迭 �ʱ�ȭ
        this.cooldowns = cooldowns; // ��Ÿ�� �迭 �ʱ�ȭ
        lastFireTime = new float[bulletPrefabs.Length]; // �� �Ѿ˿� ���� ������ �߻� �ð� �迭 �ʱ�ȭ
    }

    // �Ѿ��� �߻� �������� ���θ� Ȯ���ϴ� �Լ�
    public bool CanFire(int bulletIndex)
    {
        // ���� �ð��� ������ �߻� �ð� + ��Ÿ�Ӻ��� ũ�ų� ������ �߻� ����
        return Time.time >= lastFireTime[bulletIndex] + cooldowns[bulletIndex];
    }

    // �Ѿ��� �߻��ϴ� �Լ�
    public void FireBullet(int bulletIndex, Vector3 position, Vector3 direction, float speed)
    {
        // �߻��� �� �ִٸ�
        if (CanFire(bulletIndex))
        {
            // ������ �Ѿ� �������� ����� �Ѿ� ����
            GameObject bullet = Object.Instantiate(bulletPrefabs[bulletIndex], position, Quaternion.identity);

            // ������ �Ѿ˿� �������� �ӵ� �ο� (����� �ӵ� ����)
            bullet.GetComponent<Rigidbody>().velocity = direction * speed;

            // �Ѿ��� �߻��� �ð��� ����Ͽ� ���� �߻������ ��Ÿ�� ����
            lastFireTime[bulletIndex] = Time.time;
        }
    }
}