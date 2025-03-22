using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public DateTime GameTime = new DateTime(1, 1, 1, 0, 0, 0); //1³â 1¿ù 1ÀÏ 00:00:00
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            GameTime = GameTime.AddSeconds((int)timer);
            timer %= 1f;
        }
    }
}
