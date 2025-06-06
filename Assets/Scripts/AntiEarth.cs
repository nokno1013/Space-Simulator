using Unity.VisualScripting;
using UnityEngine;

public class AntiEarth : MonoBehaviour
{
    [SerializeField] Transform Sun;

    Earth theEarth;

    float antiTheta;

    private void Start()
    {
        theEarth = FindAnyObjectByType<Earth>();
    }

    void Update()
    {
        antiTheta = theEarth.theta + Mathf.PI + 1.672f * theEarth.e * Mathf.Sin(theEarth.theta);

        Vector3 antiPos = new Vector3(theEarth.LongR * Mathf.Cos(antiTheta), 0, theEarth.ShortR * Mathf.Sin(antiTheta));

        transform.position = antiPos;
    }

    public float GetTheta()
    {
        return antiTheta;
    }

    public float GetR()
    {
        return Vector3.Distance(transform.position, Sun.position);
    }
}
