using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] Transform sun;
    [SerializeField] LineRenderer orbitLine;

    [SerializeField] int softness = 1;

    [HideInInspector] public float ShortR;

    public float e = 0.0167f;
    public float LongR = 149.6f; // 장반경 (단위: 백만 km)

    CSVManager theCSVManager;

    private int segments = 365;

    private float theta = 0f;
    private float R = 0f;
    private float w = 0f;
    private Vector3 position = Vector3.zero;

    void Start()
    {
        theCSVManager = FindAnyObjectByType<CSVManager>();

        ShortR = LongR * Mathf.Sqrt(1 - e * e); // 단반경
        CreateOrbit();
        transform.position = GetEllipsePosition(theta);

        segments = softness * 365;
    }

    void Update()
    {
        Kepler2Law();
        theCSVManager.WriteData(theta, R, w, position);
    }

    void CreateOrbit()
    {
        orbitLine.loop = true;
        orbitLine.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = (i / (float)segments) * 2f * Mathf.PI;
            Vector3 point = GetEllipsePosition(angle);
            orbitLine.SetPosition(i, point);
        }
    }

    Vector3 GetEllipsePosition(float angle)
    {
        float x = LongR * Mathf.Cos(angle);
        float z = ShortR * Mathf.Sin(angle);
        return new Vector3(x, 0, z);
    }

    void Kepler2Law()
    {
        R = Vector3.Distance(transform.position, sun.position);
        w = 1f / R;
        w *= 100;

        theta += w * Time.deltaTime;
        if (theta > Mathf.PI * 2f)
        {
            theta -= Mathf.PI * 2f;
        }

        transform.position = GetEllipsePosition(theta);
        position = transform.position;

        transform.LookAt(sun);
    }
}
