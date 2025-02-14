using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public Transform[] mPoints;
    public int mCount = 0; 

    private void Awake()
    {
        mPoints = new Transform[transform.childCount];
        for(int i = 0; i < mPoints.Length; i++ )
        {
            mPoints[i] = transform.GetChild(i);
            mCount++;
        }
    }
}
