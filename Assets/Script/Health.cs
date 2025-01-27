using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Text mHealthText;

    void Update()
    {
        mHealthText.text = PlayerStats.mHealth.ToString(); 
    }
}
