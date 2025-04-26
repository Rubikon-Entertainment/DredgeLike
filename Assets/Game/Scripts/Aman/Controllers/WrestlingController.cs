using UnityEngine;

public class WrestlingController : MonoBehaviour
{
    public static WrestlingController instance { get; private set; }
    public Fish fishPrefab;
    public Arrows arrowsPrefab;

    private Fish currentFish;
    private Arrows currentArrows;
    private float timer;
    public float successTime = 2f;

    private void Awake()
    {
        Singleton();
        InitializeFish();
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
        if (currentFish.isWaiting)
        {
            int fishDirection = currentFish.GetDirection();
            float arrowPosition = currentArrows.transform.position.x;
            float fishPosition = currentFish.transform.position.x;

            bool isOppositeDirection = (fishDirection == 1 && arrowPosition < fishPosition) ||
                                       (fishDirection == -1 && arrowPosition > fishPosition) ||
                                       (fishDirection == 1 && arrowPosition > fishPosition) ||
                                       (fishDirection == -1 && arrowPosition < fishPosition);

            if (isOppositeDirection)
            {
                timer += Time.deltaTime;
                if (timer >= successTime)
                {
                    Debug.Log("Success");
                    currentFish.isWaiting = false;
                    timer = 0;
                }
            }
            else
            {
                timer = 0;
            }
        }
    }

}
