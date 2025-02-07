using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillNumber : MonoBehaviour
{
    public Text mKillText;
    public float mWaitTime = 0.05f; 

    private void OnEnable()
    {
        StartCoroutine(AnimalText());
    }

    IEnumerator AnimalText()
    {
        mKillText.text = "0";
        int killCount = 0;
        while(killCount < PlayerStats.mKillNumber)
        {
            killCount++;
            mKillText.text = killCount.ToString();
            yield return new WaitForSeconds(mWaitTime);
        }
    }
}