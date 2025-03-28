using UnityEngine;

public class BulletSpawnerStage1 : MonoBehaviour
{
    public BulletSpawner bulletSpawner;
    public StageManager stageManager;

    public float BPM = 128f; // 노래의 BPM
    private float beatInterval; // 비트 간격 (초 단위)
    private float timeSinceLastBeat = 0f; // 마지막 비트 후 경과 시간

    public int fastBulletBeatInterval = 12; // 빠른 총알 생성 비트 주기
    public int bigBulletBeatInterval = 24; // 큰 총알 생성 비트 주기
    private int beatCount = 0; // 현재 비트 카운트

    private AudioSource audioSource;

    void Start()
    {
        // BPM을 비트 간격으로 변환
        beatInterval = 60f / BPM;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        // 오디오가 재생 중이고 스테이지가 성공하지 않은 경우에만 동작
        if (audioSource.isPlaying && !stageManager.isStageComplete)
        {
            timeSinceLastBeat += Time.deltaTime;

            // 비트 간격마다 총알 패턴 생성
            if (timeSinceLastBeat >= beatInterval)
            {
                bulletSpawner.SpawnBullet(bulletSpawner.bulletPrefab); // 기본 총알 생성
                timeSinceLastBeat -= beatInterval;
                beatCount++;

                // 특정 비트 주기에 따라 빠른 총알 생성
                if (beatCount % fastBulletBeatInterval == 0)
                {
                    bulletSpawner.SpawnBullet(bulletSpawner.fastBulletPrefab);
                }

                // 특정 비트 주기에 따라 큰 총알 생성
                if (beatCount % bigBulletBeatInterval == 0)
                {
                    bulletSpawner.SpawnBullet(bulletSpawner.bigBulletPrefab);
                }
            }
        }

        // BGM이 끝났을 때 스테이지 성공 처리
        stageManager.CheckStageSuccess(audioSource);
    }
}