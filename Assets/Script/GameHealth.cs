using UnityEngine;
using UnityEngine.UI;

public class GameHealth : MonoBehaviour
{

    private static GameHealth mInstance;

    public static GameHealth Instance
    {
        get
        {
            if (mInstance == null)
            {
                Debug.LogError("GameHealth instance is missing!");
            }
            return mInstance;
        }
    }
    private void Awake()
    {
        if (mInstance != null && mInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        mInstance = this;
    }


    private int mNumberOfHealth = 0;
    public Image[] mHealthBar;

    private void Start()
    {
        mNumberOfHealth = mHealthBar.Length -1;
    }

    public void GetHealthDown()
    {
        if(!GameManager.mGameIsOver)
        {
            if (mNumberOfHealth >= mHealthBar.Length)
            {
                Debug.LogWarning("No more health to reduce!");
                return;
            }

            mHealthBar[mNumberOfHealth].enabled = false;
            mNumberOfHealth--;
        }
    }
}
