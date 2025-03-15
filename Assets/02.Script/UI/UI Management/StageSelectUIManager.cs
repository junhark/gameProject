using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectUIManager : MonoBehaviour
{
    public GameObject optionsPanel; // �ɼ� �г�

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Title"); // Title ������ �̵�
    }

    public void OnOptionsButtonClicked()
    {
        optionsPanel.SetActive(true); // �ɼ� �г� Ȱ��ȭ
    }

    public void OnCloseOptionsButtonClicked()
    {
        optionsPanel.SetActive(false); // �ɼ� �г� ��Ȱ��ȭ
    }
}