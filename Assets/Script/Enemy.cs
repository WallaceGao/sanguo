using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float mHealth = 5.0f;
    public float mCurrentHealth = 0.0f;
    public float mSpeed = 10.0f;
    public float mCloseDistance = 0.2f;
    public float mCloseEndPosition = 2.0f;
    public int mAddMoney = 20;
    public GameObject mDeathEffect;
    [Header("Unity Staff")]
    public Image mHealthBar;

    private void Start()
    {
        mCurrentHealth = mHealth;
    }

    private void Update()
    {
        if (mCurrentHealth <= 0)
        {
            PlayerStats.mKillNumber++;
            GameObject deathEffect = (GameObject)Instantiate(mDeathEffect, transform.position, Quaternion.identity);
            Destroy(deathEffect, 2.0f);
            PlayerStats.mMoney += mAddMoney;
            Destroy(gameObject);
        }
        if (Vector3.Distance(transform.position, WaveSpawner.mEndPosition.position) <= mCloseEndPosition)
        {
            Debug.Log("player lose health");
            PlayerStats.mHealth--;
            Destroy(gameObject);
        }
    }

    public void GetDamage(float damage)
    {
        mCurrentHealth -= damage;

        float healthImage = mCurrentHealth / mHealth; 
        mHealthBar.fillAmount = healthImage;
    }
}
