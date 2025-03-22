using UnityEngine;
using System.Collections.Generic;

public class Earth : MonoBehaviour
{
    [SerializeField] Transform sun;
    [SerializeField] LineRenderer orbitLine;

    private List<Vector3> orbitPoints = new List<Vector3>();
    private int currentIndex = 0;
    private int segments = 360;

    private float semiMajorAxis = 149.6f; // 반장축 (단위: 백만 km)
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
        float semiMinorAxis = semiMajorAxis * Mathf.Sqrt(1 - e * e); // 반단축 계산
        orbitPoints.Clear();
        orbitLine.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float theta = (i / (float)segments) * 2f * Mathf.PI; // 각도 계산
            float x = semiMajorAxis * Mathf.Cos(theta);
            float z = semiMinorAxis * Mathf.Sin(theta);
            Vector3 point = new Vector3(x, 0, z);
            orbitPoints.Add(point);
            orbitLine.SetPosition(i, point); // 궤도 라인 렌더러에 적용
        }
    }

    void CalculateSpeed()
    {
        // 타원의 둘레 근사값 계산
        float semiMinorAxis = semiMajorAxis * Mathf.Sqrt(1 - e * e);
        float perimeter = Mathf.PI * (3 * (semiMajorAxis + semiMinorAxis) - Mathf.Sqrt((3 * semiMajorAxis + semiMinorAxis) * (semiMajorAxis + 3 * semiMinorAxis)));

        // 365초 동안 한 바퀴 돌도록 속도 계산
        orbitSpeed = perimeter / 365;
    }

    void MoveToOrbit()
    {
        if (orbitPoints.Count == 0) return;

        // 현재 위치에서 다음 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, orbitPoints[currentIndex], orbitSpeed * Time.deltaTime);

        // 목표 지점에 도달하면 다음 지점으로 이동
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
