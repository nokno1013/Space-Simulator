using UnityEngine;
using System.Collections;

public class MoveCamaera : MonoBehaviour
{
    [SerializeField] private Transform earth;
    [SerializeField] private Transform cam;

    UIManager theUIManager;
    SettingManager theSettingManager;
    StartUIManager theStartUIManager;

    [HideInInspector] public float moveSpeed = 100f;
    [HideInInspector] public float lookSpeed = 2f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private float duration = 2f;

    private bool is_parent = false;

    void Start()
    {
        theUIManager = FindAnyObjectByType<UIManager>();
        theSettingManager = FindAnyObjectByType<SettingManager>();
        theStartUIManager = FindAnyObjectByType<StartUIManager>();
    }

    void Update()
    {
        if (!theStartUIManager.is_simulationStart) return;

        if (!theSettingManager.is_SettingOpen)
        {
            CameraLook();
            CameraMove();

            if (Input.GetKeyUp(KeyCode.G)) SetFollow();
        }
    }

    public void GoToPosition(Transform targetTransform)
    {
        StartCoroutine(MoveCoroutine(targetTransform));
    }

    private IEnumerator MoveCoroutine(Transform targetTransform)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        Vector3 endPos = targetTransform.position;
        Quaternion endRot = targetTransform.rotation;

        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;
    }

    private void CameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -160f, 20f);

        rotationY += mouseX;

        transform.localRotation = Quaternion.Euler(rotationX + 70, rotationY, 0f);
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
