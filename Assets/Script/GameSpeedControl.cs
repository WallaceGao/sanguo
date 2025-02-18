
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedController : MonoBehaviour
{
    public Button button1x;
    public Button button2x;
    public Button button3x;
    public Color mNoClick;
    public Color mClick;

    private void Start()
    {
        // 绑定点击事件
        button1x.onClick.AddListener(SetSpeed1x);
        button2x.onClick.AddListener(SetSpeed2x);
        button3x.onClick.AddListener(SetSpeed3x);

        // 默认 1x 速度
        SetSpeed1x();
    }


    public void SetSpeed1x()
    {
        Time.timeScale = 1f;
        UpdateButtonState(button1x);
        Debug.Log("Game Speed Set To: 1x");
    }

    public void SetSpeed2x()
    {
        Time.timeScale = 2f;
        UpdateButtonState(button2x);
        Debug.Log("Game Speed Set To: 2x");
    }

    public void SetSpeed3x()
    {
        Time.timeScale = 3f;
        UpdateButtonState(button3x);
        Debug.Log("Game Speed Set To: 3x");
    }

    private void UpdateButtonState(Button activeButton)
    {
        // set all color to deffuf
        button1x.image.color = mNoClick;
        button2x.image.color = mNoClick;
        button3x.image.color = mNoClick;

        // set active Button to deepColors
        activeButton.image.color = mClick;
    }
}

