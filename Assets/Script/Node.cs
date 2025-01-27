using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color mHoverColor;
    public Color mStartColor;
    public Color mCantBuildColor;
    public Vector3 mTurretOffSet;

    private Renderer mRenderer;
    private GameObject mCurrentTurret;
    private BuildManager mBuildManager;

    private void Start()
    {
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
        if (!mBuildManager.CanBuild)
            return;

        if (mCurrentTurret != null)
        {
            Debug.Log("Turret already build");
            return;
        }

        //build turret
        mBuildManager.BuildTurretOn(this);
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
