using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] mSetEndPosition;
    public static Transform[] mEndPosition;
    public float mTimeBetweenWave = 2.0f;
    public float mTimeBetweenEnemySpwan = 0.5f;
    public static int mTatalWave = 0;
    public Text mWaveCountDown;
    public static int mEnemyAlive = 0;
    public Text mEnemyCount;
    public Wave[] mWaves;

    private float mCountDown = 2.0f;
    private int mWaveNumber = 0;

    private void Awake()
    {
        mEndPosition = new Transform[mSetEndPosition.Length];
        for (int i = 0; i < mSetEndPosition.Length; i++)
        {
            mEndPosition[i] = mSetEndPosition[i];
        }
    }

    public void Start()
    {
        mEnemyAlive = 0;
        mTatalWave = 0;
        mWaveNumber = 0;
    }

    private void Update()
    {
        mEnemyCount.text = mEnemyAlive.ToString();
        mWaveCountDown.text = (mWaveNumber + 1).ToString();
        //Debug.Log(mEnemyAlive);

        if (mEnemyAlive > 0)
        {
            return;
        }
        if (mCountDown <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            mCountDown = mTimeBetweenWave;
        }

        if (mWaveNumber == mWaves.Length && mEnemyAlive <= 0)
        {
            GameManager.mInstence.WinLevel();
            this.enabled = false;
        }

        mCountDown -= Time.deltaTime;
        mCountDown = Mathf.Clamp(mCountDown, 0.0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave()
    {
        Wave wave = mWaves[mWaveNumber];

        Debug.Log($"Wave {mWaveNumber + 1} Incoming!");

        List<Coroutine> activeCoroutines = new List<Coroutine>();

        // 遍历每个敌人组，并行生成不同类型的敌人
        foreach (var group in wave.mEnemyGroups)
        {
            mEnemyAlive += group.mCount;
            activeCoroutines.Add(StartCoroutine(SpawnEnemyGroup(group)));
        }

        // **等待所有敌人组刷怪完毕**
        foreach (var coroutine in activeCoroutines)
        {
            yield return coroutine;
        }

        Debug.Log($"Wave {mWaveNumber + 1} Completed!");
        mWaveNumber++; // **在所有敌人刷完后再增加 Wave 计数**
    }

    IEnumerator SpawnEnemyGroup(Wave.EnemyGroup group)
    {
        // 先等待 `mTimeDelay` 秒
        if (group.mTimeDelay > 0)
        {
            yield return new WaitForSeconds(group.mTimeDelay);
        }

        // 让这个组稍后再开始刷怪
        for (int i = 0; i < group.mCount; i++)
        {
            SpawnEnemy(group.mEnemyPrefab, group.mSpawnPosition, group.mEnemyPath);
            yield return new WaitForSeconds(group.mRate); // 按照 mRate 控制刷怪速度
        }
    }

    private void SpawnEnemy(GameObject enemy, Transform startPositon, WayPoints enemyPath)
    {
        if (enemy == null)
        {
            Debug.LogError("Enemy prefab is null!");
            return;
        }

        GameObject newEnemy = Instantiate(enemy, startPositon.position, startPositon.rotation);

        EnemyMovement enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetPath(enemyPath); // path the WayPoints
        }
        else
        {
            Debug.LogError("Spawned enemy does not have an EnemyMovement component!");
        }
    }
}
