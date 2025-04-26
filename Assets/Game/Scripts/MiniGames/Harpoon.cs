using UnityEngine;

public class Harpoon : MonoBehaviour
{
    public float speed = 5f;
    public float range = 5f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isReturning = false;
    private bool canShoot = true;

    void Start()
    {
        startPosition = transform.position;
    }

    public void Shoot(Vector3 direction)
    {
        if (canShoot)
        {
            targetPosition = startPosition + direction.normalized * range;
            isReturning = false;
            canShoot = false;
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isReturning = true; 
        }

        if (isReturning)
        {
            ReturnBack();
        }
    }

    private void ReturnBack()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, startPosition) < 0.1f)
        {
            isReturning = false; 
            canShoot = true;
        }
    }

    void Update()
    {
        if (!isReturning)
        {
            MoveToTarget();
        }
    }
}
