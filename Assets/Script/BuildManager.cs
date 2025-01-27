using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager mInstence;
    private void Awake()
    {
        if(mInstence != null)
        {
            Debug.Log("You build more than one Build Manager");
        }
        mInstence = this;
    }

    public GameObject mStandProjectPrefab;
    public GameObject mMissileTurretPrefab;
    public GameObject mBuildEffect;

    private TurretBluePrint mTurretToBuild;

    public bool CanBuild { get { return mTurretToBuild != null; } }
    public bool HaveMoney { get { return PlayerStats.mMoney >= mTurretToBuild.mCost; } }

    public void BuildTurretOn(Node node)
    {
        if(PlayerStats.mMoney < mTurretToBuild.mCost)
        {
            Debug.Log("Not Enough Money!");
            return;
        }

        PlayerStats.mMoney -= mTurretToBuild.mCost;

        GameObject effect = (GameObject)Instantiate(mBuildEffect, node.GetTurretPosition(), Quaternion.identity);
        Destroy(effect, 5.0f);

        GameObject turret = (GameObject)Instantiate(mTurretToBuild.mPrefab, node.GetTurretPosition(), Quaternion.identity);
        node.SetTurret(turret);
        
    }

    public void SelectTurretToBuild(TurretBluePrint selectTurret)
    {
        mTurretToBuild = selectTurret;
    }



}
