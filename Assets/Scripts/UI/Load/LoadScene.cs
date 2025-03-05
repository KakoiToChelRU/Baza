using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Tooltip("Имя сцены для загрузки")]
    public string sceneName;

    [Tooltip("Время ожидания перед загрузкой сцены")]
    public float delayBeforeLoad = 1f;

    public void LoadTheScene() // Сделал метод публичным, чтобы его можно было вызывать из кнопок или других объектов UI.
    {
        // Запускаем корутину для загрузки сцены с задержкой.
        StartCoroutine(LoadSceneAfterDelay());
    }

    private System.Collections.IEnumerator LoadSceneAfterDelay()
    {
        // Ждем указанное время.
        yield return new WaitForSeconds(delayBeforeLoad);

        // Загружаем сцену.
        SceneManager.LoadScene(sceneName);
    }
}