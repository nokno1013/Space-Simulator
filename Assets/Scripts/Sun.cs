using UnityEngine;

public class Sun : MonoBehaviour
{
    Earth theEarth;

    void Start()
    {
        theEarth = FindAnyObjectByType<Earth>();

        float c = Mathf.Sqrt(theEarth.LongR * theEarth.LongR - theEarth.ShortR * theEarth.ShortR);

        transform.position = new Vector3(c, 0, 0);
    }
}
