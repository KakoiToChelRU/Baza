using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 10f;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _minSize = 5f;
    [SerializeField] private float _maxSize = 20f;
    [SerializeField] private Vector2 _scrollLimits;
    [SerializeField] private float _verticalScrollMultiplier = 1f; // Дополнительный множитель для вертикальной прокрутки колесиком мыши.

    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
        if (_camera == null)
        {
            Debug.LogError("CameraScroll requires a Camera component!");
            enabled = false;
            return;
        }

        if (_camera.orthographic == false)
        {
            Debug.LogError("CameraScroll requires an Orthographic camera!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // Прокрутка камеры с помощью WASD или стрелок.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(moveDirection * _scrollSpeed * Time.deltaTime);

        // Вертикальная прокрутка камеры с помощью колесика мыши.
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.up * scroll * _scrollSpeed * _verticalScrollMultiplier * Time.deltaTime);

        // Ограничение положения камеры.
        ClampCameraPosition();
    }

    private void ClampCameraPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -_scrollLimits.x, _scrollLimits.x);
        pos.y = Mathf.Clamp(pos.y, -_scrollLimits.y, _scrollLimits.y);
        transform.position = pos;
    }
}