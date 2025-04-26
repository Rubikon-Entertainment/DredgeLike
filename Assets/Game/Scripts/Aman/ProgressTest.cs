using UnityEngine;

public class ProgressTest : MonoBehaviour
{
    private ProgressController progressController;

    void Start()
    {
        progressController = ProgressController.instance;


        progressController.penaltyTime = 5;
        progressController.penaltyValue = 10;
        progressController.currentValue = 100;

        progressController.StartPenaltyTimer();
    }

    void Update()
    {
        progressController.IsFinished();
    }
}
