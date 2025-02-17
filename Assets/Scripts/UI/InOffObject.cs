using UnityEngine;
using System.Collections.Generic; // Required for List

public class InOffObject : MonoBehaviour
{
    [Tooltip("Objects to hide when the script starts.")]
    public List<GameObject> objectsToHide; // Use List<GameObject> for hiding
    [Tooltip("Objects to show when the script starts. These will start hidden.")]
    public List<GameObject> objectsToShow; // Use List<GameObject> for showing.

    void Start()
    {
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Missing object in objectsToShow list in " + gameObject.name);
            }
        }
    }

    public void HideObject()
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Missing object in objectsToHide list in " + gameObject.name);
            }
        }
    }

    public void ShowObject()
    {
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Missing object in objectsToShow list in " + gameObject.name);
            }
        }
    }

    public void Back()
    {
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(false); //Crucial change here
            }
            else
            {
                Debug.LogWarning("Missing object in objectsToShow list in " + gameObject.name);
            }
        }

        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Missing object in objectsToHide list in " + gameObject.name);
            }
        }
    }
}