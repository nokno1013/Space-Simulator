using UnityEngine;

public class MoveCamaera : MonoBehaviour
{
    [SerializeField] private Transform earth;
    [SerializeField] private Transform cam;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float lookSpeed = 2f;

    UIManager theUIManager;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private bool is_parent = false;

    void Start()
    {
        theUIManager = FindAnyObjectByType<UIManager>();

        Cursor.lockState = CursorLockMode.Locked;   //Ä¿¼­ ¼û±è
    }

    void Update()
    {
        CameraLook();
        CameraMove();

        if (Input.GetKeyUp(KeyCode.G)) SetFollow();
    }

    private void CameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        rotationY += mouseX;

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    private void CameraMove()
    {
        float undt = Time.unscaledDeltaTime;

        float moveX = 0;
        float moveZ = 0;
        float moveY = 0;

        if (Input.GetKey(KeyCode.W)) moveZ += moveSpeed * undt;
        if (Input.GetKey(KeyCode.A)) moveX -= moveSpeed * undt;
        if (Input.GetKey(KeyCode.S)) moveZ -= moveSpeed * undt;
        if (Input.GetKey(KeyCode.D)) moveX += moveSpeed * undt;

        if (Input.GetKey(KeyCode.Space)) moveY += moveSpeed * undt;
        if (Input.GetKey(KeyCode.LeftShift)) moveY -= moveSpeed * undt;

        Vector3 move = transform.right * moveX + transform.up * moveY + transform.forward * moveZ;
        transform.position += move;
    }
    
    private void SetFollow()
    {
        if (!is_parent)
        {
            cam.SetParent(earth);
            transform.position = earth.transform.position;
            is_parent = true;
            theUIManager.ShowFollowText(is_parent);
        }
        else
        {
            cam.SetParent(null);
            is_parent = false;
            theUIManager.ShowFollowText(is_parent);
        }
    }
}
