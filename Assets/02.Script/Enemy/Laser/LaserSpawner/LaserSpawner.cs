using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public GameObject[] rLaserPrefabs; // R ������ (��/��, ��/��)
    public GameObject[] gLaserPrefabs; // G ������ (��/��, ��/��)
    public GameObject[] bLaserPrefabs; // B ������ (��/��, ��/��)
    public float spawnInterval = 2f; // ������ ���� ����
    public float spawnDistance = 0f; // ȭ�� �ۿ��� ������ �Ÿ� (�����ڸ� �ٱ���)

    private float timeSinceLastSpawn = 0f;
    private int previousLaserType = -1; // ���� ������ Ÿ�� (-1: �ʱⰪ)
    private int previousSide = -1; // ���� ������ ���� (-1: �ʱⰪ)

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnLaser();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnLaser()
    {
        Vector2 spawnPosition = Vector2.zero;
        GameObject laserToSpawn = null;

        int laserType, side;

        // ������ �ٸ� ������ Ÿ�԰� ������ ���� ������ �ݺ�
        do
        {
            laserType = Random.Range(0, 2); // 0: R, 1: G, 2: B
            side = Random.Range(0, 3); // 0: ��, 1: ��, 2: ��, 3: ��
        } while (laserType == previousLaserType && side == previousSide);

        previousLaserType = laserType; // ���� ������ Ÿ�� ������Ʈ
        previousSide = side; // ���� ������ ���� ������Ʈ

        switch (side)
        {
            case 0: // ��
                spawnPosition = new Vector2(Random.Range(0, Screen.width), Screen.height + spawnDistance);
                laserToSpawn = laserType == 0 ? rLaserPrefabs[1] : laserType == 1 ? gLaserPrefabs[1] : bLaserPrefabs[1];
                break;
            case 1: // ��
                spawnPosition = new Vector2(Random.Range(0, Screen.width), -spawnDistance);
                laserToSpawn = laserType == 0 ? rLaserPrefabs[1] : laserType == 1 ? gLaserPrefabs[1] : bLaserPrefabs[1];
                break;
            case 2: // ��
                spawnPosition = new Vector2(-spawnDistance, Random.Range(0, Screen.height));
                laserToSpawn = laserType == 0 ? rLaserPrefabs[0] : laserType == 1 ? gLaserPrefabs[0] : bLaserPrefabs[0];
                break;
            case 3: // ��
                spawnPosition = new Vector2(Screen.width + spawnDistance, Random.Range(0, Screen.height));
                laserToSpawn = laserType == 0 ? rLaserPrefabs[0] : laserType == 1 ? gLaserPrefabs[0] : bLaserPrefabs[0];
                break;
        }

        // ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ�ϰ� ������ ����
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(spawnPosition.x, spawnPosition.y, Camera.main.nearClipPlane));
        Instantiate(laserToSpawn, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
    }
}