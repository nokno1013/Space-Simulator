using UnityEngine;

public class Sun : MonoBehaviour
{
    OrbitMaker theOrbitMaker;

    void Start()
    {
        theOrbitMaker = FindAnyObjectByType<OrbitMaker>();

        float c = Mathf.Sqrt(theOrbitMaker.LongR * theOrbitMaker.LongR - theOrbitMaker.ShortR * theOrbitMaker.ShortR);

        transform.position = new Vector3(c, 0, 0);
    }
}
