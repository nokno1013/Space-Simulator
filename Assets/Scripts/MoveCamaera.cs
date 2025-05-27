using UnityEngine;

public class MoveCamaera : MonoBehaviour
{
    [SerializeField] private Transform earth;
    [SerializeField] private Transform cam;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float lookSpeed = 2f;

    UIManager theUIManager;

    private float yaw = 0f;
    private float pitch = 0f;

    private bool is_parent = false;

    void Start()
    {
        theUIManager = FindAnyObjectByType<UIManager>();

        Cursor.lockState = CursorLockMode.Locked;   //커서 숨김
        Cursor.visible = false;     //커서 고정
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);  //최대 90도까지 제한

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += transform.forward;
        if (Input.GetKey(KeyCode.S)) move -= transform.forward;
        if (Input.GetKey(KeyCode.A)) move -= transform.right;
        if (Input.GetKey(KeyCode.D)) move += transform.right;
        if (Input.GetKey(KeyCode.Space)) move += transform.up;
        if (Input.GetKey(KeyCode.LeftShift)) move -= transform.up;

        if (Input.GetKeyUp(KeyCode.G)) SetFollow();

        transform.position += move.normalized * moveSpeed * Time.deltaTime;

        //if(Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
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
