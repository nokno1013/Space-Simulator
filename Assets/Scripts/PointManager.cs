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
    UIManager theUIManager;

    private bool is_setted = false;

    void Start()
    {
        theEarth = FindAnyObjectByType<Earth>();
        theUIManager = FindAnyObjectByType<UIManager>();

        EarthLine.positionCount = 2;
        AntiEarthLine.positionCount = 2;

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
        AntiEarthLine.SetPosition(0, sun.position);

        p1Line.SetPosition(0, sun.position);
        p2Line.SetPosition(0, sun.position);
        q1Line.SetPosition(0, sun.position);
        q2Line.SetPosition(0, sun.position);

        EarthLine.SetPosition(1, Earth.position);
        AntiEarthLine.SetPosition(1, AntiEarth.position);
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
        if (is_setted)
        {
            p2Line.SetPosition(1, Earth.position);
            q2Line.SetPosition(1, AntiEarth.position);

            p2Line.enabled = true;
            q2Line.enabled = true;
        }
        else
        {
            p1Line.SetPosition(1, Earth.position);
            q1Line.SetPosition(1, AntiEarth.position);

            p1Line.enabled = true;
            q1Line.enabled = true;

            is_setted = true;
        }
    }

    private void DeleteArea()
    {
        p1Line.enabled = false;
        p2Line.enabled = false;
        q1Line.enabled = false;
        q2Line.enabled = false;

        is_setted = false;
    }
}
