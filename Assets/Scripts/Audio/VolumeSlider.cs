using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider _slider;

    void Start()
    {
        _slider = GetComponent<Slider>();
        if (_slider == null)
        {
            enabled = false;
            return;
        }

        // Set the slider value to the current volume.
        _slider.value = AudioManager.Instance.Volume;

        // Add a listener to the slider's value change event.
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        // When the slider value changes, update the AudioManager's volume.
        AudioManager.Instance.SetVolume(value);
    }
}