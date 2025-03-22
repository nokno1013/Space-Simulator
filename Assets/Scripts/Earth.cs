using UnityEngine;
using System.Collections.Generic;

public class Earth : MonoBehaviour
{
    [SerializeField] Transform sun;
    [SerializeField] LineRenderer orbitLine;

    private List<Vector3> orbitPoints = new List<Vector3>();
    private int currentIndex = 0;
    private int segments = 360;

    private float semiMajorAxis = 149.6f; // ������ (����: �鸸 km)
    private float e = 0.0167f;

    private float orbitSpeed;

    private float rotationSpeed = 530f;
    private float axialTilt = 23.5f;
    private float rotationAngle = 0f;

    void Start()
    {
        CreateOrbit();
        CalculateSpeed();
        transform.position = orbitPoints[0];
    }

    void Update()
    {
        MoveToOrbit();
        RotateEarth();
    }

    void CreateOrbit()
    {
        float semiMinorAxis = semiMajorAxis * Mathf.Sqrt(1 - e * e); // �ݴ��� ���
        orbitPoints.Clear();
        orbitLine.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float theta = (i / (float)segments) * 2f * Mathf.PI; // ���� ���
            float x = semiMajorAxis * Mathf.Cos(theta);
            float z = semiMinorAxis * Mathf.Sin(theta);
            Vector3 point = new Vector3(x, 0, z);
            orbitPoints.Add(point);
            orbitLine.SetPosition(i, point); // �˵� ���� �������� ����
        }
    }

    void CalculateSpeed()
    {
        // Ÿ���� �ѷ� �ٻ簪 ���
        float semiMinorAxis = semiMajorAxis * Mathf.Sqrt(1 - e * e);
        float perimeter = Mathf.PI * (3 * (semiMajorAxis + semiMinorAxis) - Mathf.Sqrt((3 * semiMajorAxis + semiMinorAxis) * (semiMajorAxis + 3 * semiMinorAxis)));

        // 365�� ���� �� ���� ������ �ӵ� ���
        orbitSpeed = perimeter / 365;
    }

    void MoveToOrbit()
    {
        if (orbitPoints.Count == 0) return;

        // ���� ��ġ���� ���� ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, orbitPoints[currentIndex], orbitSpeed * Time.deltaTime);

        // ��ǥ ������ �����ϸ� ���� �������� �̵�
        if (Vector3.Distance(transform.position, orbitPoints[currentIndex]) < 0.01f)
        {
            currentIndex = (currentIndex + 1) % orbitPoints.Count;
        }

        transform.LookAt(sun);
    }
    void RotateEarth()
    {
        rotationAngle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(axialTilt, rotationAngle, 0);
    }
}
