using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint mStandTurret;
    public TurretBluePrint mMissileTurret;

    private BuildManager mBuildManager;
    

    private void Start()
    {
        mBuildManager = BuildManager.mInstence;
        mStandTurret.SetUI();
        mMissileTurret.SetUI();
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
}
