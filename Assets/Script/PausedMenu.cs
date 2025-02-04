using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public GameObject ui;
    public string mMenu = "Main";
    public SceneFader mSceneFader;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
       
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf); // active Self do ture when it's a false
        if (ui.activeSelf)
        {
            Time.timeScale = 0.0f;   
        }
        else
        {
            Time.timeScale = 1.0f; // timeScale 1 is nomal speed, 0 is stop, 2 is double speed
        }

    }

    public void Retry()
    {
        Toggle();
        mSceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    
    public void Menu()
    {
        Toggle();
        mSceneFader.FadeTo(mMenu);
    }
}
