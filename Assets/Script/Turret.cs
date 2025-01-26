using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    public float mRange = 15.0f;
    public float mRotaSpeed = 5.0f;
    public float mFireRate = 1.0f;
    public float mFireCountDown = 1.0f;

    [Header("UnitySetUp")]
    public string mEnemyTag = "Enemy";
    public Transform mRotaPart;
    public GameObject mBullet;
    public Transform mFirePosition;

    private Transform mTarget;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    private void UpdateTarget()
    {
        //Find Target
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(mEnemyTag);
        float shortestDistence = Mathf.Infinity;
        GameObject NearestEnemy = null;
        foreach (GameObject Enemy in Enemies)
        {
            float distenceToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);
            if(shortestDistence > distenceToEnemy)
            {
                shortestDistence = distenceToEnemy;
                NearestEnemy = Enemy;
            }
        }

        if(NearestEnemy != null && shortestDistence <= mRange )
        {
            mTarget = NearestEnemy.transform;
        }
        else
        {
            mTarget = null;
        }
    }

    private void Update()
    {
        if(mTarget == null)
        {
            return;
        }

        //Target Lock on
        Vector3 dirction = mTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dirction);
        Vector3 rotation = Quaternion.Lerp(mRotaPart.rotation ,lookRotation,Time.deltaTime * mRotaSpeed).eulerAngles;
        mRotaPart.rotation = Quaternion.Euler(0.0f,rotation.y,0.0f);

        if(mFireCountDown <= 0.0f)
        {
            Shoot();
            mFireCountDown = 1.0f / mFireRate;
        }

        mFireCountDown -= Time.deltaTime;
    }

    private void Shoot()
    {
        //Debug.Log("Shoot");
        GameObject bulletGo = (GameObject)Instantiate(mBullet, mFirePosition.position, mFirePosition.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(mTarget);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, mRange);
    }



}
