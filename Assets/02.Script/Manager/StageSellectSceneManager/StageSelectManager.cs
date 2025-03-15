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
        public string sceneName; // 씬 이름 (해당 스테이지 씬으로 이동할 때 사용)
        public Image[] starImages; // 스테이지 버튼에 표시될 별 UI
    }

    public StageInfo[] stages; // 스테이지 정보 배열
    public Sprite filledStarSprite; // 불이 들어온 별 이미지
    public Sprite emptyStarSprite; // 불이 꺼진 별 이미지

    public GameObject infoPanel; // 스테이지 정보 패널
    public Text stageNameText; // 패널 내 스테이지 이름 텍스트
    public Image stageImageDisplay; // 패널 내 스테이지 이미지
    private string selectedSceneName; // 선택된 씬 이름 저장

    private void Start()
    {
        infoPanel.SetActive(false); // 처음에는 패널을 비활성화

        // 모든 스테이지의 별 UI를 초기화
        UpdateAllStageStars();
    }

    public void OnStageButtonClicked(int stageIndex)
    {
        // 스테이지 정보 업데이트
        stageNameText.text = stages[stageIndex].stageName;
        stageImageDisplay.sprite = stages[stageIndex].stageImage;
        selectedSceneName = stages[stageIndex].sceneName;

        infoPanel.SetActive(true); // 패널 활성화
    }

    public void OnStartButtonClicked()
    {
        if (!string.IsNullOrEmpty(selectedSceneName))
        {
            SceneManager.LoadScene(selectedSceneName); // 선택된 스테이지 씬으로 이동
        }
    }

    public void OnCloseButtonClicked()
    {
        infoPanel.SetActive(false); // 패널 닫기
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
        int starsEarned = PlayerPrefs.GetInt(stageKey, 0); // 저장된 별 개수 불러오기

        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < starsEarned)
            {
                starImages[i].sprite = filledStarSprite; // 불이 들어온 별
            }
            else
            {
                starImages[i].sprite = emptyStarSprite; // 불이 꺼진 별
            }
        }
    }
}