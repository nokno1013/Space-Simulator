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

    float p1Theta;
    float q1Theta;
    float p2Theta;
    float q2Theta;

    float p1R;
    float q1R;
    float p2R;
    float q2R;

    float area1 = 0;
    float area2 = 0;

    private int seted_idx = 0;

    void Start()
    {
        theEarth = FindAnyObjectByType<Earth>();
        theUIManager = FindAnyObjectByType<UIManager>();

        EarthLine.positionCount = 2;

        ALine.positionCount = 2;
        PLine.positionCount = 2;

        p1Line.positionCount = 2;
        p2Line.positionCount = 2;
        q1Line.positionCount = 2;
        q2Line.positionCount = 2;

        DeleteArea();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F)) SwupLineEnable();
        if (Input.GetKeyUp(KeyCode.R)) SetArea();
        if (Input.GetKeyUp(KeyCode.T)) DeleteArea();

        if (seted_idx == 3) CalculateArea();

        theUIManager.ShowAreaText(area1, area2);

        EarthLine.SetPosition(0, sun.position);

        p1Line.SetPosition(0, sun.position);
        p2Line.SetPosition(0, sun.position);
        q1Line.SetPosition(0, sun.position);
        q2Line.SetPosition(0, sun.position);

        EarthLine.SetPosition(1, Earth.position);
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
            p1Theta = theEarth.GetTheta();
            p1R = theEarth.GetR();

            p1Line.SetPosition(1, Earth.position);
            p1Line.enabled = true;

            seted_idx++;
        }
        else if (seted_idx == 1)
        {
            p2Theta = theEarth.GetTheta();
            p2R = theEarth.GetR();

            float area1Theta = Mathf.Abs(p1Theta - p2Theta);
            float area1R = (p1R + p2R) / 2;
            area1 = 0.5f * area1Theta * area1R * area1R;

            p2Line.SetPosition(1, Earth.position);
            p2Line.enabled = true;

            seted_idx++;
        }
        else if (seted_idx == 2)
        {
            q1Theta = theEarth.GetTheta();
            q1R = theEarth.GetR();

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
        seted_idx = 0;
    }

    private void CalculateArea()
    {
        q2Theta = theEarth.GetTheta();
        q2R = theEarth.GetR();

        float area2Theta = Mathf.Abs(q1Theta - q2Theta);
        float area2R = (q1R + q2R) / 2;
        area2 = 0.5f * area2Theta * area2R * area2R;

        if (Mathf.Abs(area1 - area2) <= 10)
        {
            q2Line.SetPosition(1, Earth.position);
            q2Line.enabled = true;

            seted_idx++;
        }
    }
}
