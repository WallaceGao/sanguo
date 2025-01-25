using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mSpeed = 70.0f;
    public GameObject mImpactEffect;
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


    }

    private void HitTarget()
    {
        Debug.Log("We hit");
        GameObject EffectIns = (GameObject)Instantiate(mImpactEffect, transform.position, transform.rotation);
        Destroy(EffectIns, 1.0f);
        Destroy(mBulletTarget.gameObject);
        Destroy(gameObject);
    }
}
