using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int mMoney;
    public int mStartMoney = 100;
    public static int mHealth;
    public int mStartHealth;

    private void Start()
    {
        mMoney = mStartMoney;
        mHealth = mStartHealth;
    }
}
