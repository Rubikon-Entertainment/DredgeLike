using System.Collections;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    public static ProgressController instance { get; private set; }
    public int currentValue { get; set; }
    public int targetValue { get; set; }
    public float penaltyTime { get; set; }
    public int penaltyValue { get; set; }

    private Coroutine penaltyCoroutine;

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

    public bool IsFinished()
    {
        if (currentValue == targetValue)
        {
            StopPenaltyTimer();
            Debug.Log("Fish is gone :(");
            return true;
        }
        else return false;
    }

    void Penalty(bool isPenalty)
    {
        if (isPenalty)
        {
            currentValue -= penaltyValue;
            Debug.Log("Penalty! Current value is " + currentValue);
        }
    }

    public void StartPenaltyTimer()
    {
        if (penaltyCoroutine == null)
        {
            penaltyCoroutine = StartCoroutine(PenaltyTimerCoroutine());
        }
    }

    public void StopPenaltyTimer()
    {
        if (penaltyCoroutine != null)
        {
            StopCoroutine(penaltyCoroutine);
            penaltyCoroutine = null;
        }
    }

    private IEnumerator PenaltyTimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(penaltyTime);
            Penalty(true);
        }
    }
}
