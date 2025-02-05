using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader mSceneFader;
    public Button[] mButtons;


    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached",0);
        Debug.Log(levelReached.ToString());
        for (int i = 0; i < mButtons.Length; i++)
        {
            if (i > levelReached)// 0 > 1 then level can run
            {
                Debug.Log("level" + i.ToString());
                mButtons[i].interactable = false;
            }
        }
    }

    public void SelectLevel(string LevelName)
    {
        mSceneFader.FadeTo(LevelName);
    }

    

}
