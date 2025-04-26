using System.Collections.Generic;
using UnityEngine;

public class TappingController : MonoBehaviour
{
    public static TappingController instance { get; private set; }
    public Circle circlePrefab;
    public FishController fishPrefab;
    public int tappedTargets;
    public int targets = 4;
    public float spawnAreaWidth = 5f;
    public float spawnAreaHeight = 5f;
    public float timeLimit = 5f;
    public float spawnAreaZMin = -5f;
    public float spawnAreaZMax = -2.5f;

    private float timer; 
    private bool isTimerRunning = false;
    private bool isGameActive = false;
    private FishController currentFish;

    private List<Circle> activeCircles = new List<Circle>();


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
        isGameActive = true;
        InitializeCircles();
        currentFish = Instantiate(fishPrefab);
        GenerateNewPositions();
    }

    public void StopGame()
    {
        isGameActive = false;
        isTimerRunning = false;
        foreach (Circle circle in activeCircles)
        {
            circle.gameObject.SetActive(false);
        }
        currentFish.gameObject.SetActive(false);
        Debug.Log("Game stopped!");
    }

    public void InitializeCircles()
    {
        for (int i = 0; i < targets; i++)
        {
            Circle newCircle = Instantiate(circlePrefab);
            newCircle.gameObject.SetActive(false);
            activeCircles.Add(newCircle);
        }
    }

    private void Update()
    {
        if (isTimerRunning && isGameActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ApplyPenalty();
                isTimerRunning = false; 
            }
        }
    }

    public void Tap(Circle circle)
    {
        if (!isGameActive) return;
        tappedTargets++;
        circle.gameObject.SetActive(false);

        if (IsFinished())
        {
            ProgressController.instance.UpdateProgress();
            Debug.Log("Current Value: " + ProgressController.instance.currentValue);

            if (ProgressController.instance.IsFinished())
            {
                StopGame();
            }

            GenerateNewPositions();
        }
        else
        {
            ResetTimer();
        }
    }


    public void GenerateNewPositions()
    {
        if (!isGameActive) return;
        tappedTargets = 0;

        foreach (Circle circle in activeCircles)
        {
            Vector3 spawnPosition;
            bool positionFound = false;

            for (int attempts = 0; attempts < 100; attempts++)
            {
                spawnPosition = new Vector3(
                    UnityEngine.Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
                    0,
                    UnityEngine.Random.Range(spawnAreaZMin, spawnAreaZMax));

                if (IsPositionValid(spawnPosition, circle.transform.localScale.x / 2))
                {
                    circle.transform.position = spawnPosition;
                    positionFound = true;
                    break;
                }
            }

            if (positionFound)
            {
                circle.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Не удалось найти подходящую позицию для круга после 100 попыток.");
            }
        }

        StartTimer();
    }

    private void StartTimer()
    {
        timer = timeLimit;
        isTimerRunning = true;
    }

    private void ResetTimer()
    {
        timer = timeLimit; 
    }

    private void ApplyPenalty()
    {
        ProgressController.instance.Penalty();
        Debug.Log("Penalty applied! Current Value: " + ProgressController.instance.currentValue);
        GenerateNewPositions();
    }

    private bool IsPositionValid(Vector3 position, float radius)
    {
        foreach (Circle circle in activeCircles)
        {
            if (circle.gameObject.activeSelf)
            {
                if (Vector3.Distance(position, circle.transform.position) < radius * 2)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsFinished()
    {
        return tappedTargets == targets;
    }
}
