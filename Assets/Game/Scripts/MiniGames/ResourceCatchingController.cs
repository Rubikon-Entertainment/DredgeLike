using System.Collections.Generic;
using UnityEngine;

public class ResourceCatchingController : MonoBehaviour
{
    public static ResourceCatchingController instance { get; private set; }

    public Harpoon harpoonPrefab;
    public Resource resourcePrefab;
    public int numberOfResources = 4;
    public float resourceSpeed = 2f;
    public float spawnAreaWidth = 5f;
    public float spawnAreaHeight = 5f;

    private List<Resource> activeResources = new List<Resource>();
    private int caughtResources = 0;
    private Harpoon currentHarpoon;

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
        InitializeResources();
        InitializeHarpoon();
        GenerateNewPositions();
    }

    private void InitializeResources()
    {
        for (int i = 0; i < numberOfResources; i++)
        {
            Resource resource = Instantiate(resourcePrefab);
            resource.gameObject.SetActive(false);
            activeResources.Add(resource);
        }
        GenerateNewPositions();
    }

    private void InitializeHarpoon()
    {
        currentHarpoon = Instantiate(harpoonPrefab, new Vector3(0, 0, -2), Quaternion.identity);
        currentHarpoon.gameObject.SetActive(true);
    }

    public void GenerateNewPositions()
    {
        foreach (Resource resource in activeResources)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
                0,
                Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2)
            );

            resource.transform.position = spawnPosition;
            resource.gameObject.SetActive(true);
        }
    }

    public void CatchResource(Resource resource)
    {
        caughtResources++;

        if (IsFinished())
        {
            GenerateNewPositions();
        }
    }

    public bool IsFinished()
    {
        return caughtResources == numberOfResources;
    }

    public void ShootHarpoon(Vector3 direction)
    {
        if (currentHarpoon != null)
        {
            currentHarpoon.Shoot(direction);
        }
    }
}
