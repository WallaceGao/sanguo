using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public bool mDoMoverment = true;
    public float mPanSpeed = 25.0f;
    public float mPanBorderThickness = 5.0f;
    public float mScrollSpeed = 5.0f ;
    public float mMinY = 10.0f;
    public float mMaxY = 80.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            mDoMoverment = !mDoMoverment;

        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * mPanSpeed * Time.deltaTime,Space.World);
        else if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * mPanSpeed * Time.deltaTime, Space.World);
        else if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * mPanSpeed * Time.deltaTime, Space.World);
        else if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * mPanSpeed * Time.deltaTime, Space.World);
        else if (mDoMoverment && Input.mousePosition.y >= Screen.height - mPanBorderThickness)
            transform.Translate(Vector3.forward * mPanSpeed * Time.deltaTime, Space.World);
        else if (mDoMoverment && Input.mousePosition.y <= mPanBorderThickness)
            transform.Translate(Vector3.back * mPanSpeed * Time.deltaTime, Space.World);
        else if (mDoMoverment && Input.mousePosition.x <= mPanBorderThickness)
            transform.Translate(Vector3.left * mPanSpeed * Time.deltaTime, Space.World);
        else if (mDoMoverment && Input.mousePosition.x >= Screen.width - mPanBorderThickness)
            transform.Translate(Vector3.right * mPanSpeed * Time.deltaTime, Space.World);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 postion = transform.position;
        postion.y -= scroll * 1000 * mScrollSpeed * Time.deltaTime;
        postion.y = Mathf.Clamp(postion.y, mMinY, mMaxY);  // min and max
        transform.position = postion;
    }
}
