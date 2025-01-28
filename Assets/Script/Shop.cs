using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint mStandTurret;
    public TurretBluePrint mMissileTurret;
    public TurretBluePrint mLaserTurret;

    private BuildManager mBuildManager;
    

    private void Start()
    {
        mBuildManager = BuildManager.mInstence;
        mStandTurret.SetUI();
        mMissileTurret.SetUI();
        mLaserTurret.SetUI();
    }

    public void SelectStanderTurret()
    {
        Debug.Log("Stander Turret Puchase");
        mBuildManager.SelectTurretToBuild(mStandTurret);
    }


    public void SelectMissleTurret()
    {
        Debug.Log("Stander Turret Puchase");
        mBuildManager.SelectTurretToBuild(mMissileTurret);
    }

    public void SelectLaserTurret()
    {
        Debug.Log("Stander Turret Puchase");
        mBuildManager.SelectTurretToBuild(mLaserTurret);
    }

}
