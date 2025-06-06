using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] Transform sun;
    [SerializeField] Transform Earth;
    [SerializeField] Transform AntiEarth;

    [SerializeField] LineRenderer EarthLine;
    [SerializeField] LineRenderer AntiEarthLine;

    [SerializeField] LineRenderer ALine;
    [SerializeField] LineRenderer PLine;

    [SerializeField] LineRenderer p1Line;
    [SerializeField] LineRenderer p2Line;
    [SerializeField] LineRenderer q1Line;
    [SerializeField] LineRenderer q2Line;

    Earth theEarth;
    AntiEarth theAntiEarth;
    UIManager theUIManager;

    float p1Theta;
    float q1Theta;
    float p2Theta;
    float q2Theta;

    private int seted_idx = 0;

    void Start()
    {
        theEarth = FindAnyObjectByType<Earth>();
        theAntiEarth = FindAnyObjectByType<AntiEarth>();
        theUIManager = FindAnyObjectByType<UIManager>();

        EarthLine.positionCount = 2;
        //AntiEarthLine.positionCount = 2;

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

        EarthLine.SetPosition(0, sun.position);
        //AntiEarthLine.SetPosition(0, sun.position);

        p1Line.SetPosition(0, sun.position);
        p2Line.SetPosition(0, sun.position);
        q1Line.SetPosition(0, sun.position);
        q2Line.SetPosition(0, sun.position);

        EarthLine.SetPosition(1, Earth.position);
        //AntiEarthLine.SetPosition(1, AntiEarth.position);
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
        AntiEarthLine.enabled |= AntiEarthLine.enabled;

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
            q1Theta = theAntiEarth.GetTheta();

            p1Line.SetPosition(1, Earth.position);
            q1Line.SetPosition(1, AntiEarth.position);

            p1Line.enabled = true;
            q1Line.enabled = true;

            seted_idx++;
        }
        else if(seted_idx == 1)
        {
            p2Theta = theEarth.GetTheta();
            q2Theta = theAntiEarth.GetTheta();

            float area1Theta = Mathf.Abs(p1Theta - p2Theta);
            float area2Theta = Mathf.Abs(q1Theta - q2Theta);

            float area1R = theEarth.GetR();
            float area2R = theAntiEarth.GetR();

            float area1 = 0.5f * area1R * area1R * area1Theta;
            float area2 = 0.5f * area2R * area2R * area2Theta;

            p2Line.SetPosition(1, Earth.position);
            q2Line.SetPosition(1, AntiEarth.position);

            theUIManager.ShowAreaText(area1, area2);

            p2Line.enabled = true;
            q2Line.enabled = true;

            seted_idx++;
        }
    }

    private void DeleteArea()
    {
        p1Line.enabled = false;
        p2Line.enabled = false;
        q1Line.enabled = false;
        q2Line.enabled = false;

        seted_idx = 0;
    }
}
