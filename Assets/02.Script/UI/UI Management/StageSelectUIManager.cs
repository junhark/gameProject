using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectUIManager : MonoBehaviour
{
    public GameObject optionsPanel; // 옵션 패널

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Title"); // Title 씬으로 이동
    }

    public void OnOptionsButtonClicked()
    {
        optionsPanel.SetActive(true); // 옵션 패널 활성화
    }

    public void OnCloseOptionsButtonClicked()
    {
        optionsPanel.SetActive(false); // 옵션 패널 비활성화
    }
}