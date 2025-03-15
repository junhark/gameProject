using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public GameObject[] rLaserPrefabs; // R 레이저 (좌/우, 상/하)
    public GameObject[] gLaserPrefabs; // G 레이저 (좌/우, 상/하)
    public GameObject[] bLaserPrefabs; // B 레이저 (좌/우, 상/하)
    public float spawnInterval = 2f; // 레이저 생성 간격
    public float spawnDistance = 0f; // 화면 밖에서 생성할 거리 (가장자리 바깥쪽)

    private float timeSinceLastSpawn = 0f;
    private int previousLaserType = -1; // 이전 레이저 타입 (-1: 초기값)
    private int previousSide = -1; // 이전 레이저 방향 (-1: 초기값)

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

        // 이전과 다른 레이저 타입과 방향이 나올 때까지 반복
        do
        {
            laserType = Random.Range(0, 2); // 0: R, 1: G, 2: B
            side = Random.Range(0, 3); // 0: 상, 1: 하, 2: 좌, 3: 우
        } while (laserType == previousLaserType && side == previousSide);

        previousLaserType = laserType; // 이전 레이저 타입 업데이트
        previousSide = side; // 이전 레이저 방향 업데이트

        switch (side)
        {
            case 0: // 상
                spawnPosition = new Vector2(Random.Range(0, Screen.width), Screen.height + spawnDistance);
                laserToSpawn = laserType == 0 ? rLaserPrefabs[1] : laserType == 1 ? gLaserPrefabs[1] : bLaserPrefabs[1];
                break;
            case 1: // 하
                spawnPosition = new Vector2(Random.Range(0, Screen.width), -spawnDistance);
                laserToSpawn = laserType == 0 ? rLaserPrefabs[1] : laserType == 1 ? gLaserPrefabs[1] : bLaserPrefabs[1];
                break;
            case 2: // 좌
                spawnPosition = new Vector2(-spawnDistance, Random.Range(0, Screen.height));
                laserToSpawn = laserType == 0 ? rLaserPrefabs[0] : laserType == 1 ? gLaserPrefabs[0] : bLaserPrefabs[0];
                break;
            case 3: // 우
                spawnPosition = new Vector2(Screen.width + spawnDistance, Random.Range(0, Screen.height));
                laserToSpawn = laserType == 0 ? rLaserPrefabs[0] : laserType == 1 ? gLaserPrefabs[0] : bLaserPrefabs[0];
                break;
        }

        // 화면 좌표를 월드 좌표로 변환하고 레이저 생성
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(spawnPosition.x, spawnPosition.y, Camera.main.nearClipPlane));
        Instantiate(laserToSpawn, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
    }
}