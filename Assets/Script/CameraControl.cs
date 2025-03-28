using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public bool mDoMovement = true;
    public float mPanSpeed = 25.0f;
    public float mPanBorderThickness = 5.0f;
    public float mScrollSpeed = 5.0f ;

    public float mMinX = -5f;
    public float mMaxX = 35f;
    public float mMinY = 10.0f;
    public float mMaxY = 30.0f;
    public float mMinZ = -10f;
    public float mMaxZ = 26f;

    public float mRotationSpeed = 10.0f;
    public float mMinRotateX = 0;
    public float mMaxRotateX = 70f;
    public float mMinRotateY = -75;
    public float mMaxRotateY = 75;
    private Vector3 mLastMousePosition;
    private bool mIsRotating = false;



    private void Update()
    {
        if(!GameManager.mGameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                mDoMovement = !mDoMovement;

            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward * mPanSpeed * Time.deltaTime, Space.World);
            else if (Input.GetKey(KeyCode.S))
                transform.Translate(Vector3.back * mPanSpeed * Time.deltaTime, Space.World);
            else if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector3.left * mPanSpeed * Time.deltaTime, Space.World);
            else if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector3.right * mPanSpeed * Time.deltaTime, Space.World);
            else if (mDoMovement && Input.mousePosition.y >= Screen.height - mPanBorderThickness)
                transform.Translate(Vector3.forward * mPanSpeed * Time.deltaTime, Space.World);
            else if (mDoMovement && Input.mousePosition.y <= mPanBorderThickness)
                transform.Translate(Vector3.back * mPanSpeed * Time.deltaTime, Space.World);
            else if (mDoMovement && Input.mousePosition.x <= mPanBorderThickness)
                transform.Translate(Vector3.left * mPanSpeed * Time.deltaTime, Space.World);
            else if (mDoMovement && Input.mousePosition.x >= Screen.width - mPanBorderThickness)
                transform.Translate(Vector3.right * mPanSpeed * Time.deltaTime, Space.World);

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Vector3 position = transform.position;
            position.y -= scroll * 1000 * mScrollSpeed * Time.deltaTime;

            position.x = Mathf.Clamp(position.x, mMinX, mMaxX);
            position.y = Mathf.Clamp(position.y, mMinY, mMaxY);  // min and max
            position.z = Mathf.Clamp(position.z, mMinZ, mMaxZ);

            transform.position = position;

            CameraRotation();
        }
    }

    private void CameraRotation()
    {
        if (Input.GetMouseButtonDown(2))  // 2 is middle bottom;
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


            // eulerAngles is Yaw-Pitch-Roll, it will make sure the eulerAngles is 
            Vector3 currentRotation = transform.eulerAngles;

            // make sure if it's 270, it will be 270- 360 = -90
            if (currentRotation.x > 180)
                currentRotation.x -= 360;
            currentRotation.x = Mathf.Clamp(currentRotation.x + rotateX, mMinRotateX, mMaxRotateX);

            if (currentRotation.y > 180)
                currentRotation.y -= 360;
            currentRotation.y = Mathf.Clamp(currentRotation.y + rotateY, mMinRotateY, mMaxRotateY);

            // Set new new eulerAngle 
            transform.eulerAngles = currentRotation;

            mLastMousePosition = Input.mousePosition;
        }
    }
}
