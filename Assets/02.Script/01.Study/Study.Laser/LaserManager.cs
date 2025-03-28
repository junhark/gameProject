using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public GameObject[] rLaserPrefabs; // R ������ (��/��, ��/��)
    public GameObject[] gLaserPrefabs; // G ������ (��/��, ��/��)
    public GameObject[] bLaserPrefabs; // B ������ (��/��, ��/��)
    public float spawnInterval = 2f; // ������ ���� ����
    public float spawnDistance = 0f; // ȭ�� �ۿ��� ������ �Ÿ�

    private float timeSinceLastSpawn = 0f;
    private int previousLaserType = -1; // ���� ������ Ÿ��
    private int previousSide = -1; // ���� ������ ����

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

        // ������ �ٸ� ������ Ÿ���� ������
        do
        {
            laserType = Random.Range(0, 3); // 0: R, 1: G, 2: B
            side = Random.Range(0, 4); // 0: ��, 1: ��, 2: ��, 3: ��
        } while (laserType == previousLaserType && side == previousSide);

        previousLaserType = laserType; // ���� ������ Ÿ�� ������Ʈ
        previousSide = side; // ���� ������ ���� ������Ʈ

        switch (side)
        {
            case 0: // ��
                spawnPosition = new Vector2(Random.Range(0, Screen.width), Screen.height + spawnDistance);
                laserToSpawn = GetLaserPrefab(laserType, 1);
                break;
            case 1: // ��
                spawnPosition = new Vector2(Random.Range(0, Screen.width), -spawnDistance);
                laserToSpawn = GetLaserPrefab(laserType, 1);
                break;
            case 2: // ��
                spawnPosition = new Vector2(-spawnDistance, Random.Range(0, Screen.height));
                laserToSpawn = GetLaserPrefab(laserType, 0);
                break;
            case 3: // ��
                spawnPosition = new Vector2(Screen.width + spawnDistance, Random.Range(0, Screen.height));
                laserToSpawn = GetLaserPrefab(laserType, 0);
                break;
        }

        // ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ�ϰ� ������ ����
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(spawnPosition.x, spawnPosition.y, Camera.main.nearClipPlane));
        Instantiate(laserToSpawn, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
    }

    GameObject GetLaserPrefab(int laserType, int direction)
    {
        switch (laserType)
        {
            case 0: return rLaserPrefabs[direction];
            case 1: return gLaserPrefabs[direction];
            case 2: return bLaserPrefabs[direction];
            default: return null;
        }
    }
}