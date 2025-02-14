using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject mEnemy;
    public int mCount;
    public float mRate;
    public Transform mSpawnPosition;
    public WayPoints mEnemyPath;
}
