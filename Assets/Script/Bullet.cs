using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mDamage = 2.0f;
    public float mSpeed = 70.0f;
    public GameObject mImpactEffect;
    public float mExplotionRadius = 0.0f;
    public string mEnemyTag = "Enemy";

    private Transform mBulletTarget;

    public void Seek(Transform bulletTarget)
    {
        mBulletTarget = bulletTarget;
    }

    private void Update()
    {
        if (mBulletTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = mBulletTarget.position - transform.position;
        float distenceThisFrame = mSpeed * Time.deltaTime;

        if (direction.magnitude <= distenceThisFrame)
        {
            HitTarget();
        }

        transform.Translate(direction.normalized * distenceThisFrame, Space.World);
        transform.LookAt(mBulletTarget);

    }

    private void HitTarget()
    {
        //Debug.Log("We hit");
        GameObject EffectIns = (GameObject)Instantiate(mImpactEffect, transform.position, transform.rotation);
        Destroy(EffectIns, 5.0f);

        if (mExplotionRadius > 0.0f)
        {
            Explode();
        }
        else
        {
            mBulletTarget.GetComponent<Enemy>().GetDamage(mDamage);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,mExplotionRadius);
        foreach(Collider collider in colliders)
        {
            //Debug.Log(collider.tag);
            if (collider.tag == mEnemyTag)
            {
                //Debug.Log("hit enemy");
                collider.GetComponent<Enemy>().GetDamage(mDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(mExplotionRadius > 0.0f)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, mExplotionRadius);
        }
    }
}
