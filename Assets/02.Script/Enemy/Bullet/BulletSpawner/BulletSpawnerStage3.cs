using UnityEngine;
using UnityEngine.UI;

public class BulletSpawnerStage3 : MonoBehaviour
{
    public GameObject bulletPrefab; // 기본 총알 프리팹
    public GameObject fastBulletPrefab; // 빠른 총알 프리팹
    public GameObject bigBulletPrefab; // 큰 총알 프리팹

    public float BPM = 128f; // 노래의 BPM
    private float beatInterval; // 비트 간격 (초 단위)
    private float timeSinceLastBeat = 0f; // 마지막 비트 후 경과 시간

    public int fastBulletBeatInterval = 12; // 빠른 총알 생성 비트 주기
    public int bigBulletBeatInterval = 24; // 큰 총알 생성 비트 주기
    private int beatCount = 0; // 현재 비트 카운트

    private AudioSource audioSource;

    public GameObject stageSuccessPanel; // 스테이지 성공 패널
    public Image[] starImages; // 별 이미지 배열 (스테이지 클리어 패널에 배치된 별)
    public Sprite filledStarSprite; // 불이 들어온 별 이미지 스프라이트

    public PlayerMovement playerMovement; // 플레이어 스크립트 참조

    private bool isStageComplete = false; // 스테이지 성공 상태 확인

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
        if (audioSource.isPlaying && !isStageComplete)
        {
            timeSinceLastBeat += Time.deltaTime;

            // 비트 간격마다 총알 패턴 생성
            if (timeSinceLastBeat >= beatInterval)
            {
                SpawnBullet(bulletPrefab); // 기본 총알 생성
                timeSinceLastBeat -= beatInterval;
                beatCount++;

                // 특정 비트 주기에 따라 빠른 총알 생성
                if (beatCount % fastBulletBeatInterval == 0)
                {
                    SpawnBullet(fastBulletPrefab);
                }

                // 특정 비트 주기에 따라 큰 총알 생성
                if (beatCount % bigBulletBeatInterval == 0)
                {
                    SpawnBullet(bigBulletPrefab);
                }
            }
        }

        // BGM이 끝났을 때 스테이지 성공 처리
        if (!audioSource.isPlaying && !isStageComplete)
        {
            StageSuccess();
        }
    }

    void SpawnBullet(GameObject bulletPrefab)
    {
        Vector3 spawnPosition = GetRandomEdgePosition();
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // 총알을 화면 안에서만 보이도록 설정 (Z축을 0으로)
        bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y, 0);
    }

    Vector3 GetRandomEdgePosition()
    {
        // 화면의 경계 계산
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // 왼쪽
                return new Vector3(bottomLeft.x, Random.Range(bottomLeft.y, topRight.y), 0);
            case 1: // 오른쪽
                return new Vector3(topRight.x, Random.Range(bottomLeft.y, topRight.y), 0);
            case 2: // 아래
                return new Vector3(Random.Range(bottomLeft.x, topRight.x), bottomLeft.y, 0);
            case 3: // 위
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
            SaveStarData(starsEarned, "Stage3_Stars"); // 스테이지 키를 전달
            UpdateStarDisplay(starsEarned);

            stageSuccessPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void SaveStarData(int starsEarned, string stageKey)
    {
        int previousStars = PlayerPrefs.GetInt(stageKey, 0); // 저장된 별 개수 불러오기

        // 기존 별 개수보다 높은 성과만 저장
        if (starsEarned > previousStars)
        {
            PlayerPrefs.SetInt(stageKey, starsEarned);
        }
    }

    int CalculateStars(int health)
    {
        if (health == 5) return 3; // 체력이 5개면 별 3개
        if (health >= 3) return 2; // 체력이 3개 이상이면 별 2개
        return 1; // 체력이 1개 이상이면 별 1개
    }

    void UpdateStarDisplay(int starsEarned)
    {
        for (int i = 0; i < starsEarned; i++)
        {
            starImages[i].sprite = filledStarSprite; // 불이 들어온 별 이미지로 변경
        }
    }
}