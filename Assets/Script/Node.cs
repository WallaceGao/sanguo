using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color mHoverColor;
    public Color mStartColor;

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
        if (mBuildManager.GetTurretToBuild() == null)
            return;

        mRenderer.material.color = mHoverColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //check is the object still select
            return;
        if (mBuildManager.GetTurretToBuild() == null)
            return;

        if(mCurrentTurret != null)
        {
            Debug.Log("Turret already build");
            return;
        }

        //build turret
        GameObject TurretToBuild = BuildManager.mInstence.GetTurretToBuild();
        mCurrentTurret = (GameObject)Instantiate(TurretToBuild, transform.position, transform.rotation);
    }

    private void OnMouseExit()
    {
        mRenderer.material.color = mStartColor;
    }

}
