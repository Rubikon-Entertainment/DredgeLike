using UnityEngine;

public class ProgressController : MonoBehaviour
{
    public static ProgressController instance { get; private set; }
    public int currentValue = 0;
    public int deltaValue;
    public int targetValue = 10;
    public float penaltyTime;
    public int penaltyValue = 1;


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
        if (currentValue >= targetValue)
        {
            Debug.Log("Progress finished. You won!");
            return true;
        }
        else return false;
    }

    public void Penalty()
    {
        currentValue -= penaltyValue;
        if (currentValue < 0)
        {
            currentValue = 0;
        }
        Debug.Log("Penalty! Current value is " + currentValue);
    }

    public void UpdateProgress()
    {
        currentValue += deltaValue;
        if (currentValue >= targetValue)
        {
            currentValue = targetValue;
        }
    }

    public void SetDeltaValue(Level level)
    {
        switch (level)
        {
            case Level.Easy:
                deltaValue = 3;
                penaltyTime = 3f;
                break;
            case Level.Medium:
                deltaValue = 2;
                penaltyTime = 2f;
                break;
            case Level.Hard:
                deltaValue = 1;
                penaltyTime = 1f;
                break;
            default:
                Debug.LogError("Unknown level selected!");
                break;
        }
    }
}
