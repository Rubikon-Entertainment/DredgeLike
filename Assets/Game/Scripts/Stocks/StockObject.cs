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
    
    protected override void HandleInteraction(GameObject interactor)
    {
        if (!hasInteracted)
        {
            DisplayInfo();
            Debug.Log($"Interacted with {gameObject.name}");
            hasInteracted = true;
            CameraController.Instance.ChangeTargetWithMode(gameObject.transform, "Stock");
            PlayerController.Instance.SweemToStock(gameObject.transform);
        }
    }
}