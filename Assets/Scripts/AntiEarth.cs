using UnityEngine;

public class AntiEarth : MonoBehaviour
{
    [SerializeField] private Transform earth;
    [SerializeField] private Transform sun;
    [SerializeField] private LineRenderer area1;
    [SerializeField] private LineRenderer area2;

    Earth theEarth;

    private void Start()
    {
        theEarth = FindAnyObjectByType<Earth>();

        area1.positionCount = 2;
        area1.useWorldSpace = true;

        area2.positionCount = 2;
        area2.useWorldSpace = true;
    }

    void Update()
    {
        Vector3 sunPos = sun.position;

        float antiTheta = theEarth.theta + Mathf.PI + 1.672f * theEarth.e * Mathf.Sin(theEarth.theta);

        Vector3 antiPos = new Vector3(theEarth.LongR * Mathf.Cos(antiTheta), 0, theEarth.ShortR * Mathf.Sin(antiTheta));

        transform.position = antiPos;
        area1.SetPosition(0, sunPos);
        area1.SetPosition(1, antiPos);
    }
}
