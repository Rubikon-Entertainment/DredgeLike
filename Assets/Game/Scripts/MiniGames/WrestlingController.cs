using UnityEngine;

public class WrestlingController : MonoBehaviour
{
    public static WrestlingController instance { get; private set; }
    public FishController fishPrefab;
    public Arrows arrowsPrefab;

    private FishController currentFish;
    public Arrows currentArrows;
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
        CheckArrowDirection();
    }

    private void CheckArrowDirection()
    {
        if (currentFish && currentFish.isWaiting)
        {
            float arrowPosition = currentArrows.transform.position.x;
            float fishPosition = currentFish.transform.position.x;

            bool isOppositeSide = (arrowPosition < fishPosition && currentFish.transform.localScale.x > 0) ||
                                  (arrowPosition > fishPosition && currentFish.transform.localScale.x < 0);

            if (isOppositeSide)
            {
                timer += Time.deltaTime;

                if (timer >= successTime)
                {
                    if (!ProgressController.instance.IsFinished() && currentFish.isWaiting)
                    {
                        if (playerOnSameSide)
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
            }
            else
            {
                timer = 0;
                penaltyApplied = false;
            }
        }
    }



    public void SetPlayerOnSameSide(bool value)
    {
        playerOnSameSide = value;
    }

    public void StopGame()
    {
        Debug.Log("Game Over! You've reached the target value.");
        if (currentFish != null)
        {
            currentFish.gameObject.SetActive(false);
        }

        if (currentArrows != null)
        {
            currentArrows.gameObject.SetActive(false);
        }
    }
}
