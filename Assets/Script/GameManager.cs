using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager mInstence;
    public GameObject mGameOverUi;
    public int mLevelToUnlock = 2;
    public string mNextLevel = "Level1-2";

    public SceneFader mSceneFader;

    public static bool mGameIsOver;

    private void Awake()
    {
        if (mInstence != null)
        {
            Debug.Log("You build more than one Build Manager");
        }
        mInstence = this;
    }

    private void Start()
    {
        mGameIsOver = false;
        mGameOverUi.SetActive(false);
    }

    private void Update()
    {
        if(mGameIsOver)
        {
            return;
        }

        if(PlayerStats.mHealth <= 0)
        {
            mGameOverUi.SetActive(true);
            Debug.Log("GameOver");
            GameEnd();
        }
    }

    private void GameEnd()
    {
        mGameIsOver = true;
        mGameOverUi.SetActive(true);
    }

    public void WinLevel()
    {
        Debug.Log("finish level");
        PlayerPrefs.SetInt("LevelReached", mLevelToUnlock);
        mSceneFader.FadeTo(mNextLevel);
    }
}
