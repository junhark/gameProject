using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject stageSuccessPanel; // 스테이지 성공 패널
    public Image[] starImages; // 별 이미지 배열 (스테이지 클리어 패널에 배치된 별)
    public Sprite filledStarSprite; // 불이 들어온 별 이미지 스프라이트

    public PlayerMovement playerMovement; // 플레이어 스크립트 참조

    public bool isStageComplete = false; // 스테이지 성공 상태 확인

    public void CheckStageSuccess(AudioSource audioSource)
    {
        if (!audioSource.isPlaying && !isStageComplete)
        {
            StageSuccess();
        }
    }

    private void StageSuccess()
    {
        if (playerMovement.currentHealth > 0)
        {
            isStageComplete = true;

            int starsEarned = CalculateStars(playerMovement.currentHealth);
            SaveStarData(starsEarned, "Stage1_Stars"); // 스테이지 키를 전달
            UpdateStarDisplay(starsEarned);

            stageSuccessPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void SaveStarData(int starsEarned, string stageKey)
    {
        int previousStars = PlayerPrefs.GetInt(stageKey, 0); // 저장된 별 개수 불러오기

        // 기존 별 개수보다 높은 성과만 저장
        if (starsEarned > previousStars)
        {
            PlayerPrefs.SetInt(stageKey, starsEarned);
        }
    }

    private int CalculateStars(int health)
    {
        if (health == 5) return 3; // 체력이 5개면 별 3개
        if (health >= 3) return 2; // 체력이 3개 이상이면 별 2개
        return 1; // 체력이 1개 이상이면 별 1개
    }

    private void UpdateStarDisplay(int starsEarned)
    {
        for (int i = 0; i < starsEarned; i++)
        {
            starImages[i].sprite = filledStarSprite; // 불이 들어온 별 이미지로 변경
        }
    }
}