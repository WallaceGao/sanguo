using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager mBuildManager;

    private void Start()
    {
        mBuildManager = BuildManager.mInstence; 
    }

    public void PurchaseStanderTurret()
    {
        Debug.Log("Stander Turret Puchase");
        mBuildManager.SetTurretToBuild(mBuildManager.mStandProjectPrefab);
    }


    public void PurchaseMissleTurret()
    {
        Debug.Log("Stander Turret Puchase");
        mBuildManager.SetTurretToBuild(mBuildManager.mMissileTurretPrefab);
    }
}
