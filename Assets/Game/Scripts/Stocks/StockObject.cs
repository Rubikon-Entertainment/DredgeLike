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

    //public void DestroyObject(){
    //    Destroy(gameObject);
    //}
    
    protected override void HandleInteraction(GameObject interactor)
    {
        if (!hasInteracted)
        {
            DisplayInfo();
            Debug.Log($"Interacted with {stock.name}");
            hasInteracted = true;
            

            if (stock is ResourceStock){
                switch(stock.name)
                {
                    case "wood":
                        InventoryController.instance.AddWood(stock.quantity);
                        break;
                    case "metal":
                        InventoryController.instance.AddMetal(stock.quantity);
                        break;
                    case "scrap":
                        InventoryController.instance.AddScraps(stock.quantity);
                        break;
                }
               // DestroyObject();
                return;
            }
            CameraController.Instance.ChangeTargetWithMode(gameObject.transform, "Stock");
            PlayerController.Instance.SweemToStock(gameObject.transform);
        }
    }
}