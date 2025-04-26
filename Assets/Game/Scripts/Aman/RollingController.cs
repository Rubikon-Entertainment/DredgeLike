using UnityEngine;

public class RollingController : MonoBehaviour
{
    public static RollingController instance { get; private set; }
    public int currentRolls { get; set; }
    public int targetRolls { get; set; }


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

    public void Roll(bool isRolled)
    {
        if (isRolled) currentRolls += 1;
    }

    public bool IsFinished()
    {
        if (currentRolls == targetRolls) { 
            return true; 
        } else { 
            return false; 
        }
    }
}
