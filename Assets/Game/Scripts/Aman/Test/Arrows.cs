using UnityEngine;

public class Arrows : MonoBehaviour
{
    public float moveSpeed = 5f;
    public WrestlingController wrestlingController;

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
