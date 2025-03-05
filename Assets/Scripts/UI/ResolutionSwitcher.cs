using UnityEngine;
using TMPro; // Required for TextMeshPro
using UnityEngine.UI; // Required for Button

public class ResolutionSwitcher : MonoBehaviour
{
    public TextMeshProUGUI resolutionText; // Assign in the Inspector
    public Button leftArrowButton; // Assign in the Inspector
    public Button rightArrowButton; // Assign in the Inspector

    // Array of resolutions to cycle through
    private Resolution[] resolutions = {
        new Resolution { width = 800, height = 600 },
        new Resolution { width = 1280, height = 800 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 2560, height = 1440 }
    };

    private int currentResolutionIndex = 3; // Start at 1920x1080 (default)

    void Start()
    {
        if (resolutionText == null)
        {
            Debug.LogError("ResolutionText is not assigned in the Inspector!");
            enabled = false;
            return;
        }

        if (leftArrowButton == null || rightArrowButton == null)
        {
            Debug.LogError("LeftArrowButton or RightArrowButton is not assigned in the Inspector!");
            enabled = false;
            return;
        }

        UpdateResolution(); // Set initial resolution

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
        currentResolutionIndex--;
        if (currentResolutionIndex < 0)
        {
            currentResolutionIndex = resolutions.Length - 1; // Wrap around to the end
        }
        UpdateResolution();
    }

    public void OnRightArrowClicked()
    {
        currentResolutionIndex++;
        if (currentResolutionIndex >= resolutions.Length)
        {
            currentResolutionIndex = 0; // Wrap around to the beginning
        }
        UpdateResolution();
    }

    void UpdateResolution()
    {
        Resolution resolution = resolutions[currentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); // Set resolution
        resolutionText.text = resolution.width + "x" + resolution.height; // Update text
    }
}