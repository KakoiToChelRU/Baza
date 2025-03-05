using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkOpener : MonoBehaviour
{
    [System.Serializable]
    public struct LinkButton
    {
        public Button button;
        public string url;
    }

    [Tooltip("Список кнопок и соответствующих ссылок")]
    public List<LinkButton> linkButtons = new List<LinkButton>();

    void Start()
    {
        // Проверяем, что все кнопки назначены и подписываемся на события OnClick.
        foreach (LinkButton linkButton in linkButtons)
        {
            if (linkButton.button == null)
            {
                Debug.LogError("Button is not assigned in LinkButtons list!");
                continue; // Пропускаем эту итерацию, но продолжаем проверять остальные кнопки.
            }
            if (string.IsNullOrEmpty(linkButton.url))
            {
                Debug.LogWarning("URL is not assigned for button " + linkButton.button.name);
            }

            Button button = linkButton.button; // Necessary to capture the correct 'linkButton' in the lambda expression
            string url = linkButton.url;
            linkButton.button.onClick.AddListener(() => OpenLink(url)); // Используем lambda для передачи параметра url.
        }
    }

    public void OpenLink(string url) // Метод должен быть публичным, чтобы его можно было вызвать из кнопки.
    {
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogWarning("Attempted to open empty URL");
            return;
        }
        Application.OpenURL(url);
    }
}