using System.Collections.Generic;
using UnityEngine;

public class SetActiveCamera : MonoBehaviour
{
    [Tooltip("Список объектов, которые нужно скрыть.")]
    public List<GameObject> objectsToHide = new List<GameObject>(); // Используем List<GameObject>

    [Tooltip("Список объектов, которые нужно показать.")]
    public List<GameObject> objectsToShow = new List<GameObject>(); // Используем List<GameObject>

    [Tooltip("Клавиша для скрытия объектов (по умолчанию 'E').")]
    public KeyCode hideKey = KeyCode.E;

    [Tooltip("Клавиша для выхода (по умолчанию 'Esc').")]
    public KeyCode hideKey = KeyCode.Esc;

    private bool _areObjectsHidden = false;  // отслеживаем, скрыты объекты или нет

    void Start()
    {
        // Скрываем объекты при старте
        SetObjectsVisibility(false);
    }



    void Update()
    {
        if (Input.GetKeyDown(hideKey))
        {
            _areObjectsHidden = !_areObjectsHidden; // Переключаем состояние
            SetObjectsVisibility(_areObjectsHidden);
        }
    }

    // Метод для установки видимости объектов
    void SetObjectsVisibility(bool isVisible)
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null) // Проверяем, что объект существует
            {
                obj.SetActive(isVisible);
            }
            else
            {
                Debug.LogWarning("Object in the list is null!  Check your list.");
            }
        }
    }

    // Helper метод для поиска объектов по тегу и добавления их в список
    private void FindObjectsWithTagAndAddToList(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        if (taggedObjects.Length > 0)
        {
            foreach (GameObject obj in taggedObjects)
            {
                objectsToHide.Add(obj);
            }
            Debug.Log($"Found and added {taggedObjects.Length} objects with tag '{tag}' to the list.");
        }
        else
        {
            Debug.LogWarning($"No objects found with tag '{tag}'. Make sure your objects have this tag or assign objects in the inspector.");
        }
    }
}