using UnityEngine;
using System.Collections.Generic;

public class Earth : MonoBehaviour
{
    [SerializeField] Transform sun;
    [SerializeField] LineRenderer orbitLine;

    [SerializeField] int segments = 1;
    public float e = 0.0167f;
    public float LongR = 149.6f; //장반경 (단위: 백만 km)
    public float ShortR;

    private List<Vector3> orbitPoints = new List<Vector3>();

    private int currentIndex = 0;
    private int points;
    private float t = 0f;
    private float segmentTime = 1f;

    private void OnEnable()
    {
        points = segments * 365;
        ShortR = LongR * Mathf.Sqrt(1 - e * e); //단반경
    }

    void Start()
    {
        CreateOrbit();
        transform.position = orbitPoints[0];
    }

    void Update()
    {
        MoveWithKeplerLaw();
    }

    void CreateOrbit()
    {
        orbitPoints.Clear();
        orbitLine.loop = true;
        orbitLine.positionCount = points;

        for (int i = 0; i < points; i++)
        {
            float theta = (i / (float)points) * 2f * Mathf.PI;
            float x = LongR * Mathf.Cos(theta);
            float z = ShortR * Mathf.Sin(theta);
            Vector3 point = new Vector3(x, 0, z);
            orbitPoints.Add(point);
            orbitLine.SetPosition(i, point);
        }
    }

    void MoveWithKeplerLaw()
    {
        if (orbitPoints.Count == 0) return;

        Vector3 start = orbitPoints[currentIndex];
        Vector3 end = orbitPoints[(currentIndex + 1) % orbitPoints.Count];

        float R = Vector3.Distance(transform.position, sun.position);
        Debug.Log(R);
        float speed = 1f / R;
        speed *= 1000000f * Mathf.Pow(100, segments);

        float distance = Vector3.Distance(start, end);
        segmentTime = distance / speed;
        t += Time.deltaTime / segmentTime;

        transform.position = Vector3.Lerp(start, end, t);

        if (t >= 1f)
        {
            currentIndex = (currentIndex + 1) % orbitPoints.Count;
            t = 0f;
        }

        transform.LookAt(sun);
        //transform.Rotate(Vector3.up, 360f / 365f * Time.deltaTime); // 자전 (옵션)
    }
}
