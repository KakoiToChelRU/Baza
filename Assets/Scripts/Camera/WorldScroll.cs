using UnityEngine;

public class WorldScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 10f;
    [SerializeField] private Vector2 _scrollLimits;

    private Transform _cameraTransform;

    void Start()
    {
        _cameraTransform = Camera.main.transform;

        if (_cameraTransform == null)
        {
            Debug.LogError("Main Camera not found in the scene!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // Получаем ввод от игрока.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Вычисляем вектор движения камеры.
        Vector3 cameraMoveDirection = new Vector3(horizontalInput, verticalInput, 0f);

        // Вычисляем вектор движения мира в противоположном направлении.
        Vector3 worldMoveDirection = -cameraMoveDirection;

        // Перемещаем все объекты мира.
        ScrollWorld(worldMoveDirection);
    }

    void ScrollWorld(Vector3 direction)
    {
        // Перемещаем все объекты в сцене, кроме камеры.
        foreach (Transform child in transform)  // Assumes the objects to scroll are children of this object
        {
            child.Translate(direction * _scrollSpeed * Time.deltaTime);
            ClampObjectPosition(child);
        }
    }

    void ClampObjectPosition(Transform objTransform)
    {
        Vector3 pos = objTransform.localPosition; //Use localPosition, as position is relative to the parent
        pos.x = Mathf.Clamp(pos.x, -_scrollLimits.x, _scrollLimits.x);
        pos.y = Mathf.Clamp(pos.y, -_scrollLimits.y, _scrollLimits.y);
        objTransform.localPosition = pos;
    }
}

