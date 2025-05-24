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

        writer.WriteLine("중심각[°], 반지름[10E+10m], 속도[10E+10m/s], 각속도[rad/s]," +
            "주기[s], 진동수[Hz], 구심가속도[10E+20m/s²], 구심력[10E+20N], 좌표, 반지름 * 속도");
    }

    public void WriteData(float theta, float r, float v, float w, Vector3 position)
    {
        float T = 2 * Mathf.PI / w;

        string newLine = $"{CalDM(theta)}, {r}, {v}, {w}, " +
            $"{T}, {1f / T}, {v * w}, {v * v / r + " * 지구 질량"}, {position}, {r*v}";
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
