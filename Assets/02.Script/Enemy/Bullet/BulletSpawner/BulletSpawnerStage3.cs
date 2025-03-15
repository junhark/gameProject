using UnityEngine;
using UnityEngine.UI;

public class BulletSpawnerStage3 : MonoBehaviour
{
    public GameObject bulletPrefab; // �⺻ �Ѿ� ������
    public GameObject fastBulletPrefab; // ���� �Ѿ� ������
    public GameObject bigBulletPrefab; // ū �Ѿ� ������

    public float BPM = 128f; // �뷡�� BPM
    private float beatInterval; // ��Ʈ ���� (�� ����)
    private float timeSinceLastBeat = 0f; // ������ ��Ʈ �� ��� �ð�

    public int fastBulletBeatInterval = 12; // ���� �Ѿ� ���� ��Ʈ �ֱ�
    public int bigBulletBeatInterval = 24; // ū �Ѿ� ���� ��Ʈ �ֱ�
    private int beatCount = 0; // ���� ��Ʈ ī��Ʈ

    private AudioSource audioSource;

    public GameObject stageSuccessPanel; // �������� ���� �г�
    public Image[] starImages; // �� �̹��� �迭 (�������� Ŭ���� �гο� ��ġ�� ��)
    public Sprite filledStarSprite; // ���� ���� �� �̹��� ��������Ʈ

    public PlayerMovement playerMovement; // �÷��̾� ��ũ��Ʈ ����

    private bool isStageComplete = false; // �������� ���� ���� Ȯ��

    void Start()
    {
        // BPM�� ��Ʈ �������� ��ȯ
        beatInterval = 60f / BPM;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        // ������� ��� ���̰� ���������� �������� ���� ��쿡�� ����
        if (audioSource.isPlaying && !isStageComplete)
        {
            timeSinceLastBeat += Time.deltaTime;

            // ��Ʈ ���ݸ��� �Ѿ� ���� ����
            if (timeSinceLastBeat >= beatInterval)
            {
                SpawnBullet(bulletPrefab); // �⺻ �Ѿ� ����
                timeSinceLastBeat -= beatInterval;
                beatCount++;

                // Ư�� ��Ʈ �ֱ⿡ ���� ���� �Ѿ� ����
                if (beatCount % fastBulletBeatInterval == 0)
                {
                    SpawnBullet(fastBulletPrefab);
                }

                // Ư�� ��Ʈ �ֱ⿡ ���� ū �Ѿ� ����
                if (beatCount % bigBulletBeatInterval == 0)
                {
                    SpawnBullet(bigBulletPrefab);
                }
            }
        }

        // BGM�� ������ �� �������� ���� ó��
        if (!audioSource.isPlaying && !isStageComplete)
        {
            StageSuccess();
        }
    }

    void SpawnBullet(GameObject bulletPrefab)
    {
        Vector3 spawnPosition = GetRandomEdgePosition();
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // �Ѿ��� ȭ�� �ȿ����� ���̵��� ���� (Z���� 0����)
        bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y, 0);
    }

    Vector3 GetRandomEdgePosition()
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

    void StageSuccess()
    {
        if (playerMovement.currentHealth > 0)
        {
            isStageComplete = true;

            int starsEarned = CalculateStars(playerMovement.currentHealth);
            SaveStarData(starsEarned, "Stage3_Stars"); // �������� Ű�� ����
            UpdateStarDisplay(starsEarned);

            stageSuccessPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void SaveStarData(int starsEarned, string stageKey)
    {
        int previousStars = PlayerPrefs.GetInt(stageKey, 0); // ����� �� ���� �ҷ�����

        // ���� �� �������� ���� ������ ����
        if (starsEarned > previousStars)
        {
            PlayerPrefs.SetInt(stageKey, starsEarned);
        }
    }

    int CalculateStars(int health)
    {
        if (health == 5) return 3; // ü���� 5���� �� 3��
        if (health >= 3) return 2; // ü���� 3�� �̻��̸� �� 2��
        return 1; // ü���� 1�� �̻��̸� �� 1��
    }

    void UpdateStarDisplay(int starsEarned)
    {
        for (int i = 0; i < starsEarned; i++)
        {
            starImages[i].sprite = filledStarSprite; // ���� ���� �� �̹����� ����
        }
    }
}