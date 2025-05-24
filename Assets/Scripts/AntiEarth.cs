using UnityEngine;

public class AntiEarth : MonoBehaviour
{
    [SerializeField] private Transform earth;
    [SerializeField] private Transform sun;
    [SerializeField] private LineRenderer area1;
    [SerializeField] private LineRenderer area2;

    Earth theEarth;

    float theta;

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
        Vector3 center = sun.position + new Vector3(theEarth.LongR * theEarth.e, 0, 0);
        Vector3 earthLocal = earth.position - center;

        float antiTheta = theEarth.theta + Mathf.PI; //반대편 각도

        Vector3 antiPos = new Vector3(theEarth.LongR * Mathf.Cos(antiTheta + Mathf.PI), 0, theEarth.ShortR * Mathf.Sin(antiTheta + Mathf.PI)) + center;
        

        transform.position = antiPos;
        area1.SetPosition(0, sunPos);
        area1.SetPosition(1, antiPos);
    }
}
