using System.Collections;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = 5f;
    public float moveRange = 3f;

    private Vector3 startPosition;
    private bool movingRight = true;
    private bool isWaiting = false;
    private float waitTime;
    public float minWaitTime, maxWaitTime;



    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveFish();
    }

    private void MoveFish()
    {
        if (isWaiting) return;

        if (movingRight)
        {
            speed += Time.deltaTime;
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (transform.position.x >= startPosition.x + moveRange)
            {
                movingRight = false;
                speed = Random.Range(5f, 10f);
                StartCoroutine(WaitBeforeChangingDirection());
            }
        }
        else
        {
            speed -= Time.deltaTime;
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (transform.position.x <= startPosition.x - moveRange)
            {
                movingRight = true;
                speed = Random.Range(5f, 10f);
                StartCoroutine(WaitBeforeChangingDirection());
            }
        }

        if (speed < 5f)
        {
            speed = 5f;
        }
    }

    private IEnumerator WaitBeforeChangingDirection()
    {
        isWaiting = true;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}
