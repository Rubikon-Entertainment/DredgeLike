using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceStock : Stock
{
    public ResourceStock(string name, int quantity, Sprite icon) : base(name, quantity, icon)
    {
        
    }
}