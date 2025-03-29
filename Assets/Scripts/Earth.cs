using UnityEngine;
using System.Collections.Generic;

public class Earth : MonoBehaviour
{
    [SerializeField] Transform sun;
    [SerializeField] LineRenderer orbitLine;

    public static float LongR = 149.6f; //장반경 (단위: 백만 km)
    public static float ShortR;
    public float e = 0.9f;

    private List<Vector3> orbitPoints = new List<Vector3>();
    private int currentIndex = 0;
    private int points = 360;

    private float orbitSpeed;

    private float rotationSpeed = 530f;
    private float axialTilt = 23.5f;
    private float rotationAngle = 0f;

    private void OnEnable()
    {
        ShortR = LongR * Mathf.Sqrt(1 - e * e); //단반경
    }

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
        orbitLine.loop = true;
        orbitPoints.Clear();
        orbitLine.positionCount = points;

        for (int i = 0; i < points; i++)
        {
            float theta = (i / (float)points) * 2f * Mathf.PI; //각도 계산
            float x = LongR * Mathf.Cos(theta);
            float z = ShortR * Mathf.Sin(theta);
            Vector3 point = new Vector3(x, 0, z);
            orbitPoints.Add(point);
            orbitLine.SetPosition(i, point);
        }
    }

    void CalculateSpeed()
    {
        float perimeter = Mathf.PI * (3 * (LongR + ShortR) - Mathf.Sqrt((3 * LongR + ShortR) * (LongR + 3 * ShortR)));  //둘레 계산

        // 365초 동안 한 바퀴 돌도록 속도 계산
        orbitSpeed = perimeter / 365;
    }

    void MoveToOrbit()
    {
        if (orbitPoints.Count == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, orbitPoints[currentIndex], orbitSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, orbitPoints[currentIndex]) < 0.01f)
            currentIndex = (currentIndex + 1) % orbitPoints.Count;

        transform.LookAt(sun);
    }
    void RotateEarth()
    {
        rotationAngle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(axialTilt, rotationAngle, 0);
    }
}
