using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] Transform sun;
    [SerializeField] Transform Earth;

    [SerializeField] LineRenderer EarthLine;

    [SerializeField] LineRenderer ALine;
    [SerializeField] LineRenderer PLine;

    [SerializeField] LineRenderer p1Line;
    [SerializeField] LineRenderer p2Line;
    [SerializeField] LineRenderer q1Line;
    [SerializeField] LineRenderer q2Line;

    Earth theEarth;
    UIManager theUIManager;
    SettingManager theSettingManager;

    float area1Time = 0;
    float area2Time = 0;

    float area1 = 0;
    float area2 = 0;

    private int seted_idx = 0;

    void Start()
    {
        theEarth = FindAnyObjectByType<Earth>();
        theUIManager = FindAnyObjectByType<UIManager>();
        theSettingManager = FindAnyObjectByType<SettingManager>();

        EarthLine.positionCount = 2;

        ALine.positionCount = 2;
        PLine.positionCount = 2;

        p1Line.positionCount = 2;
        p2Line.positionCount = 2;
        q1Line.positionCount = 2;
        q2Line.positionCount = 2;

        DeleteArea();
    }

    private void FixedUpdate()
    {
        EarthLine.SetPosition(0, sun.position);
        p1Line.SetPosition(0, sun.position);
        p2Line.SetPosition(0, sun.position);
        q1Line.SetPosition(0, sun.position);
        q2Line.SetPosition(0, sun.position);
    }

    private void Update()
    {
        if (!theSettingManager.is_SettingOpen)
        {
            if (Input.GetKeyUp(KeyCode.F)) SwupLineEnable();
            if (Input.GetKeyUp(KeyCode.R)) SetArea();
            if (Input.GetKeyUp(KeyCode.T)) DeleteArea();
        }

        if (seted_idx == 1)
        {
            area1 += theEarth.GetR() * Time.deltaTime;
            area1Time += Time.deltaTime;
        }
        if (seted_idx == 3)
        {
            CalculateArea();
            area2 += theEarth.GetR() * Time.deltaTime;
            area2Time += Time.deltaTime;
        }

        EarthLine.SetPosition(1, Earth.position);
        theUIManager.ShowAreaText(area1, area2);
    }

    public void SetAphelion(Vector3 position)
    {
        ALine.SetPosition(0, sun.position);
        ALine.SetPosition(1, position);
    }

    public void SetPerihelion(Vector3 position)
    {
        PLine.SetPosition(0, sun.position);
        PLine.SetPosition(1, position);
    }

    public void SetSize(float size)
    {
        theEarth.orbitLine.startWidth = size;
        theEarth.orbitLine.endWidth = size;

        EarthLine.startWidth = size;
        EarthLine.endWidth = size;

        ALine.startWidth = size;
        ALine.endWidth = size;

        PLine.startWidth = size;
        PLine.endWidth = size;

        p1Line.startWidth = size;
        p1Line.endWidth = size;

        p2Line.startWidth = size;
        p2Line.endWidth = size;

        q1Line.startWidth = size;
        q1Line.endWidth = size;

        q2Line.startWidth = size;
        q2Line.endWidth = size;
    }

    private void SwupLineEnable()
    {
        EarthLine.enabled = !EarthLine.enabled;

        ALine.enabled = !ALine.enabled;
        PLine.enabled = !PLine.enabled;
        theEarth.orbitLine.enabled = !theEarth.orbitLine.enabled;

        theUIManager.ShowOrbitText(EarthLine.enabled);
    }

    private void SetArea()
    {
        if (seted_idx == 0)
        {
            p1Line.SetPosition(1, Earth.position);
            p1Line.enabled = true;

            seted_idx++;
        }
        else if (seted_idx == 1)
        {
            p2Line.SetPosition(1, Earth.position);
            p2Line.enabled = true;

            seted_idx++;
        }
        else if (seted_idx == 2)
        {
            q1Line.SetPosition(1, Earth.position);
            q1Line.enabled = true;

            seted_idx++;
        }
    }

    private void DeleteArea()
    {
        p1Line.enabled = false;
        p2Line.enabled = false;
        q1Line.enabled = false;
        q2Line.enabled = false;

        area1 = 0;
        area2 = 0;
        area1Time = 0;
        area2Time = 0;
        seted_idx = 0;
    }

    private void CalculateArea()
    {
        if (Mathf.Abs(area1Time - area2Time) < 0.005f)
        {
            q2Line.SetPosition(1, Earth.position);
            q2Line.enabled = true;

            seted_idx++;
        }
    }
}
