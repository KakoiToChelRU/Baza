using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FullscreenSwitcher : MonoBehaviour
{
    public TextMeshProUGUI fullscreenText; // Assign in the Inspector
    public Button leftArrowButton; // Assign in the Inspector
    public Button rightArrowButton; // Assign in the Inspector

    private FullScreenMode[] fullscreenModes = {
        FullScreenMode.ExclusiveFullScreen,
        FullScreenMode.FullScreenWindow,
        FullScreenMode.MaximizedWindow, //Maximised Window - Not Fullscreen
        FullScreenMode.Windowed
    };

    private int currentModeIndex = 1; // Start with FullScreenWindow (default)

    void Start()
    {
        if (fullscreenText == null)
        {
            Debug.LogError("FullscreenText is not assigned in the Inspector!");
            enabled = false;
            return;
        }

        if (leftArrowButton == null || rightArrowButton == null)
        {
            Debug.LogError("LeftArrowButton or RightArrowButton is not assigned in the Inspector!");
            enabled = false;
            return;
        }

        UpdateFullscreenMode(); // Set initial mode

        // Add listeners to the arrow buttons
        leftArrowButton.onClick.AddListener(OnLeftArrowClicked);
        rightArrowButton.onClick.AddListener(OnRightArrowClicked);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Сохраняем объект при смене сцены
    }

    public void OnLeftArrowClicked()
    {
        currentModeIndex--;
        if (currentModeIndex < 0)
        {
            currentModeIndex = fullscreenModes.Length - 1; // Wrap around to the end
        }
        UpdateFullscreenMode();
    }

    public void OnRightArrowClicked()
    {
        currentModeIndex++;
        if (currentModeIndex >= fullscreenModes.Length)
        {
            currentModeIndex = 0; // Wrap around to the beginning
        }
        UpdateFullscreenMode();
    }

    void UpdateFullscreenMode()
    {
        FullScreenMode mode = fullscreenModes[currentModeIndex];
        Screen.fullScreenMode = mode;
        fullscreenText.text = GetModeName(mode);
    }

    string GetModeName(FullScreenMode mode)
    {
        switch (mode)
        {
            case FullScreenMode.ExclusiveFullScreen: return "Exclusive Fullscreen";
            case FullScreenMode.FullScreenWindow: return "Fullscreen Window";
            case FullScreenMode.MaximizedWindow: return "Maximized Window";
            case FullScreenMode.Windowed: return "Windowed";
            default: return "Unknown";
        }
    }
}