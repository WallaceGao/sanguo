using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform mSetStartPosition;
    public Transform mSetEndPosition;
    public static Transform mStartPosition;
    public static Transform mEndPosition;
    public float mTimeBetweenWave = 2.0f;
    public float mTimeBetweenEnemySpwan = 0.5f;
    public static int mTatalWave = 0;
    public Text mWaveCountDown;
    public static int mEnemyAlive = 0;
    public Text mEnemyCount;
    public Wave[] mWaves;

    private float mCountDown = 2.0f;
    private int mWaveNumeber = 0;

    private void Awake()
    {
        mStartPosition = mSetStartPosition;
        mEndPosition = mSetEndPosition;
    }

    public void Start()
    {
        mWaveNumeber = 0;
        mEnemyAlive = 0;
        mTatalWave = 0;
    }

    private void Update()
    {
        mEnemyCount.text = string.Format(mEnemyAlive.ToString());
        mWaveCountDown.text = string.Format((mWaveNumeber + 1).ToString());
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

        if (mWaveNumeber == mWaves.Length)
        {
            GameManager.mInstence.WinLevel();
            this.enabled = false;
        }

        mCountDown -= Time.deltaTime;
        mCountDown = Mathf.Clamp(mCountDown, 0.0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave()
    {
        Wave wave = mWaves[mWaveNumeber];
        mEnemyAlive = wave.mCount;
        for (int i = 0; i < wave.mCount; i++)
        {
            SpawnEnemy(wave.mEnemy);
            yield return new WaitForSeconds(1f/wave.mRate);
        }

        Debug.Log("Wave Incomming");
        mWaveNumeber++;
    }


    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,mStartPosition.position,mStartPosition.rotation);
    }
}
