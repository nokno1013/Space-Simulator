using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int Year = 0;
    public int Day = 0;
    public int Hour = 0;
    public int Minute = 0;
    public int Second = 0;

    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1)
        {
            Hour++;
            timer = 0;
        }

        if (Hour == 24)
        {
            Day++;
            Hour = 0;
        }
        if (Day == 365)
        {
            Year++;
            Day = 0;
        }
    }
}
