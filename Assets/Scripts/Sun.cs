using UnityEngine;

public class Sun : MonoBehaviour
{

    void Start()
    {
        float c = Mathf.Sqrt(Earth.LongR * Earth.LongR - Earth.ShortR * Earth.ShortR);

        transform.position = new Vector3(c, 0, 0);
    }

    void Update()
    {
        
    }
}
