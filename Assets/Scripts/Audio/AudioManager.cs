using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Если AudioManager еще не существует, попробуем найти его в сцене
                _instance = FindObjectOfType<AudioManager>();

                // Если AudioManager не найден, создадим новый GameObject и добавим к нему AudioManager
                if (_instance == null)
                {
                    GameObject audioManagerGO = new GameObject("AudioManager");
                    _instance = audioManagerGO.AddComponent<AudioManager>();
                }

                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    [Range(0f, 1f)]
    public float Volume = 0.5f; // Начальное значение громкости

    void Awake()
    {
        // Гарантируем, что существует только один экземпляр AudioManager.
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject); // Не уничтожать AudioManager при загрузке новой сцены
    }


    public void SetVolume(float volume)
    {
        Volume = Mathf.Clamp01(volume); // Убеждаемся, что громкость находится в диапазоне от 0 до 1
        Debug.Log("Volume set to: " + Volume);

        // Примените эту громкость ко всем AudioSource в вашей игре.
        AudioListener.volume = Volume;
    }
}