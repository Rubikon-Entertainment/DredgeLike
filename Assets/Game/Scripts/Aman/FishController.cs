using System.Collections;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float speed = 5f;
    public float moveRange = 5f;

    private Vector3 startPosition;
    private bool movingRight = true;
    public bool isWaiting = false;
    public float waitTime;
    public float minWaitTime, maxWaitTime;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveFish();
        UpdateZPosition();
    }

    public int GetDirection()
    {
        return movingRight ? 1 : -1;
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

    private void UpdateZPosition()
    {
        int currentValue = ProgressController.instance.currentValue;

        float zPosition = Mathf.Lerp(4f, -2f, (float)currentValue / ProgressController.instance.targetValue);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }

    private IEnumerator WaitBeforeChangingDirection()
    {
        isWaiting = true;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;

        WrestlingController.instance.SetPlayerOnSameSide(!movingRight);
    }
}
