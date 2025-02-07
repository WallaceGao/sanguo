using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public string mMenu = "Main";
    public SceneFader mSceneFader;

    public void Retry()
    {
        mSceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        mSceneFader.FadeTo(mMenu);
    }

}
