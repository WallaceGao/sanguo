using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform mSetEndPosition;
    public static Transform mEndPosition;
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
        mEndPosition = mSetEndPosition;
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

        if (mWaveNumber == mWaves.Length)
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
        mEnemyAlive = wave.mCount;
        for (int i = 0; i < wave.mCount; i++)
        {
            SpawnEnemy(wave.mEnemy,wave.mSpawnPosition,wave.mEnemyPath);
            yield return new WaitForSeconds(1f/wave.mRate);
        }

        Debug.Log("Wave Incomming");
        mWaveNumber++;
    }


    private void SpawnEnemy(GameObject enemy, Transform startPositon,WayPoints enemyPath)
    {
        if (enemy == null)
        {
            Debug.LogError("Enemy prefab is null!");
            return;
        }

        GameObject newEnemy = Instantiate(enemy, startPositon.position , startPositon.rotation);
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
