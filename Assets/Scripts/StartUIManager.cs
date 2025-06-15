using UnityEngine;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour
{
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject SimulationCanvas;
    [SerializeField] GameObject SettingCanvas;
    [SerializeField] Button startButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button quitButton;
    [SerializeField] Transform simulationTransform;
    [SerializeField] Transform SettingTransform;

    [HideInInspector] public bool is_simulationStart = false;

    MoveCamaera theMoveCamaera;

    void Start()
    {
        theMoveCamaera = FindAnyObjectByType<MoveCamaera>();

        startButton.onClick.AddListener(OnStartButtonClicked);
        settingButton.onClick.AddListener(OnSettingButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        is_simulationStart = true;

        startCanvas.SetActive(false);
        SimulationCanvas.SetActive(true);

        theMoveCamaera.GoToPosition(simulationTransform);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnSettingButtonClicked()
    {
        startCanvas.SetActive(false);
        SettingCanvas.SetActive(true);

        theMoveCamaera.GoToPosition(SettingTransform);
    }

    private void OnQuitButtonClicked()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
