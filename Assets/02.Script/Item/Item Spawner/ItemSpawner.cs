using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject healthItemPrefab;
    public float spawnInterval = 5f;

    //ȭ�� ������
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

        // spawnInterval �ð����� ������ ����
        if (timer >= spawnInterval)
        {
            SpawnHealthItem();
            timer = 0f;
        }
    }

    void SpawnHealthItem()
    {
        //������ġ ����
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(healthItemPrefab, spawnPosition, Quaternion.identity);
    }
}