using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playLevel = "Level1";
    public SceneFader mSceneFader;

    public void Play()
    {
        mSceneFader.FadeTo(playLevel);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
