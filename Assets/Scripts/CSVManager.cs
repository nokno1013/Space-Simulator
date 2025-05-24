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

        writer.WriteLine("�߽ɰ�[��], ������[10E+10m], �ӵ�[10E+10m/s], ���ӵ�[rad/s]," +
            "�ֱ�[s], ������[Hz], ���ɰ��ӵ�[10E+20m/s��], ���ɷ�[10E+20N], ��ǥ, ������ * �ӵ�");
    }

    public void WriteData(float theta, float r, float v, float w, Vector3 position)
    {
        float T = 2 * Mathf.PI / w;

        string newLine = $"{CalDM(theta)}, {r}, {v}, {w}, " +
            $"{T}, {1f / T}, {v * w}, {v * v / r + " * ���� ����"}, {position}, {r*v}";
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
