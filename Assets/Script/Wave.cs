using UnityEngine;

[System.Serializable]
public class Wave
{
    [System.Serializable]
    public struct EnemyGroup
    {
        public GameObject mEnemyPrefab;
        public int mCount;
        public float mRate;
        public Transform mSpawnPosition;
        public WayPoints mEnemyPath;
        public float mTimeDelay;
    }

    public EnemyGroup[] mEnemyGroups;
}
