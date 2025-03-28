using UnityEngine;

public class BulletSpawnerStage1 : MonoBehaviour
{
    public BulletSpawner bulletSpawner;
    public StageManager stageManager;

    public float BPM = 128f; // �뷡�� BPM
    private float beatInterval; // ��Ʈ ���� (�� ����)
    private float timeSinceLastBeat = 0f; // ������ ��Ʈ �� ��� �ð�

    public int fastBulletBeatInterval = 12; // ���� �Ѿ� ���� ��Ʈ �ֱ�
    public int bigBulletBeatInterval = 24; // ū �Ѿ� ���� ��Ʈ �ֱ�
    private int beatCount = 0; // ���� ��Ʈ ī��Ʈ

    private AudioSource audioSource;

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
        if (audioSource.isPlaying && !stageManager.isStageComplete)
        {
            timeSinceLastBeat += Time.deltaTime;

            // ��Ʈ ���ݸ��� �Ѿ� ���� ����
            if (timeSinceLastBeat >= beatInterval)
            {
                bulletSpawner.SpawnBullet(bulletSpawner.bulletPrefab); // �⺻ �Ѿ� ����
                timeSinceLastBeat -= beatInterval;
                beatCount++;

                // Ư�� ��Ʈ �ֱ⿡ ���� ���� �Ѿ� ����
                if (beatCount % fastBulletBeatInterval == 0)
                {
                    bulletSpawner.SpawnBullet(bulletSpawner.fastBulletPrefab);
                }

                // Ư�� ��Ʈ �ֱ⿡ ���� ū �Ѿ� ����
                if (beatCount % bigBulletBeatInterval == 0)
                {
                    bulletSpawner.SpawnBullet(bulletSpawner.bigBulletPrefab);
                }
            }
        }

        // BGM�� ������ �� �������� ���� ó��
        stageManager.CheckStageSuccess(audioSource);
    }
}