using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    public string mMenu = "Main";
    public SceneFader mSceneFader;
    public int mLevelToUnlock = 2;
    public string mNextLevel = "Level1-2";

    public void Continue()
    {
        PlayerPrefs.SetInt("LevelReached", mLevelToUnlock);
        mSceneFader.FadeTo(mNextLevel);
    }

    public void Menu()
    {
        mSceneFader.FadeTo(mMenu);
    }
}
