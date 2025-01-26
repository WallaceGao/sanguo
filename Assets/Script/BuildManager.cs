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

    private GameObject mTurretToBuild;
    public GameObject GetTurretToBuild()
    {
        return mTurretToBuild; 
    }

    public void SetTurretToBuild(GameObject selectTurret)
    {
        mTurretToBuild = selectTurret;
    }

}
