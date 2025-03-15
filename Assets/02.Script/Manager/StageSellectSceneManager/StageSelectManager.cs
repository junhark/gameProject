using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    [System.Serializable]
    public class StageInfo
    {
        public string stageName;
        public Sprite stageImage;
        public string sceneName; // �� �̸� (�ش� �������� ������ �̵��� �� ���)
        public Image[] starImages; // �������� ��ư�� ǥ�õ� �� UI
    }

    public StageInfo[] stages; // �������� ���� �迭
    public Sprite filledStarSprite; // ���� ���� �� �̹���
    public Sprite emptyStarSprite; // ���� ���� �� �̹���

    public GameObject infoPanel; // �������� ���� �г�
    public Text stageNameText; // �г� �� �������� �̸� �ؽ�Ʈ
    public Image stageImageDisplay; // �г� �� �������� �̹���
    private string selectedSceneName; // ���õ� �� �̸� ����

    private void Start()
    {
        infoPanel.SetActive(false); // ó������ �г��� ��Ȱ��ȭ

        // ��� ���������� �� UI�� �ʱ�ȭ
        UpdateAllStageStars();
    }

    public void OnStageButtonClicked(int stageIndex)
    {
        // �������� ���� ������Ʈ
        stageNameText.text = stages[stageIndex].stageName;
        stageImageDisplay.sprite = stages[stageIndex].stageImage;
        selectedSceneName = stages[stageIndex].sceneName;

        infoPanel.SetActive(true); // �г� Ȱ��ȭ
    }

    public void OnStartButtonClicked()
    {
        if (!string.IsNullOrEmpty(selectedSceneName))
        {
            SceneManager.LoadScene(selectedSceneName); // ���õ� �������� ������ �̵�
        }
    }

    public void OnCloseButtonClicked()
    {
        infoPanel.SetActive(false); // �г� �ݱ�
    }

    private void UpdateAllStageStars()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            UpdateStageStars($"Stage{i + 1}_Stars", stages[i].starImages);
        }
    }

    private void UpdateStageStars(string stageKey, Image[] starImages)
    {
        int starsEarned = PlayerPrefs.GetInt(stageKey, 0); // ����� �� ���� �ҷ�����

        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < starsEarned)
            {
                starImages[i].sprite = filledStarSprite; // ���� ���� ��
            }
            else
            {
                starImages[i].sprite = emptyStarSprite; // ���� ���� ��
            }
        }
    }
}