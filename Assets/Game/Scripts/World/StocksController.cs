using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StocksController : MonoBehaviour
{
    public static StocksController Instance { get; private set; }

    [Header("Spawn Settings")]
    [SerializeField] private List<Stock> resources;
    [SerializeField] private List<Stock> fishes;
    [SerializeField] private int maxStocks = 10;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float minDistanceFromPlayer = 5f;

    [Header("Timers")]
    [SerializeField] private float spawnInterval = 10f;
    private float timer = 0f;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private GameObject resourcePrefab;

    private List<GameObject> spawnedStocks = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ValidateReferences();
    }

    private void OnDestroy()
    {
        // Clean up spawned objects when this controller is destroyed
        CleanupSpawnedStocks();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && spawnedStocks.Count < maxStocks)
        {
            SpawnStock();
            timer = 0f;
        }

        // Clean up any null references in the list (destroyed objects)
        RemoveNullReferences();
    }

    private void ValidateReferences()
    {
        if (player == null)
        {
            Debug.LogError("StocksController: Player reference is missing!");
        }
        
        if (fishPrefab == null)
        {
            Debug.LogError("StocksController: Fish prefab is missing!");
        }
        
        if (resourcePrefab == null)
        {
            Debug.LogError("StocksController: Resource prefab is missing!");
        }

        if (resources == null || resources.Count == 0)
        {
            Debug.LogWarning("StocksController: No resources defined!");
        }

        if (fishes == null || fishes.Count == 0)
        {
            Debug.LogWarning("StocksController: No fishes defined!");
        }
    }

    private void SpawnStock()
    {
        // Check if we have valid data to spawn
        if ((resources == null || resources.Count == 0) && 
            (fishes == null || fishes.Count == 0))
        {
            Debug.LogWarning("StocksController: Cannot spawn stocks - no resources or fishes defined.");
            return;
        }

        // Determine which type to spawn based on available options
        bool canSpawnResources = resources != null && resources.Count > 0;
        bool canSpawnFish = fishes != null && fishes.Count > 0;
        
        StockType type;
        if (canSpawnResources && canSpawnFish)
        {
            type = Random.value > 0.5f ? StockType.Fish : StockType.Resource;
        }
        else if (canSpawnResources)
        {
            type = StockType.Resource;
        }
        else
        {
            type = StockType.Fish;
        }

        Stock randomStock = null;
        GameObject prefab = null;

        switch (type)
        {
            case StockType.Fish:
                randomStock = fishes[Random.Range(0, fishes.Count)];
                prefab = fishPrefab;
                break;

            case StockType.Resource:
                randomStock = resources[Random.Range(0, resources.Count)];
                prefab = resourcePrefab;
                break;
        }

        if (prefab == null || randomStock == null)
        {
            Debug.LogWarning($"StocksController: Cannot spawn {type} - missing prefab or stock data.");
            return;
        }

        Vector3 spawnPosition = GetRandomSpawnPosition();
        if (spawnPosition == Vector3.zero)
        {
            Debug.LogWarning("StocksController: Could not find a valid spawn position.");
            return;
        }

        GameObject newStock = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
        StockObject stockObject = newStock.GetComponent<StockObject>();
        if (stockObject != null)
        {
            stockObject.stock = randomStock;
        }
        else
        {
            Debug.LogError($"StocksController: {prefab.name} does not have a StockObject component!");
        }

        spawnedStocks.Add(newStock);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        if (player == null)
        {
            Debug.LogError("StocksController: Cannot get spawn position - player reference is missing!");
            return Vector3.zero;
        }

        const int MAX_ATTEMPTS = 30;
        Vector3 randomPosition;
        int attempts = 0;
        
        do
        {
            randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPosition.y = transform.position.y; // Keep on the same y-plane
            attempts++;
            
            if (attempts >= MAX_ATTEMPTS)
            {
                return Vector3.zero; // Could not find a valid position
            }
        }
        while (Vector3.Distance(randomPosition, player.position) < minDistanceFromPlayer);

        return randomPosition;
    }
    
    private void RemoveNullReferences()
    {
        spawnedStocks = spawnedStocks.Where(stock => stock != null).ToList();
    }
    
    private void CleanupSpawnedStocks()
    {
        foreach (var stock in spawnedStocks)
        {
            if (stock != null)
            {
                Destroy(stock);
            }
        }
        spawnedStocks.Clear();
    }
}