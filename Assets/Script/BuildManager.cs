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
    public NodeUi mNodeUi;

    private TurretBluePrint mTurretToBuild;
    private Node mSelectNode;

    public bool CanBuild { get { return mTurretToBuild != null; } }
    public bool HaveMoney { get { return PlayerStats.mMoney >= mTurretToBuild.mCost; } }


    public void SelectTurretToBuild(TurretBluePrint selectTurret)
    {
        mTurretToBuild = selectTurret;
        DeselectNode();
    }

    public void SelectTurretNode(Node node)
    {
        if (mSelectNode == node)
        {
            DeselectNode();
            return;
        }
        mSelectNode = node;
        mNodeUi.SetNode(node);
        mTurretToBuild = null;
    }

    public void DeselectNode()
    {
        Debug.Log("deselect Node");
        mSelectNode = null;
        mNodeUi.Hide();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return mTurretToBuild;
    }

    public Node GetSelectNode()
    {
        return mSelectNode;
    }
}
