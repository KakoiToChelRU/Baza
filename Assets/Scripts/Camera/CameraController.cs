using UnityEngine;

public class CameraController : MonoBehaviour
{
    public MouseSensitivityController sensitivityController;
    public float rotationSpeed = 100f;

    private float _rotationX = 0f;
    private float _rotationY = 0f;

    void Start()
    {
       // Получаем ссылку на компонент MouseSensitivityController, если он не назначен в Inspector.
       if (sensitivityController == null)
       {
            sensitivityController = FindObjectOfType<MouseSensitivityController>(); //Ищем первый компонент на сцене
            if(sensitivityController == null){
                Debug.LogError("MouseSensitivityController not found!");
                enabled = false; // Отключаем скрипт, если MouseSensitivityController не найден.
                return;
            }
       }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Применяем чувствительность мыши.
        Vector2 sensitiveMouseDelta = sensitivityController.ApplySensitivity(new Vector2(mouseX, mouseY));

        _rotationX -= sensitiveMouseDelta.y * rotationSpeed * Time.deltaTime;
        _rotationY += sensitiveMouseDelta.x * rotationSpeed * Time.deltaTime;

        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}