using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseSensitivity : MonoBehaviour
{
    public Slider sensitivitySlider;
    public float minSensitivity = 0.1f;
    public float maxSensitivity = 10.0f;
    
    private void Start()
    {
        // Устанавливаем начальное значение чувствительности
        UpdateMouseSensitivity(sensitivitySlider.value);
        
        // Подписываемся на событие изменения значения слайдера
        sensitivitySlider.onValueChanged.AddListener(UpdateMouseSensitivity);
    }

    private void UpdateMouseSensitivity(float value)
    {
        // Отображаем текущее значение чувствительности
        Debug.Log("Чувствительность: " + value);
        
        // Вычисляем новое значение чувствительности
        float sensitivity = Mathf.Lerp(minSensitivity, maxSensitivity, value);
        
        // Применяем новую чувствительность к камере или другому компоненту
        Camera.main.fieldOfView = sensitivity * 2.0f; // Пример использования чувствительности
    }
}