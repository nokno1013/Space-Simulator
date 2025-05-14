using System.Collections.Generic;
using UnityEngine;

public class OrbitMaker : MonoBehaviour
{
    [SerializeField] LineRenderer orbitLine;
    [SerializeField] Transform sun;
    [SerializeField] int Accuracy = 1;

    public float LongR = 149.6f; //��ݰ� (����: �鸸 km)
    public float ShortR;
    public float e = 0.016f;

    public List<Vector3> orbit = new List<Vector3>();
    public bool is_CalculateComplete = false;

    private int idx = 0;
    private int lastIdx = 0;
    private int points;

    private float dS;

    private void OnEnable()
    {
        ShortR = LongR * Mathf.Sqrt(1 - e * e); //�ܹݰ�
        points = Accuracy * 365;

        float S = Mathf.PI * LongR * ShortR;
        dS = S / points;
    }

    void Start()
    {
        orbit.Clear();
        CreateOrbit();

        transform.position = new Vector3(LongR, 0, 0);
    }

    void Update()
    {
        if (idx < points)
        {
            CalculateKepler2Low();
            Orbiting();
        }
        else
        {
            Destroy(gameObject);
            is_CalculateComplete = true;
            Debug.Log("Calculation Complete");
        }
    }

    void CreateOrbit()
    {
        orbitLine.loop = true;
        orbitLine.positionCount = points;

        for (int i = 0; i < points; i++)
        {
            float theta = (i / (float)points) * 2f * Mathf.PI; //���� ���
            float x = LongR * Mathf.Cos(theta);
            float z = ShortR * Mathf.Sin(theta);
            Vector3 point = new Vector3(x, 0, z);
            orbitLine.SetPosition(i, point);
        }
    }

    void Orbiting()
    {
        transform.position = orbitLine.GetPosition(idx);
        idx++;
    }

    void CalculateKepler2Low()
    {
        if (idx >= points) return;

        Vector3 p1 = transform.position;
        Vector3 p2 = orbitLine.GetPosition(lastIdx);

        float r1 = Vector3.Distance(sun.position, p1);
        float r2 = Vector3.Distance(sun.position, p2);
        Vector3 v1 = p1 - sun.position;
        Vector3 v2 = p2 - sun.position;

        float r_avg = (r1 + r2) / 2;    //�� �������� ��հ�
        float area = 0.5f * (r_avg * r_avg) * Vector3.Angle(v1, v2) * Mathf.Deg2Rad;    // 1/2 * ������^2 * �߽ɰ�(����)

        if(Mathf.Abs(dS - area) < 1f)
        {
            orbit.Add(p1);
            lastIdx += idx;
            Debug.Log("find p" + orbit.Count);
        }
    }
}
