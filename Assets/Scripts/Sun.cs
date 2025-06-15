using UnityEngine;

public class Sun : MonoBehaviour
{
    public void SetSun(float a, float b)
    {
        float c = Mathf.Sqrt(a * a - b * b);

        transform.position = new Vector3(c, 0, 0);
    }
}
