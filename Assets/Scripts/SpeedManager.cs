using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{
    [SerializeField] Image SpeedController;
    [SerializeField] Sprite PlayedImage;
    [SerializeField] Sprite PausedImage;

    UIManager theUIManager;

    bool is_paused = false;
    private float currentSpeed = 1.0f;

    void Start()
    {
        theUIManager = FindAnyObjectByType<UIManager>();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Z)) AddTimeScale(-1f);
        if(Input.GetKeyUp(KeyCode.X)) AddTimeScale(-0.1f);
        if(Input.GetKeyUp(KeyCode.C)) Pause();
        if(Input.GetKeyUp(KeyCode.V)) AddTimeScale(0.1f);
        if(Input.GetKeyUp(KeyCode.B)) AddTimeScale(1f);

        theUIManager.ShowSpeedText(currentSpeed);
    }


    private void AddTimeScale(float scale)
    {
        if(currentSpeed + scale < 0)
        {
            Time.timeScale = 0;
            currentSpeed = 0;

            return;
        }

        if(!is_paused)
            Time.timeScale += scale;
        currentSpeed += scale;
    }

    private void Pause()
    {
        if (Time.timeScale == 0f && is_paused)
        {
            SpeedController.sprite = PlayedImage;
            is_paused = false;
            Time.timeScale = currentSpeed;
        }
        else
        {
            SpeedController.sprite = PausedImage;
            is_paused = true;
            Time.timeScale = 0f;
        }
    }

}
