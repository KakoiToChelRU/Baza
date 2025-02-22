using UnityEngine;
using TMPro;

public class MyUIController : MonoBehaviour
{
    public TextMeshProUGUI windowedModeText; // Присвойте в инспекторе текстовый элемент

    void Start()
    {
        UpdateWindowedModeText(); // Инициализируем текстовый элемент
    }

    // Метод обновления текста на основе текущего индекса режима окон
    public void UpdateWindowedModeText()
    {
        if (windowedModeText != null)
        {
            // Получаем ключ на основе текущего индекса
            string key = $"windowedMode{CSVLocalizationManager.instance.currentLanguageIndex + 1}";
            windowedModeText.text = CSVLocalizationManager.instance.GetText(key);
        }
    }
}