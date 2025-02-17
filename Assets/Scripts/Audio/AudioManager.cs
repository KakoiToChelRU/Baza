using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.IO;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } 

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private float _defaultVolume = 0.5f; 
    [SerializeField] private bool _resetSettings = false;
    private const string SAVE_FILE = "volumeSettings.json";

    public float Volume { get; private set; } 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadVolume();

        if (_musicSource == null)
        {
            enabled = false;
            return;
        }
        _musicSource.volume = Volume;
    }

    public void SetVolume(float volume)
    {
        Volume = Mathf.Clamp01(volume); 
        _musicSource.volume = Volume;
        SaveVolume(); 
    }

    private void LoadVolume()
    {
        string path = Application.persistentDataPath + "/" + SAVE_FILE;
        if (_resetSettings)
        {
            Debug.LogWarning("RESET VOLUME TRIGGERED");
            _resetSettings = false;
            if (File.Exists(path)) File.Delete(path);
            Volume = _defaultVolume;
            SetVolume(Volume);
            return;
        }
        if (!File.Exists(path))
        {
            Volume = _defaultVolume;
            SetVolume(Volume);
            return;
        }

        string jsonData = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
        Volume = data.volume;
        SetVolume(Volume);
    }

    private void SaveVolume()
    {
        SaveData data = new() { volume = Volume };
        string jsonData = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/" + SAVE_FILE;
        File.WriteAllText(path, jsonData);
    }

    private class SaveData
    {
        public float volume;
    }
}