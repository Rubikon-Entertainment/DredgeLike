using UnityEngine;

public class StockObject : BaseInteractable
{
    [Header("Stock Details")]
    public Stock stock; // The stock assigned to this object

    protected override void DisplayInfo()
    {
        if (stock != null)
        {
            Debug.Log($"Stock Name: {stock.name}, Quantity: {stock.quantity}");
        }
        else
        {
            Debug.LogWarning("StockObject has no assigned stock.");
        }
    }

    public void DestroyObject()
    {
        // Optionally trigger animations or events here
        Destroy(gameObject);
    }

    protected override void HandleInteraction(GameObject interactor)
    {
        if (!hasInteracted)
        {
            DisplayInfo();
            Debug.Log($"Interacted with {stock?.name ?? "Unknown Stock"}");

            hasInteracted = true;

            if (stock is ResourceStock resourceStock)
            {
                Debug.Log($"Processing interaction for {resourceStock.resourceType} with quantity {resourceStock.quantity}");
                switch (resourceStock.resourceType)
                {
                    case ResourceType.Wood:
                        InventoryController.instance?.AddWood(resourceStock.quantity);
                        break;
                    case ResourceType.Metal:
                        InventoryController.instance?.AddMetal(resourceStock.quantity);
                        break;
                    case ResourceType.Scrap:
                        InventoryController.instance?.AddScraps(resourceStock.quantity);
                        break;
                }
                DestroyObject();
                return;
            }

            // Handle camera and player controller logic
            if (CameraController.Instance != null)
            {
                CameraController.Instance.ChangeTargetWithMode(gameObject.transform, "Stock");
            }
            else
            {
                Debug.LogWarning("CameraController instance is not initialized.");
            }

            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.SweemToStock(gameObject.transform);
            }
            else
            {
                Debug.LogWarning("PlayerController instance is not initialized.");
            }
        }
        else
        {
            Debug.Log("Already interacted with this stock object.");
        }
    }
}