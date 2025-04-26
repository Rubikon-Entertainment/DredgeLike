using UnityEngine;

public class Arrows : MonoBehaviour
{
    public static Arrows instance { get; private set; }
    private Vector3 startPosition;
    public float moveSpeed = 5f;
    public float moveRange = 3f;
    

    private void Awake()
    {
        Singleton();
        startPosition = transform.position;
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

        if (transform.position.x + movement.x < startPosition.x - moveRange ||
            transform.position.x + movement.x > startPosition.x + moveRange)
        {
            return;
        }

        transform.Translate(movement);
    }
}
