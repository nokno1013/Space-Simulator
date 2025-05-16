using UnityEngine;
using System.IO;

public class CSVManager : MonoBehaviour
{
    private string path;
    StreamWriter writer;

    void Start()
    {
        path = Application.persistentDataPath + "/orbiting_data.csv";
        writer = new StreamWriter(path, append: false);

        writer.WriteLine("중심각[°], 반지름[10E7km], 각속도[rad/s], 속도[10E7km/s]," +
            "주기[T], 진동수[f], 구심가속도[10E7km], 구심력[N], 좌표");
    }

    public void WriteData(float theta, float R, float w, Vector3 position)
    {
        float T = 2 * Mathf.PI / w;
        float v = R * w;

        string newLine = $"{CalDM(theta)}, {R}, {w}, {v}, " +
            $"{T}, {1f / T}, {R * w * w}, {v * v / R + " * 지구 질량"}, {position}";
        writer.WriteLine(newLine);
        writer.Flush();
    }

    private string CalDM(float theta)
    {
        float degrees = theta * Mathf.Rad2Deg;

        int d = Mathf.FloorToInt(degrees);
        float minutesFloat = (degrees - d) * 60f;
        int m = Mathf.FloorToInt(minutesFloat);

        return $"{d}°{m}'";
    }

    private void OnApplicationQuit()
    {
        writer.Close();
    }
}
