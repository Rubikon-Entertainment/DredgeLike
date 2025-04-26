using System.Collections.Generic;
using UnityEngine;

public class TappingController : MonoBehaviour
{
    public static TappingController instance { get; private set; }
    public Circle circlePrefab;
    public int tappedTargets;
    public int targets = 4;
    public float spawnAreaWidth = 5f;
    public float spawnAreaHeight = 5f;

    private List<Circle> activeCircles = new List<Circle>();

    private void Awake()
    {
        Singleton();
        InitializeCircles();
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

    private void InitializeCircles()
    {
        for (int i = 0; i < targets; i++)
        {
            Circle newCircle = Instantiate(circlePrefab);
            newCircle.gameObject.SetActive(false);
            activeCircles.Add(newCircle);
        }
    }

    public void Tap(Circle circle)
    {
        tappedTargets++;
        circle.gameObject.SetActive(false);

        if (IsFinished())
        {
            GenerateNewPositions();
        }
    }

    public void GenerateNewPositions()
    {
        tappedTargets = 0;

        foreach (Circle circle in activeCircles)
        {
            Vector3 spawnPosition;
            bool positionFound = false;

            // Пытаемся найти подходящую позицию
            for (int attempts = 0; attempts < 100; attempts++)
            {
                spawnPosition = new Vector3(
                    UnityEngine.Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
                    0,
                    UnityEngine.Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2));

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
