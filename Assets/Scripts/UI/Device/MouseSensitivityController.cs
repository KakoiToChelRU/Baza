using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityController : MonoBehaviour
{
    [Tooltip("Ссылка на слайдер в UI")]
    public Slider sensitivitySlider;

    [Tooltip("Чувствительность мыши (по умолчанию)")]
    public float defaultSensitivity = 2.0f;

    [Tooltip("Минимальная чуствительность")]
    public float minSensitivity = 0.1f;

    [Tooltip("Максимальная чуствительность")]
    public float maxSensitivity = 10.0f;

    private float _mouseSensitivity;

    public float MouseSensitivity
    {
        get { return _mouseSensitivity; }
        set { _mouseSensitivity = value; }
    }


    void Start()
    {
        // Проверяем, назначен ли слайдер.
        if (sensitivitySlider == null)
        {
            Debug.LogError("Sensitivity Slider is not assigned!");
            enabled = false; // Отключаем скрипт, если слайдер не назначен.
            return;
        }

        // Устанавливаем минимальное и максимальное значение слайдера.
        sensitivitySlider.minValue = minSensitivity;
        sensitivitySlider.maxValue = maxSensitivity;

        // Устанавливаем значение слайдера в соответствии с текущей чувствительностью.
        _mouseSensitivity = defaultSensitivity;
        sensitivitySlider.value = _mouseSensitivity;

        // Подписываемся на событие OnValueChanged слайдера.
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void OnSensitivityChanged(float newValue)
    {
        // Обновляем чувствительность мыши при изменении значения слайдера.
        _mouseSensitivity = newValue;
        Debug.Log("Mouse sensitivity changed to: " + _mouseSensitivity);
    }

    // Пример использования чувствительности в скрипте управления камерой.
    // (Этот метод нужно вызвать из скрипта управления камерой, передав изменение позиции мыши)
    public Vector2 ApplySensitivity(Vector2 mouseDelta)
    {
        return mouseDelta * _mouseSensitivity;
    }
}