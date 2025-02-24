using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float mHealth = 5.0f;
    public float mShield = 0.0f;
    public float mShieldResetTime = 2.0f;
    public float mShieldRechargeValue = 0.3f;
    public float mShieldRechargeRate = 0.5f;
    public float mSpeed = 10.0f;
    public float mCloseDistance = 0.2f;
    public float mCloseEndPosition = 2.0f;
    public int mAddMoney = 20;
    public GameObject mDeathEffect;
    [Header("Unity Staff")]
    public Image mHealthBar;
    public Image mShieldBar;
    
    private float mCurrentHealth = 0.0f;
    private float mCurrentShield = 0.0f;
    private float mShieldChargeCountDown = 0.0f;

    private void Start()
    {
        mCurrentHealth = mHealth;
        mCurrentShield = mShield;
    }

    private void Update()
    {
        if (mCurrentHealth <= 0)
        {
            OnDeath();
        }

        for (int i = 0; i < WaveSpawner.mEndPosition.Length; i++)
        {
            if (Vector3.Distance(transform.position, WaveSpawner.mEndPosition[i].position) <= mCloseEndPosition)
            {
                OnReachEnd();
            }
        }

        if (mShieldChargeCountDown <= 0.0f)
        {
            StartCoroutine(ShiledCharge());
        }

        mShieldChargeCountDown -= Time.deltaTime;
        mShieldChargeCountDown = Mathf.Clamp(mShieldChargeCountDown, 0.0f, Mathf.Infinity);
    }

    public void GetDamage(float damage)
    {
        mShieldChargeCountDown = mShieldResetTime;
        if (mCurrentShield > 0.0f)
        {
            Debug.Log("hit shield");
            mCurrentShield -= damage;
            GetBarChange(mCurrentShield, mShield, mShieldBar);
        }
        else
        {
            mCurrentHealth -= damage;
            GetBarChange(mCurrentHealth, mHealth, mHealthBar);
        }
    }


    private void GetBarChange(float currentValue ,float value, Image UI)
    {
        UI.fillAmount = currentValue / value;
    }

    IEnumerator ShiledCharge()
    {
        while (mCurrentShield < mShield)
        {
            mCurrentShield += mShieldRechargeValue / mShield;
            GetBarChange(mCurrentShield, mShield, mShieldBar);
            yield return new WaitForSeconds(mShieldRechargeRate);
        }
    }

    private void OnDeath()
    {
        PlayerStats.mKillNumber++;
        if (mDeathEffect != null)
        {
            GameObject deathEffect = Instantiate(mDeathEffect, transform.position, Quaternion.identity);
            Destroy(deathEffect, 2.0f);
        }
        PlayerStats.mMoney += mAddMoney;
        WaveSpawner.mEnemyAlive--;
        Destroy(gameObject);
    }

    private void OnReachEnd()
    {
        Debug.Log("Player lost health");
        PlayerStats.mHealth--;
        GameHealth.Instance.GetHealthDown();
        WaveSpawner.mEnemyAlive--;
        Destroy(gameObject);
    }
}
