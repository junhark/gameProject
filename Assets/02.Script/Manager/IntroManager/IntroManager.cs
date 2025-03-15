using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public GameObject optionsPanel; // �ɼ� �г� ������Ʈ
    public Slider bgmSlider; // BGM ���� ���� �����̴�
    public AudioSource bgmSource; // BGM ����� �ҽ�

    void Start()
    {
        optionsPanel.SetActive(false); // ó������ �ɼ� �г��� ��Ȱ��ȭ
        bgmSlider.value = bgmSource.volume; // �����̴� �ʱⰪ�� ���� BGM �������� ����
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
        optionsPanel.SetActive(true); // �ɼ� �г� Ȱ��ȭ
    }

    public void OnCloseOptionsButtonClicked()
    {
        optionsPanel.SetActive(false); // �ɼ� �г� ��Ȱ��ȭ
    }

    public void OnBGMSliderValueChanged()
    {
        bgmSource.volume = bgmSlider.value; // �����̴� ���� ���� BGM ���� ����
    }
}