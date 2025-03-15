using UnityEngine;
using UnityEngine.SceneManagement;

public class StageFailPanelManager : MonoBehaviour
{
    // 현재 씬 다시 시작
    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // 스테이지 선택 씬으로 이동
    public void GoToStageSelectScene()
    {
        SceneManager.LoadScene("StageSellect");
    }
}