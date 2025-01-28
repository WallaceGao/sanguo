using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //make a class where other class can use and it will show on unity
public class TurretBluePrint
{
    public Text mCostText;
    public Text mNameText;
    public string mTurretName;
    public GameObject mPrefab;
    public int mCost;
    public GameObject mUpgradePrefab;
    public int mUpgradeCost;

    public void SetUI()
    {
        mCostText.text = "$: " + mCost.ToString();
        mNameText.text = mTurretName;
    }

}
