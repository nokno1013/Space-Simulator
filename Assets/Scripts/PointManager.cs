using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] LineRenderer CLine;
    [SerializeField] LineRenderer ALine;
    [SerializeField] LineRenderer PLine;

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
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            CLine.enabled = !CLine.enabled;
            ALine.enabled = !ALine.enabled;
            PLine.enabled = !PLine.enabled;
            theEarth.orbitLine.enabled = !theEarth.orbitLine.enabled;

            theUIManager.ShowOrbitText(CLine.enabled);
        }
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
}
