using UnityEngine;

public class WrestlingController : MonoBehaviour
{
    public static WrestlingController instance { get; private set; }
    public FishController fishPrefab;
    public Arrows arrowsPrefab;

    private FishController currentFish;
    private Arrows currentArrows;
    private float timer;
    public float successTime = 2f;

    private bool playerOnSameSide = false;
    private bool penaltyApplied = false;

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

    public void StartGame()
    {
        InitializeFish();
    }

    private void InitializeFish()
    {
        currentFish = Instantiate(fishPrefab);
        currentArrows = Instantiate(arrowsPrefab);
    }

    private void Update()
    {
        if (ProgressController.instance.IsFinished())
        {
            StopGame();
            return;
        }

        CheckArrowDirection();
    }

    private void CheckArrowDirection()
    {
        if (currentFish.isWaiting)
        {
            int fishDirection = currentFish.GetDirection();
            float arrowPosition = currentArrows.transform.position.x;
            float fishPosition = currentFish.transform.position.x;

            bool isOppositeDirection = (fishDirection == 1 && arrowPosition < fishPosition) ||
                                       (fishDirection == -1 && arrowPosition > fishPosition);

            if (isOppositeDirection)
            {
                timer += Time.deltaTime;
                if (timer >= successTime)
                {
                    if (!playerOnSameSide)
                    {
                        if (!penaltyApplied)
                        {
                            Debug.Log("Penalty");
                            ProgressController.instance.Penalty();
                            penaltyApplied = true;
                        }
                    }
                    else
                    {
                        Debug.Log("Success");
                        ProgressController.instance.UpdateProgress();
                    }

                    currentFish.isWaiting = false;
                    timer = 0;
                    penaltyApplied = false;
                }
            }
            else
            {
                timer = 0;
                penaltyApplied = false;
                ProgressController.instance.Penalty();
            }
        }
    }

    public void SetPlayerOnSameSide(bool value)
    {
        playerOnSameSide = value;
    }

    private void StopGame()
    {
        Debug.Log("Game Over! You've reached the target value.");
    }
}
