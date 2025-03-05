using UnityEngine;
using System.Collections.Generic;

public class PuskPanel : MonoBehaviour
{
    [Tooltip("Объекты, которые нужно скрыть, когда панель показана.")]
    public List<GameObject> objectsToHide;

    [Tooltip("Объекты, которые нужно показать, когда кнопка нажата (панель). Они будут изначально скрыты.")]
    public List<GameObject> objectsToShow;

    private Dictionary<GameObject, bool> _initialActiveState = new Dictionary<GameObject, bool>();

    // Переменная для хранения состояния панели (показана или скрыта)
    private bool isPanelShown = false;

    void Start()
    {
        // Сохраняем исходное состояние и скрываем объекты из objectsToShow.
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                _initialActiveState[obj] = obj.activeSelf; // Сохраняем исходное состояние
                obj.SetActive(false); // Изначально скрываем.
            }
            else
            {
                Debug.LogWarning("Отсутствует объект в списке objectsToShow в " + gameObject.name);
            }
        }

        //Сохраняем начальное состояние objectsToHide
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                _initialActiveState[obj] = obj.activeSelf;
            }
            else
            {
                Debug.LogWarning("Отсутствует объект в списке objectsToHide в " + gameObject.name);
            }
        }
    }

    // Метод переключения видимости объектов
    public void ToggleObjectsToShow()
    {
        isPanelShown = !isPanelShown; // Инвертируем значение состояния панели

        // Объекты, которые будут показываться/скрываться
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(isPanelShown); // Показываем или скрываем панель
            }
            else
            {
                Debug.LogWarning("Отсутствует объект в списке objectsToShow в " + gameObject.name);
            }
        }

        // Объекты, которые будут показывать/скрывать противоположно состоянию panels
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(!isPanelShown); // Скрываем, если панель видима, и наоборот
            }
            else
            {
                Debug.LogWarning("Отсутствует объект в списке objectsToHide в " + gameObject.name);
            }
        }
    }
}