using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI InfoText;
    [SerializeField] TextMeshProUGUI OrbitText;
    [SerializeField] TextMeshProUGUI FollowText;
    [SerializeField] TextMeshProUGUI AreaText;

    public void ShowInfoText(float r, float v, float w, Vector3 position)
    {
        r *= 1000000000;
        v *= 1000000000;

        InfoText.text = $"�¾����κ����� �Ÿ�: {r}m\n" +
            $"�ӵ�: {v}m/s\n" +
            $"���ӵ�: {w}rad/s\n" +
            $"���ɰ��ӵ�: {v * w}m/s��\n" +
            $"���ɷ�: {v * v / r} * ���� ����N\n" +
            $"��ǥ: {position}";
    }

    public void ShowOrbitText(bool enabled)
    {
        OrbitText.text = $"�˵� ǥ��(����Ű F): {enabled}";
    }

    public void ShowFollowText(bool is_Parent)
    {
        FollowText.text = $"���� ���󰡱�(����Ű G): {is_Parent}";
    }

    public void ShowAreaText(float area1, float area2)
    {
        AreaText.text = "���� ����(����Ű R)\n" +
            "���� ����(����Ű T)\n" +
            $"��Ʈ�� ����: {area1}\n" +
            $"����� ����: {area2}\n" +
            $"������ ��: {Mathf.Abs(area1 - area2)}";
    }
}
