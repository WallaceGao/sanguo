using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attributes General")]
    public float mRange = 15.0f;
    public float mRotaSpeed = 5.0f;

    [Header("Attributes Bullets")]
    public float mFireRate = 1.0f;
    public int mAmmo = 1;
    public float mReload = 1.0f;

    [Header("Attributes Laser")]
    public float mLaserDamage = 0.5f;
    public float mSlowPercent = 0.0f;
    public bool mUseLaser = false;
    public LineRenderer mLineRenderer;
    public ParticleSystem mImpactEffect;
    public Light mImpackLight;

    [Header("UnitySetUp")]
    public string mEnemyTag = "Enemy";
    public Transform mRotaPart;
    public GameObject mBullet;
    public Transform mFirePosition;

    private Transform mTarget;
    private float mCurrentReload;
    private int mCurrentAmmo;
    private float mFireCountDown;
    private Enemy mTargetEnemy;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
        mCurrentReload = mReload;
        mCurrentAmmo = mAmmo;
        mFireCountDown = mFireRate;
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
            mTargetEnemy = mTarget.GetComponent<Enemy>();
        }
        else
        {
            mTarget = null;
        }
    }

    private void Update()
    {
        if(!mUseLaser)
        {
            //Reload and Weapon CoolDown
            if (mCurrentAmmo <= 0)
            {
               mCurrentReload -= Time.deltaTime;
            }
            if (mCurrentReload <= 0.0f)
            {
                Debug.Log("Ammo:" + mCurrentAmmo.ToString());
                mCurrentReload = mReload;
                mCurrentAmmo = mAmmo;
            }
            mFireCountDown -= Time.deltaTime;
        }
        
        //Check Target
        if (mTarget == null)
        {
            if(mUseLaser)
            {
                if(mLineRenderer.enabled)
                {
                    mLineRenderer.enabled = false;
                    mImpackLight.enabled = false;
                    mImpactEffect.Stop();
                }
            }
            return;
        }

        LockOnTarget();
        if(mUseLaser)
        {
            Laser();
        }
        else
        {
            if(mFireCountDown <= 0.0f && mCurrentAmmo > 0)
            {
                mCurrentAmmo--;
                Shoot();
                mFireCountDown = mFireRate;
            }
        }
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


    private void LockOnTarget()
    {
        //Target Lock on
        Vector3 dirction = mTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dirction);
        Vector3 rotation = Quaternion.Lerp(mRotaPart.rotation ,lookRotation,Time.deltaTime * mRotaSpeed).eulerAngles;
        mRotaPart.rotation = Quaternion.Euler(0.0f,rotation.y,0.0f);
    }

    private void Laser()
    {
        if(!mLineRenderer.enabled)
        {
            mLineRenderer.enabled = true;
            mImpactEffect.Play();
            mImpackLight.enabled = true;
        }
        mTargetEnemy.GetDamage(mLaserDamage * Time.deltaTime);
        mTargetEnemy.GetComponent<EnemyMovement>().Slow(mSlowPercent);
        
        mLineRenderer.SetPosition(0,mFirePosition.position);
        mLineRenderer.SetPosition(1,mTarget.position);
        Vector3 dir = mFirePosition.position - mTarget.position;

        mImpactEffect.transform.position = mTarget.position+ dir.normalized * 0.5f;
        mImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
}
