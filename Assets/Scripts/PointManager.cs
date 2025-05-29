using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] LineRenderer CLine;
    [SerializeField] LineRenderer ALine;
    [SerializeField] LineRenderer PLine;

    [SerializeField] LineRenderer p1Line;
    [SerializeField] LineRenderer p2Line;
    [SerializeField] LineRenderer q1Line;
    [SerializeField] LineRenderer q2Line;

    [SerializeField] Transform sun;

    Earth theEarth;
    UIManager theUIManager;

    void Start()
    {
        theEarth = FindAnyObjectByType<Earth>();
        theUIManager = FindAnyObjectByType<UIManager>();

        CLine.positionCount = 2;
        ALine.positionCount = 2;
        PLine.positionCount = 2;

        p1Line.positionCount = 2;
        p2Line.positionCount = 2;
        q1Line.positionCount = 2;
        q2Line.positionCount = 2;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F)) SwupLineEnable();
        if (Input.GetKeyUp(KeyCode.R)) SetArea();
    }

    public void SetCurrent(Vector3 position)
    {
        CLine.SetPosition(0, sun.position);
        CLine.SetPosition(1, position);
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
        CLine.enabled = !CLine.enabled;
        ALine.enabled = !ALine.enabled;
        PLine.enabled = !PLine.enabled;
        theEarth.orbitLine.enabled = !theEarth.orbitLine.enabled;

        theUIManager.ShowOrbitText(CLine.enabled);
    }

    private void SetArea()
    {

    }
}
