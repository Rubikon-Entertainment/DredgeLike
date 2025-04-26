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
}