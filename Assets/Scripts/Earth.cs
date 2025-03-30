using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] Transform sun;

    private float orbitSpeed;

    private float rotationSpeed = 530f;
    private float axialTilt = 23.5f;
    private float rotationAngle = 0f;

    private int idx = 0;

    OrbitMaker theOrbitMaker;

    void Start()
    {
        theOrbitMaker = FindAnyObjectByType<OrbitMaker>();

        CalculateSpeed();
    }

    void Update()
    {
        if (theOrbitMaker.is_CalculateComplete)
        {
            MoveToOrbit();
            RotateEarth();
        }
    }

    void CalculateSpeed()
    {
        float perimeter = Mathf.PI * (3 * (theOrbitMaker.LongR + theOrbitMaker.ShortR) - Mathf.Sqrt((3 * theOrbitMaker.LongR + theOrbitMaker.ShortR) * (theOrbitMaker.LongR + 3 * theOrbitMaker.ShortR)));  //�ѷ� ���

        // 365�� ���� �� ���� ������ �ӵ� ���
        orbitSpeed = perimeter / 365;
    }

    void MoveToOrbit()
    {
        if (theOrbitMaker.orbit.Count == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, theOrbitMaker.orbit[idx], orbitSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, theOrbitMaker.orbit[idx]) < 0.01f)
            idx++;

        transform.LookAt(sun);
    }

    void RotateEarth()
    {
        rotationAngle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(axialTilt, rotationAngle, 0);
    }
}
