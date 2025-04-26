using System.Collections;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float speed = 5f;
    public float moveRange = 1.5f;

    public float leftBoundary = -2.5f;
    public float rightBoundary = 2.5f;


    private bool movingRight = true;
    public bool isWaiting = false;
    public float waitTime;
    public float minWaitTime, maxWaitTime;


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

            if (transform.position.x >= rightBoundary)
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

            if (transform.position.x <= leftBoundary)
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

        float zPosition = Mathf.Lerp(4f, 0f, (float)currentValue / ProgressController.instance.targetValue);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }

    private IEnumerator WaitBeforeChangingDirection()
    {
        isWaiting = true;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);

        float checkDuration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < checkDuration)
        {
            yield return null;
            elapsedTime += Time.deltaTime;

            if (WrestlingController.instance != null)
            {
                float arrowPosition = WrestlingController.instance.currentArrows.transform.position.x;
                float fishPosition = transform.position.x;

                bool isOppositeSide = (arrowPosition < fishPosition && transform.localScale.x > 0) ||
                                      (arrowPosition > fishPosition && transform.localScale.x < 0);

                if (!isOppositeSide)
                {
                    break;
                }
            }
        }

        isWaiting = false;

        WrestlingController.instance.SetPlayerOnSameSide(!movingRight);
    }

}
