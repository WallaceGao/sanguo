using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public static int mMoney;
    public int mStartMoney = 100;
    public static int mHealth;
    public int mStartHealth = 5;
    public static int mKillNumber;
    public Text mMoneyText;

    private void Start()
    {
        mMoney = mStartMoney;
        mHealth = mStartHealth;
        mKillNumber = 0;
    }

    private void Update()
    {
        mMoneyText.text = mMoney.ToString();
    }
}
