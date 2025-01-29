using UnityEngine;
using UnityEngine.UI;

public class NodeUi : MonoBehaviour
{
    public GameObject mNodeUi;

    private Node mTarget;
    public Button mUpgradeButton;


    private void Start()
    {
        mTarget = null;
        mNodeUi.SetActive(false);
    }

    public void SetNode(Node node)
    {
        if(!node.mIsUpgrade)
        {
            mUpgradeButton.interactable = true;
        }
        else
        {
            mUpgradeButton.interactable = false;
        }
        mTarget = node;
        transform.position = mTarget.GetTurretPosition();
        mNodeUi.SetActive(true);
    }

    public void Hide()
    {
        mNodeUi.SetActive(false);
    }

    public void Upgrade()
    {
        mTarget.UpgradeTurret();
        BuildManager.mInstence.DeselectNode();
    }

    public void Sell()
    {
        mTarget.SellTurret();
        BuildManager.mInstence.DeselectNode();
    }
}
