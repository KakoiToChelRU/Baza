using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key; // The localization key
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    public void UpdateText()
    {
        if (CSVLocalizationManager.instance != null)
        {
            textComponent.text = CSVLocalizationManager.instance.GetText(key);
        }
        else
        {
            Debug.LogError("CSVLocalizationManager instance not found in the scene!");
        }
    }
}