using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public GameObject optionsPanel; // 옵션 패널 오브젝트
    public Slider bgmSlider; // BGM 볼륨 조절 슬라이더
    public AudioSource bgmSource; // BGM 오디오 소스

    void Start()
    {
        optionsPanel.SetActive(false); // 처음에는 옵션 패널을 비활성화
        bgmSlider.value = bgmSource.volume; // 슬라이더 초기값을 현재 BGM 볼륨으로 설정
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("StageSellect");
    }

    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnOptionsButtonClicked()
    {
        optionsPanel.SetActive(true); // 옵션 패널 활성화
    }

    public void OnCloseOptionsButtonClicked()
    {
        optionsPanel.SetActive(false); // 옵션 패널 비활성화
    }

    public void OnBGMSliderValueChanged()
    {
        bgmSource.volume = bgmSlider.value; // 슬라이더 값에 따라 BGM 볼륨 조절
    }
}