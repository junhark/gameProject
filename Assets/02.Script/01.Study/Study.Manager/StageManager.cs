using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject stageSuccessPanel; // �������� ���� �г�
    public Image[] starImages; // �� �̹��� �迭 (�������� Ŭ���� �гο� ��ġ�� ��)
    public Sprite filledStarSprite; // ���� ���� �� �̹��� ��������Ʈ

    public PlayerMovement playerMovement; // �÷��̾� ��ũ��Ʈ ����

    public bool isStageComplete = false; // �������� ���� ���� Ȯ��

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
            SaveStarData(starsEarned, "Stage1_Stars"); // �������� Ű�� ����
            UpdateStarDisplay(starsEarned);

            stageSuccessPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void SaveStarData(int starsEarned, string stageKey)
    {
        int previousStars = PlayerPrefs.GetInt(stageKey, 0); // ����� �� ���� �ҷ�����

        // ���� �� �������� ���� ������ ����
        if (starsEarned > previousStars)
        {
            PlayerPrefs.SetInt(stageKey, starsEarned);
        }
    }

    private int CalculateStars(int health)
    {
        if (health == 5) return 3; // ü���� 5���� �� 3��
        if (health >= 3) return 2; // ü���� 3�� �̻��̸� �� 2��
        return 1; // ü���� 1�� �̻��̸� �� 1��
    }

    private void UpdateStarDisplay(int starsEarned)
    {
        for (int i = 0; i < starsEarned; i++)
        {
            starImages[i].sprite = filledStarSprite; // ���� ���� �� �̹����� ����
        }
    }
}