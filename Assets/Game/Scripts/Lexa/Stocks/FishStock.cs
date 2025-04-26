using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishStock : Stock
{
    public string fishType; // Example: Type of fish (e.g., "Salmon", "Tuna")
    public bool isRare;     // Example: Indicates if the fish is rare

    // Constructor for initializing FishStock
    public FishStock(string name, int quantity, Sprite icon, string fishType, bool isRare)
        : base(name, quantity, icon) // Call the base class constructor
    {
        this.fishType = fishType;
        this.isRare = isRare;
    }
}