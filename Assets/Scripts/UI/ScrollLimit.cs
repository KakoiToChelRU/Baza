using UnityEngine;
using UnityEngine.UI;

public class ScrollLimit : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;

    void Start()
    {
        if (_scrollRect == null)
        {
            Debug.LogError("ScrollRect is not assigned!");
            enabled = false;
            return;
        }

        _scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    void OnDestroy() // Clean up listener on destroy.
    {
        _scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
    }

    private void OnScrollValueChanged(Vector2 value)
    {
        if (_scrollRect.vertical && _scrollRect.verticalNormalizedPosition > 1f)
        {
            _scrollRect.verticalNormalizedPosition = 1f;
        }
        else if (_scrollRect.vertical && _scrollRect.verticalNormalizedPosition < 0f)
        {
            _scrollRect.verticalNormalizedPosition = 0f;
        }

        if (_scrollRect.horizontal && _scrollRect.horizontalNormalizedPosition > 1f)
        {
            _scrollRect.horizontalNormalizedPosition = 1f;
        }
        else if (_scrollRect.horizontal && _scrollRect.horizontalNormalizedPosition < 0f)
        {
            _scrollRect.horizontalNormalizedPosition = 0f;
        }
    }
}