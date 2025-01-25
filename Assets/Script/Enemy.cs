using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int mHealth = 5;
    public float mSpeed = 10.0f;
    public float mCloseDistance = 0.2f;

    private Transform mTarget;
    private int mWayPointIndex = 0 ;
    private bool mIsAlive;

    private void Start()
    {
        transform.position = WaveSpawner.mStartPosition.position;
        mTarget = WayPoints.mPoints[0];
        mIsAlive = true;
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
        if (mIsAlive == false)
        {
            return;
        }
    }

    private void EnemyDistroy()
    {
        if (mHealth <= 0)
        {
            Destroy(gameObject);
            mIsAlive = false;
        }
        else if(Vector3.Distance(transform.position, WaveSpawner.mEndPosition.position) <= mCloseDistance)
        {
            Destroy(gameObject);
            mIsAlive = false;
        }
    }
}
