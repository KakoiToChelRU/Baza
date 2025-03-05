using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider _slider;
    private AudioManager _audioManager; //Кэшируем ссылку

    void Start()
    {
        _slider = GetComponent<Slider>();
        if (_slider == null)
        {
            Debug.LogError("Slider component not found!");
            enabled = false;
            return;
        }

        _audioManager = AudioManager.Instance; //Получаем экземпляр

        if(_audioManager == null)
        {
             Debug.LogError("AudioManager.Instance is null! Ensure AudioManager exists and persists between scenes.");
             enabled = false;
             return;
        }

        // Set the slider value to the current volume.
        _slider.value = _audioManager.Volume;

        // Add a listener to the slider's value change event.
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        // When the slider value changes, update the AudioManager's volume.
        _audioManager.SetVolume(value);
    }
}