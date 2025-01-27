using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager mInstence;
    private void Awake()
    {
        if (mInstence != null)
        {
            Debug.Log("You build more than one Build Manager");
        }
        mInstence = this;
    }

    private bool mEndGame = false;

    private void Update()
    {
        if(mEndGame)
        {
            return;
        }

        if(PlayerStats.mHealth <= 0)
        {
            GameEnd();
        }
    }

    private void GameEnd()
    {
        mEndGame = true;
        Debug.Log("Game Over");
    }

}
