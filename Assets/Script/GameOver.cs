using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text mKillText;
    public string mMenu = "Main";
    public SceneFader mSceneFader;

    private void OnEnable()
    {
        mKillText.text = PlayerStats.mKillNumber.ToString(); 
    }

    public void Retry()
    {
        mSceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        mSceneFader.FadeTo(mMenu);
    }

}
