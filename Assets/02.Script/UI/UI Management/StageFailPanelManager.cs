using UnityEngine;
using UnityEngine.SceneManagement;

public class StageFailPanelManager : MonoBehaviour
{
    // ���� �� �ٽ� ����
    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // �������� ���� ������ �̵�
    public void GoToStageSelectScene()
    {
        SceneManager.LoadScene("StageSellect");
    }
}