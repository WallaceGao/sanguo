using UnityEngine;
using UnityEngine.UI;

public class MoneyUi : MonoBehaviour
{
    public Text mMoneyText;

    private void Update()
    {
        mMoneyText.text = "Money: " + PlayerStats.mMoney.ToString();
    }

}
