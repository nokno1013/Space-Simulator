using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartSettingManager : MonoBehaviour
{
    [SerializeField] TMP_InputField eInput;
    [SerializeField] TMP_InputField rInput;
    [SerializeField] Button GoToStartButton;
    [SerializeField] Transform StartTransform;
    [SerializeField] GameObject StartCanvas;
    [SerializeField] GameObject SettingCanvas; 

    Earth theEarth;
    MoveCamaera theMoveCamaera;

    void Start()
    {
        theEarth = FindAnyObjectByType<Earth>();
        theMoveCamaera = FindAnyObjectByType<MoveCamaera>();

        eInput.onSubmit.AddListener(OnEInputSubmit);
        rInput.onSubmit.AddListener(OnRInputSubmit);

        GoToStartButton.onClick.AddListener(OnGoToStartButtonClicked);
    }

    void Update()
    {
        if (!eInput.isFocused) eInput.text = $"{theEarth.e}";
        if (!rInput.isFocused) rInput.text = $"{theEarth.LongR}";
    }

    private void OnEInputSubmit(string input)
    {
        float result;
        if (float.TryParse(input, out result))
        {
            theEarth.e = result;
        }
        theEarth.StartSmulation();
    }

    private void OnRInputSubmit(string input)
    {
        float result;
        if (float.TryParse(input, out result))
        {
            theEarth.LongR = result;
        }
        theEarth.StartSmulation();
    }

    private void OnGoToStartButtonClicked()
    {
        StartCanvas.SetActive(true);
        SettingCanvas.SetActive(false);
        theMoveCamaera.GoToPosition(StartTransform);
    }
}
