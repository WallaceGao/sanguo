using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public bool mDoMoverment = true;
    public float mPanSpeed = 25.0f;
    public float mPanBorderThickness = 5.0f;
    public float mScrollSpeed = 5.0f ;

    public float mMinX = -5f;
    public float mMaxX = 35f;
    public float mMinY = 10.0f;
    public float mMaxY = 30.0f;
    public float mMinZ = -10f;
    public float mMaxZ = 26f;

    public float mRotationSpeed = 5.0f;
    private Vector3 mLastMousePosition;
    private bool mIsRotating = false;


    private void Update()
    {
        if(!GameManager.mGameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                mDoMoverment = !mDoMoverment;

            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward * mPanSpeed * Time.deltaTime, Space.World);
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

            postion.x = Mathf.Clamp(postion.x, mMinX, mMaxX);
            postion.y = Mathf.Clamp(postion.y, mMinY, mMaxY);  // min and max
            postion.z = Mathf.Clamp(postion.z, mMinZ, mMaxZ);

            transform.position = postion;

            CameraRotation();
        }
    }

    private void CameraRotation()
    {
        if (Input.GetMouseButtonDown(2))  // 2 is middle buttom;
        {
            Debug.Log("Camera Start Rotation");
            mIsRotating = true;
            mLastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(2)) 
        {
            mIsRotating = false;
        }

        if (mIsRotating)
        {
            Vector3 deltaMouse = Input.mousePosition - mLastMousePosition;
            float rotateX = deltaMouse.y * mRotationSpeed * Time.deltaTime;
            float rotateY = -deltaMouse.x * mRotationSpeed * Time.deltaTime;


            transform.Rotate(Vector3.right, rotateX);
            transform.Rotate(Vector3.up, rotateY, Space.World);

            mLastMousePosition = Input.mousePosition; 
        }
    }
}
