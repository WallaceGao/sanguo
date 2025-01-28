using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform mTarget;
    private int mWayPointIndex = 0;

    private Enemy mEnemy;
    private float mCurrentSpeed;

    private void Start()
    {
        mEnemy = GetComponent<Enemy>();
        transform.position = WaveSpawner.mStartPosition.position;
        mTarget = WayPoints.mPoints[0];
        mCurrentSpeed = mEnemy.mSpeed;
    }

    private void Update()
    {
        Vector3 direction = mTarget.position - transform.position;
        transform.Translate(direction.normalized * mCurrentSpeed * Time.deltaTime, Space.World);

        //get next waypoints
        if (Vector3.Distance(transform.position, mTarget.position) <= mEnemy.mCloseDistance && mWayPointIndex < WayPoints.mPoints.Length - 1)
        {
            mWayPointIndex++;
            mTarget = WayPoints.mPoints[mWayPointIndex];
        }

        mCurrentSpeed = mEnemy.mSpeed;
    }

    public void Slow(float percent)
    {
        mCurrentSpeed = mEnemy.mSpeed * (1.0f - percent);
    }
}
