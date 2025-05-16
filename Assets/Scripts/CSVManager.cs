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

        writer.WriteLine("�߽ɰ�[��], ������[10E7km], ���ӵ�[rad/s], �ӵ�[10E7km/s]," +
            "�ֱ�[T], ������[f], ���ɰ��ӵ�[10E7km], ���ɷ�[N], ��ǥ");
    }

    public void WriteData(float theta, float R, float w, Vector3 position)
    {
        float T = 2 * Mathf.PI / w;
        float v = R * w;

        string newLine = $"{CalDM(theta)}, {R}, {w}, {v}, " +
            $"{T}, {1f / T}, {R * w * w}, {v * v / R + " * ���� ����"}, {position}";
        writer.WriteLine(newLine);
        writer.Flush();
    }

    private string CalDM(float theta)
    {
        float degrees = theta * Mathf.Rad2Deg;

        int d = Mathf.FloorToInt(degrees);
        float minutesFloat = (degrees - d) * 60f;
        int m = Mathf.FloorToInt(minutesFloat);

        return $"{d}��{m}'";
    }

    private void OnApplicationQuit()
    {
        writer.Close();
    }
}
