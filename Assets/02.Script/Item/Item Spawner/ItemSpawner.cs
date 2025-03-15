using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject healthItemPrefab;
    public float spawnInterval = 5f;

    //화면 사이즈
    private Vector2 spawnAreaMin = new Vector2(45, 75); // x: 45, y: 75
    private Vector2 spawnAreaMax = new Vector2(1217, 570); // x: 1217, y: 570

    private float timer;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // spawnInterval 시간마다 아이템 생성
        if (timer >= spawnInterval)
        {
            SpawnHealthItem();
            timer = 0f;
        }
    }

    void SpawnHealthItem()
    {
        //랜덤위치 생성
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(healthItemPrefab, spawnPosition, Quaternion.identity);
    }
}