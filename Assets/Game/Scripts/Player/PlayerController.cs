using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public FixedJoystick fixedJoystick;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float rotationSpeed = 100f;
    public float drag = 2f;
    public float speedLevelUp = 20f;
    
    [Header("Vehicle Steering Settings")]
    public float steeringAngle = 30f;
    public float steeringSpeed = 5f;
    public float turnRadius = 3f;
    public bool onlyRotateWhileMoving = true;
    public float minSpeedToTurn = 0.5f;

    [Header("Harbor Settings")]
    public float spawnDistanceFromHarbor = 10f; // Distance to spawn from harbor
    public float moveToHarborDuration = 2f; // Duration of smooth movement to harbor

    [Header("Interaction Settings")]
    private bool controlsLocked = false; // Flag to disable controls during interactions

    private Rigidbody rb;
    private float rotationInput;   
    private float forwardInput;
    private float currentSteeringAngle = 0f;
    private float maxSpeed = 100f;

    void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            // Optional: Uncomment the line below if you want this object to persist between scenes
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the player object!");
        }

        rb.linearDamping = drag;
    }

    void Update()
    {
        // Only process inputs if controls aren't locked
        if (!controlsLocked)
        {
            rotationInput = fixedJoystick.Horizontal; // Input.GetAxisRaw("Horizontal"); 
            forwardInput =  fixedJoystick.Vertical; //Input.GetAxisRaw("Vertical");
        }
        else
        {
            // Zero out inputs when controls are locked
            rotationInput = 0f;
            forwardInput = 0f;
        }
    }

    void FixedUpdate()
    {
        HandleSteering();

        // Handle movement
        if (forwardInput != 0)
        {
            Vector3 force = transform.forward * forwardInput * speed;
            rb.AddForce(force, ForceMode.Force);

            if (rb.linearVelocity.magnitude > speed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * speed;
            }
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, drag * Time.fixedDeltaTime);
        }
    }

    void HandleSteering()
    {
        // Smoothly adjust current steering angle based on input
        float targetSteeringAngle = rotationInput * steeringAngle;
        currentSteeringAngle = Mathf.Lerp(currentSteeringAngle, targetSteeringAngle, steeringSpeed * Time.fixedDeltaTime);

        float currentSpeed = rb.linearVelocity.magnitude;
        
        if (!onlyRotateWhileMoving || currentSpeed > minSpeedToTurn)
        {
            // Steering effect increases with speed but is limited by turn radius
            float turnFactor = Mathf.Clamp01(currentSpeed *2 + 2f / turnRadius);
            
            // Calculate rotation amount based on current steering and speed
            float rotationAmount = currentSteeringAngle  * turnFactor* Time.fixedDeltaTime;
            
            // Apply the rotation - can multiply by forwardInput to make reversing steer backwards
            transform.Rotate(0, rotationAmount * Mathf.Sign(forwardInput), 0);
        }
    }

    void SweemSpeedLevelUp(){
        speed += speedLevelUp;
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }

    public void SweemToHarbor(Transform harborTarget)
    {
        if (harborTarget == null)
        {
            Debug.LogError("Harbor target is null!");
            return;
        }

        // Lock controls during harbor interaction
        LockControls();

        // Calculate the target position behind the harbor
        Vector3 backwardDirection = -harborTarget.forward; // Use the harbor's backward direction
        Vector3 rightDirection = harborTarget.right; // Use the harbor's left direction
        Vector3 targetPosition = harborTarget.position + (backwardDirection * (spawnDistanceFromHarbor + 10f));

        // Calculate the starting position to the left of the harbor
        Vector3 startPosition = harborTarget.position + (rightDirection * (spawnDistanceFromHarbor + 10f)) + (backwardDirection * (spawnDistanceFromHarbor + 10f));
        
        // Maintain the y position
        startPosition.y = transform.position.y;
        targetPosition.y = transform.position.y;
        
        // Start the smooth movement coroutine
        StartCoroutine(SmoothMoveToPosition(startPosition, targetPosition, harborTarget.rotation * Quaternion.Euler(0, -90, 0), moveToHarborDuration));
    }
    
    public void SweemToStock(Transform stockTarget)
    {
        if (stockTarget == null)
        {
            Debug.LogError("Stock target is null!");
            return;
        }

        // Lock controls during stock interaction
        LockControls();

        Vector3 targetPosition = stockTarget.position;

        // Calculate the starting position to the left of the stock
        Vector3 startPosition = transform.position;
        
        // Maintain the y position
        startPosition.y = transform.position.y;
        targetPosition.y = transform.position.y;
        
        // Start the smooth movement coroutine
        StartCoroutine(SmoothMoveToPosition(startPosition, targetPosition, stockTarget.rotation * Quaternion.Euler(0, -90, 0), moveToHarborDuration));
    }
    
    private IEnumerator SmoothMoveToPosition(Vector3 startPos, Vector3 endPos, Quaternion targetRotation, float duration)
    {
        // Teleport to the start position
        transform.position = startPos;
        
        float time = 0;
        Quaternion startRotation = transform.rotation;
        
        while (time < duration)
        {
            float t = time / duration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            
            time += Time.deltaTime;
            yield return null;
        }
        
        // Ensure we end exactly at the target
        transform.position = endPos;
        transform.rotation = targetRotation;
        
        // Unlock controls after positioning
        UnlockControls();
    }
    
    // Public methods to lock/unlock controls during interactions
    public void LockControls()
    {
        controlsLocked = true;
        // Stop any current movement
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    
    public void UnlockControls()
    {
        controlsLocked = false;
    }
}