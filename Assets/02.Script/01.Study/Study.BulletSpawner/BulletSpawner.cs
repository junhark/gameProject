using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // �⺻ �Ѿ� ������
    public GameObject fastBulletPrefab; // ���� �Ѿ� ������
    public GameObject bigBulletPrefab; // ū �Ѿ� ������ 

    public void SpawnBullet(GameObject bulletPrefab)
    {
        Vector3 spawnPosition = GetRandomEdgePosition();
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // �Ѿ��� ȭ�� �ȿ����� ���̵��� ���� (Z���� 0����)
        bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y, 0);
    }

    private Vector3 GetRandomEdgePosition()
    {
        // ȭ���� ��� ���
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // ����
                return new Vector3(bottomLeft.x, Random.Range(bottomLeft.y, topRight.y), 0);
            case 1: // ������
                return new Vector3(topRight.x, Random.Range(bottomLeft.y, topRight.y), 0);
            case 2: // �Ʒ�
                return new Vector3(Random.Range(bottomLeft.x, topRight.x), bottomLeft.y, 0);
            case 3: // ��
                return new Vector3(Random.Range(bottomLeft.x, topRight.x), topRight.y, 0);
            default:
                return Vector3.zero;
        }
    }
}