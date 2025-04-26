using UnityEngine;

public class ProgressController : MonoBehaviour
{
    public static ProgressController instance { get; private set; }
    public int currentValue { get; set; }
    public int targetValue = 10;
    public float penaltyTime = 3f;
    public int penaltyValue = 1;


    private void Awake()
    {
        Singleton();
        currentValue = 0;
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
            Debug.Log("Progress finished");
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
}
