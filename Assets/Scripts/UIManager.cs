using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI InfoText;
    [SerializeField] TextMeshProUGUI OrbitText;
    [SerializeField] TextMeshProUGUI FollowText;

    public void ShowInfoText(float r, float v, float w, Vector3 position)
    {
        r *= 1000000000;
        v *= 1000000000;

        InfoText.text = $"태양으로부터의 거리: {r}m\n" +
            $"속도: {v}m/s\n" +
            $"각속도: {w}rad/s\n" +
            $"구심가속도: {v * w}m/s²\n" +
            $"구심력: {v * v / r} * 지구 질량N\n" +
            $"좌표: {position}";
    }

    public void ShowOrbitText(bool enabled)
    {
        OrbitText.text = $"궤도 표시(단축키 F): {enabled}";
    }

    public void ShowFollowText(bool is_Parent)
    {
        FollowText.text = $"지구 따라가기(단축키 G): {is_Parent}";
    }
}
