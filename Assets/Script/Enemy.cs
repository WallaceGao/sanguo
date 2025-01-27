using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int mHealth = 5;
    public float mSpeed = 10.0f;
    public float mCloseDistance = 0.2f;
    public float mCloseEndPosition = 2.0f;
    public int mAddMoney = 20;
    public GameObject mDeathEffect;

    private Transform mTarget;
    private int mWayPointIndex = 0 ;

    private void Start()
    {
        transform.position = WaveSpawner.mStartPosition.position;
        mTarget = WayPoints.mPoints[0];
    }

    private void Update()
    {
        Vector3 direction = mTarget.position - transform.position;
        transform.Translate(direction.normalized * mSpeed * Time.deltaTime, Space.World);
        
        //get next waypoints
        if (Vector3.Distance(transform.position, mTarget.position) <= mCloseDistance && mWayPointIndex < WayPoints.mPoints.Length -1)
        {
            mWayPointIndex++;
            mTarget = WayPoints.mPoints[mWayPointIndex];
        }

        EnemyDistroy();

    }

    private void EnemyDistroy()
    {
        if (mHealth <= 0)
        {
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
}
