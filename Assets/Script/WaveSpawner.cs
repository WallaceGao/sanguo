using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform mEnemyPrefab;
    public Transform mSetStartPosition;
    public Transform mSetEndPosition;
    public static Transform mStartPosition;
    public static Transform mEndPosition;
    public float mTimeBetweenWave = 5.0f;
    public float mTimeBetweenEnemySpwan = 0.5f;
    public static int mTatalWave = 1;
    public Text WaveCountDown;

    private float mCountDown = 2.0f;
    private int mWaveNumeber = 0;

    private void Awake()
    {
        mStartPosition = mSetStartPosition;
        mEndPosition = mSetEndPosition;
    }

    public void Start()
    {
        mTatalWave = 1;
    }

    private void Update()
    {
        if(mCountDown <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            mCountDown = mTimeBetweenWave;
        }

        mCountDown -= Time.deltaTime;
        mCountDown = Mathf.Clamp(mCountDown, 0.0f, Mathf.Infinity);

        WaveCountDown.text = string.Format(mTatalWave.ToString());
    }

    IEnumerator SpawnWave()
    {
        mWaveNumeber++;
        for (int i = 0; i < mWaveNumeber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(mTimeBetweenEnemySpwan);
        }

        Debug.Log("Wave Incomming");
    }


    private void SpawnEnemy()
    {
        Instantiate(mEnemyPrefab,mStartPosition.position,mStartPosition.rotation);
    }
    
}
