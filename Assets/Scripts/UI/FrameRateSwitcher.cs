using UnityEngine;
using TMPro; // Required for TextMeshPro
using UnityEngine.UI; // Required for Button

public class FrameRateSwitcher : MonoBehaviour
{
    public TextMeshProUGUI frameRateText; // Assign in the Inspector
    public int[] frameRates = { 30, 60, 120 };
    private int currentFrameRateIndex = 1; // Start at 60 FPS (middle option)

    public Button leftArrowButton; // Assign in the Inspector
    public Button rightArrowButton; // Assign in the Inspector

    void Start()
    {
        if (frameRateText == null)
        {
            Debug.LogError("FrameRateText is not assigned in the Inspector!");
            enabled = false; // Disable the script if the text is not assigned
            return;
        }

        if (leftArrowButton == null || rightArrowButton == null)
        {
            Debug.LogError("LeftArrowButton or RightArrowButton is not assigned in the Inspector!");
            enabled = false;
            return;
        }

        UpdateFrameRate();

        // Add listeners to the arrow buttons
        leftArrowButton.onClick.AddListener(OnLeftArrowClicked);
        rightArrowButton.onClick.AddListener(OnRightArrowClicked);
    }

    public void OnLeftArrowClicked()
    {
        currentFrameRateIndex--;
        if (currentFrameRateIndex < 0)
        {
            currentFrameRateIndex = frameRates.Length - 1; // Wrap around to the end
        }
        UpdateFrameRate();
    }

    public void OnRightArrowClicked()
    {
        currentFrameRateIndex++;
        if (currentFrameRateIndex >= frameRates.Length)
        {
            currentFrameRateIndex = 0; // Wrap around to the beginning
        }
        UpdateFrameRate();
    }

    void UpdateFrameRate()
    {
        int targetFrameRate = frameRates[currentFrameRateIndex];
        Application.targetFrameRate = targetFrameRate; // Set the game's target frame rate
        frameRateText.text = targetFrameRate + " FPS"; // Update the text display
    }
}