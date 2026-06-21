using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartGameScene()
    {
        SceneManager.LoadScene("SampleScene");  // ゲーム本編のシーン名を指定
    }
    public void StartMenuScene()
    {
        SceneManager.LoadScene("MainMenu");  // ゲーム本編のシーン名を指定
    }
}
