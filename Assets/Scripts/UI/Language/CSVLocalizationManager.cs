using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Add this for ToArray()
using TMPro;

public class CSVLocalizationManager : MonoBehaviour
{
    public static CSVLocalizationManager instance;
    public TextAsset csvFile; // Drag your CSV file here in the Inspector
    public string[] availableLanguages = { "Russian", "English" }; // Add all supported languages here
    public int currentLanguageIndex = 0; // Index of the current language in the array
    private Dictionary<string, Dictionary<string, string>> localizationData = new Dictionary<string, Dictionary<string, string>>();

    public TextMeshProUGUI languageText; // Reference to the Text UI element displaying the current language
    private string currentLanguage; // Cache the current language string

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadCSV();
        LoadLanguage();  // Load the saved language from PlayerPrefs on start
        UpdateLanguageText();
    }

    void LoadCSV()
    {
        localizationData.Clear();

        string[] lines = csvFile.text.Split('\n');
        if (lines.Length <= 1)
        {
            Debug.LogError("CSV file is empty or invalid.");
            return;
        }

        string[] languages = lines[0].Split(',');
        if (languages.Length - 1 != availableLanguages.Length)
        {
            Debug.LogWarning("Number of languages in CSV (" + (languages.Length - 1) + ") does not match availableLanguages array (" + availableLanguages.Length + ").");
        }

        for (int i = 1; i < languages.Length; i++)
        {
            string language = languages[i].Trim();
            if (!availableLanguages.Contains(language))
            {
                Debug.LogWarning("Language '" + language + "' in CSV is not in availableLanguages array.");
            }
            localizationData[language] = new Dictionary<string, string>();
        }

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length != languages.Length)
            {
                Debug.LogWarning("Invalid line in CSV file: " + lines[i]);
                continue;
            }

            string key = values[0].Trim();
            for (int j = 1; j < values.Length; j++)
            {
                string language = languages[j].Trim(); // Extract the language
                if (localizationData.ContainsKey(language))
                {
                    localizationData[language][key] = values[j].Trim();
                }
            }
        }
    }


    public void SetLanguage(int index)
    {
        if (index >= 0 && index < availableLanguages.Length)
        {
            currentLanguageIndex = index;
            currentLanguage = availableLanguages[currentLanguageIndex]; // Cache it
            SaveLanguage(); // Save the current language
            UpdateAllText(); // Update all text in the scene
            UpdateLanguageText(); // Update the language Text UI
        }
        else
        {
            Debug.LogError("Invalid language index: " + index);
        }
    }

    public void NextLanguage()
    {
        currentLanguageIndex = (currentLanguageIndex + 1) % availableLanguages.Length;
        SetLanguage(currentLanguageIndex);
    }

    public void PreviousLanguage()
    {
        currentLanguageIndex = (currentLanguageIndex - 1 + availableLanguages.Length) % availableLanguages.Length;
        SetLanguage(currentLanguageIndex);
    }

    public string GetText(string key)
    {
        currentLanguage = availableLanguages[currentLanguageIndex]; //Update language variable
        if (localizationData.ContainsKey(currentLanguage) && localizationData[currentLanguage].ContainsKey(key))
        {
            return localizationData[currentLanguage][key];
        }
        else
        {
            Debug.LogWarning("Key not found: " + key + " in language: " + currentLanguage);
            return key; // Return the key itself as fallback
        }
    }

    // Save the current language to PlayerPrefs
    void SaveLanguage()
    {
        PlayerPrefs.SetInt("LanguageIndex", currentLanguageIndex);
        PlayerPrefs.Save();
    }

    // Load the current language from PlayerPrefs
    void LoadLanguage()
    {
        if (PlayerPrefs.HasKey("LanguageIndex"))
        {
            currentLanguageIndex = PlayerPrefs.GetInt("LanguageIndex");
            if (currentLanguageIndex < 0 || currentLanguageIndex >= availableLanguages.Length)
            {
                Debug.LogWarning("Invalid language index in PlayerPrefs. Resetting to 0.");
                currentLanguageIndex = 0;
                SaveLanguage();
            }
        }
        else
        {
            // Save the default language if it doesn't exist in PlayerPrefs
            SaveLanguage();
        }

        currentLanguage = availableLanguages[currentLanguageIndex]; // set the initial language
    }


    // Updates all LocalizedText components in the scene
    public void UpdateAllText()
    {
        LocalizedText[] localizedTexts = FindObjectsOfType<LocalizedText>();
        foreach (LocalizedText textComponent in localizedTexts)
        {
            textComponent.UpdateText();
        }
    }

    void UpdateLanguageText()
    {
        if (languageText != null)
        {
            languageText.text = availableLanguages[currentLanguageIndex];
        }
    }
}