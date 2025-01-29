using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color mHoverColor;
    public Color mStartColor;
    public Color mCantBuildColor;
    public Vector3 mTurretOffSet;
    public TurretBluePrint mTurretBluePrint;
    public bool mIsUpgrade;

    private Renderer mRenderer;
    private GameObject mCurrentTurret;
    private BuildManager mBuildManager;

    private void Start()
    {
        mIsUpgrade = false;
        mRenderer = GetComponent<Renderer>();
        mRenderer.material.color = mStartColor;
        mBuildManager = BuildManager.mInstence;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //check is the object still select
            return;
        if (!mBuildManager.CanBuild)
            return;

        if(!mBuildManager.HaveMoney)
        {
            mRenderer.material.color = mCantBuildColor;
        }
        else
        {
            mRenderer.material.color = mHoverColor;
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //check is the object still select
            return;
 

        if (mCurrentTurret != null)
        {
            //Debug.Log("Turret already build");
            mBuildManager.SelectTurretNode(this);
            return;
        }

        if (!mBuildManager.CanBuild)
                return;
        //build turret
        BuildTurret(mBuildManager.GetTurretToBuild());

    }

    private void BuildTurret(TurretBluePrint turretBluePrint)
    {
        if (PlayerStats.mMoney < turretBluePrint.mCost)
        {
            Debug.Log("Not Enough Money!");
            return;
        }
        if (turretBluePrint.mCost == 0)
        {
            return;
        }

        PlayerStats.mMoney -= turretBluePrint.mCost;

        GameObject effect = (GameObject)Instantiate(mBuildManager.mBuildEffect, GetTurretPosition(), Quaternion.identity);
        Destroy(effect, 5.0f);

        GameObject turret = (GameObject)Instantiate(turretBluePrint.mPrefab, GetTurretPosition(), Quaternion.identity);
        SetTurret(turret);

        mTurretBluePrint = turretBluePrint;
    }

    public void SellTurret()
    {
        if (!mIsUpgrade)
            PlayerStats.mMoney += mTurretBluePrint.mCost / 2;
        else
            PlayerStats.mMoney += (mTurretBluePrint.mCost + mTurretBluePrint.mUpgradeCost) / 2;
        //effect

        Destroy(mCurrentTurret);
        mTurretBluePrint = null;
        mIsUpgrade = false;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.mMoney < mTurretBluePrint.mUpgradeCost)
        {
            Debug.Log("Not Enough Money!");
            return;
        }

        PlayerStats.mMoney -= mTurretBluePrint.mUpgradeCost;
        Destroy(mCurrentTurret);

        GameObject effect = (GameObject)Instantiate(mBuildManager.mBuildEffect, GetTurretPosition(), Quaternion.identity);
        Destroy(effect, 5.0f);

        GameObject turret = (GameObject)Instantiate(mTurretBluePrint.mUpgradePrefab , GetTurretPosition(), Quaternion.identity);
        SetTurret(turret);
        mIsUpgrade = true;
    }

    private void OnMouseExit()
    {
        mRenderer.material.color = mStartColor;
    }

    public Vector3 GetTurretPosition()
    {
        return transform.position + mTurretOffSet;
    }

    public void SetTurret(GameObject turret)
    {
        mCurrentTurret = turret;
    }
}
