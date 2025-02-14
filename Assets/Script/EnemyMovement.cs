using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform mTarget;
    private int mWayPointIndex = 0;

    
    private Enemy mEnemy;
    private float mCurrentSpeed;
    private Transform[] mPaths;

    private void Start()
    {
        if(mPaths == null || mPaths.Length == 0)
        {
            Debug.LogError("Enemy has no path assigned");
            return;
        }
        mTarget = mPaths[0];
        mEnemy = GetComponent<Enemy>();
        mCurrentSpeed = mEnemy.mSpeed;
    }

    private void Update()
    {
        Vector3 direction = mTarget.position - transform.position;
        transform.Translate(direction.normalized * mCurrentSpeed * Time.deltaTime, Space.World);

        //get next waypoints
        if (Vector3.Distance(transform.position, mTarget.position) <= mEnemy.mCloseDistance && mWayPointIndex < mPaths.Length - 1)
        {
            mWayPointIndex++;
            mTarget = mPaths[mWayPointIndex];
        }

        mCurrentSpeed = mEnemy.mSpeed;
    }

    public void Slow(float percent)
    {
        mCurrentSpeed = mEnemy.mSpeed * (1.0f - percent);
    }

    public void SetPath(WayPoints path)
    {
        if (path != null)
        {
            mPaths = path.mPoints; // 让敌人知道应该走哪条路径
        }
        else
        {
            Debug.LogError("Path is null!");
        }
    }
}
