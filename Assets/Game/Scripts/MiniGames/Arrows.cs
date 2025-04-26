using UnityEngine;

public class Arrows : MonoBehaviour
{
    public static Arrows instance { get; private set; }
    public float moveSpeed = 12f;
    public float moveRange = 1.5f;

    public float leftBoundary = -2.5f;
    public float rightBoundary = 2.5f;

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;

        float newPositionX = transform.position.x + movement.x;

        if (newPositionX < leftBoundary)
        {
            newPositionX = leftBoundary;
        }
        else if (newPositionX > rightBoundary)
        {
            newPositionX = rightBoundary;
        }

        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }

}
