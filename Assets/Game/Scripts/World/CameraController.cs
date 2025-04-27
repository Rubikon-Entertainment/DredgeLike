using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Singleton instance
    public static CameraController Instance { get; private set; }

    [Header("Target Settings")]
    public Transform targetPlayer;
    private Transform currentTarget;

    [Header("Camera Settings")]
    public float distance = 10f;
    public float angle = 10f;
    public float heightOffset = 2f;
    public float smoothSpeed = 2f;

    [Header("Player Camera Settings")]
    public float playerDistance = 10f;
    public float playerAngle = 10f;
    public float playerHeightOffset = 4f;

    [Header("Harbor Camera Settings")]
    public float harborDistance = 10f;
    public float harborAngle = 0f;
    public float harborHeightOffset = 1f;

    [Header("Stock Camera Settings")]
    public float stockDistance = 8f;
    public float stockAngle = 89f;
    public float stockHeightOffset = 3f;

    // Camera mode constants
    public static class CameraMode 
    { 
        public const string Player = "Player";
        public const string Harbor = "Harbor";
        public const string Stock = "Stock";
    }
    
    private string currentMode = CameraMode.Player;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        // Set initial target
        currentTarget = targetPlayer;
    }

    void LateUpdate()
    {
        if (currentTarget != null)
        {
            // Вычисляем позицию камеры на основе угла и расстояния
            float radianAngle = angle * Mathf.Deg2Rad; // Конвертация угла в радианы
            float height = distance * Mathf.Sin(radianAngle); // Высота камеры
            float groundDistance = distance * Mathf.Cos(radianAngle); // Расстояние по горизонтали

            // Рассчитаем желаемую позицию камеры
            Vector3 desiredPosition = currentTarget.position;
            desiredPosition += Vector3.up * (height + heightOffset); // Поднимаем камеру
            desiredPosition -= currentTarget.forward * groundDistance; // Отодвигаем камеру назад

            // Плавно перемещаем камеру к желаемой позиции
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // Направляем камеру на цель
            transform.LookAt(currentTarget);
        }
    }

    public void SetCameraMode(string mode)
    {
        currentMode = mode;
        
        switch (mode)
        {
            case CameraMode.Player:
                distance = playerDistance;
                angle = playerAngle;
                heightOffset = playerHeightOffset;
                currentTarget = targetPlayer;
                smoothSpeed = 2f;
                break;
                
            case CameraMode.Harbor:
                distance = harborDistance;
                angle = harborAngle;
                heightOffset = harborHeightOffset;
                smoothSpeed = 1f;
                break;
                
            case CameraMode.Stock:
                distance = stockDistance;
                angle = stockAngle;
                heightOffset = stockHeightOffset;
                currentTarget = targetPlayer;
                smoothSpeed = 1f;
                break;
        }
    }
    
    public void ChangeTargetWithMode(Transform newTarget, string mode)
    {
        SetCameraMode(mode);
        currentTarget = newTarget;
    }
} 