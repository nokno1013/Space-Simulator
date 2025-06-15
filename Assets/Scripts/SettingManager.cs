using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject SimulationCanvas;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] Slider moveSpeedSlider;
    [SerializeField] Slider lookSpeedSlider;
    [SerializeField] Slider earthSizeSlider;
    [SerializeField] Slider orbitSizeSlider;
    [SerializeField] Button quitButton;
    [SerializeField] private Transform startTransform;

    MoveCamaera theMoveCamaera;
    Earth theEarth;
    PointManager thePointManager;
    StartUIManager theStartUIManager;
    SpeedManager theSpeedManager;

    [HideInInspector] public bool is_SettingOpen = false;

    private float constMoveSpeed;
    private float constLookSpeed;

    private void Start()
    {
        theMoveCamaera = FindAnyObjectByType<MoveCamaera>();
        theEarth = FindAnyObjectByType<Earth>();
        thePointManager = FindAnyObjectByType<PointManager>();
        theStartUIManager = FindAnyObjectByType<StartUIManager>();
        theSpeedManager = FindAnyObjectByType<SpeedManager>();

        constMoveSpeed = theMoveCamaera.moveSpeed;
        constLookSpeed = theMoveCamaera.lookSpeed;

        moveSpeedSlider.onValueChanged.AddListener(OnMoveSpeedChanged);
        lookSpeedSlider.onValueChanged.AddListener(OnLookSpeedChanged);
        earthSizeSlider.onValueChanged.AddListener(OnEarthSizeChanged);
        orbitSizeSlider.onValueChanged.AddListener(OnOrbitSizeChanged);

        quitButton.onClick.AddListener(OnQuitClicked);
    }

    void Update()
    {
        if (!theStartUIManager.is_simulationStart) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            is_SettingOpen = !is_SettingOpen;
            settingsPanel.SetActive(is_SettingOpen);

            Cursor.lockState = is_SettingOpen ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void OnMoveSpeedChanged(float velue)
    {
        theMoveCamaera.moveSpeed = constMoveSpeed * velue;
    }

    private void OnLookSpeedChanged(float velue)
    {
        theMoveCamaera.lookSpeed = constLookSpeed * velue;
    }

    private void OnEarthSizeChanged(float velue)
    {
        theEarth.SetSize(velue);
    }

    private void OnOrbitSizeChanged(float velue)
    {
        thePointManager.SetSize(velue * 2);
    }

    private void OnQuitClicked()
    {
        theStartUIManager.is_simulationStart = false;
        is_SettingOpen = false;
        settingsPanel.SetActive(is_SettingOpen);

        startCanvas.SetActive(true);
        SimulationCanvas.SetActive(false);

        Time.timeScale = 1f;
        theSpeedManager.currentSpeed = 1f;

        theMoveCamaera.GoToPosition(startTransform);
    }
}
