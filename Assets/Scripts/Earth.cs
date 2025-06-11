using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] Transform sun;
    public LineRenderer orbitLine;

    [SerializeField] int softness = 1;
    public float e = 0.0167f;

    [HideInInspector] public float LongR = 149.6f; // 장반경 (단위: 백만 km)
    [HideInInspector] public float ShortR;
    [HideInInspector] public float theta = 0f;

    CSVManager theCSVManager;
    PointManager thePointManager;
    UIManager theUIManager;

    private int segments = 365;

    private float r = 0f;
    private float v = 0f;
    private float w = 0f;
    private Vector3 position = Vector3.zero;

    private float aphelion = 0f;     //원일점
    private float perihelion = 100000f;     //근일점


    void Start()
    {
        theCSVManager = FindAnyObjectByType<CSVManager>();
        thePointManager = FindAnyObjectByType<PointManager>();
        theUIManager = FindAnyObjectByType<UIManager>();

        ShortR = LongR * Mathf.Sqrt(1 - e * e); //단반경
        CreateOrbit();
        transform.position = GetPosition(theta);

        segments = softness * 365;
    }

    void Update()
    {
        Kepler2Law();

        if (r > aphelion)
        {
            aphelion = r;
            thePointManager.SetAphelion(position);
        }
        if (r < perihelion)
        {
            perihelion = r;
            thePointManager.SetPerihelion(position);
        }

        //theCSVManager.WriteData(theta, r, v, w, position);
        //theUIManager.ShowInfoText(r, v, w, position);
    }

    public float GetTheta()
    {
        return theta;
    }

    public float GetR()
    {
        return r;
    }

    private void CreateOrbit()
    {
        orbitLine.loop = true;
        orbitLine.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = (i / (float)segments) * 2f * Mathf.PI;
            Vector3 point = GetPosition(angle);
            orbitLine.SetPosition(i, point);
        }
    }

    private Vector3 GetPosition(float theta)
    {
        float x = LongR * Mathf.Cos(theta);
        float z = ShortR * Mathf.Sin(theta);
        return new Vector3(x, 0, z);
    }

    private void Kepler2Law()
    {
        r = Vector3.Distance(transform.position, sun.position);
        v = 1f / r;

        w = v / r;
        w *= 10000;

        theta += w * Time.deltaTime;
        if (theta > Mathf.PI * 2f)
        {
            theta -= Mathf.PI * 2f;
        }

        transform.position = GetPosition(theta);
        position = transform.position;

        transform.LookAt(sun);
    }
}
