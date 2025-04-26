using UnityEngine;

[System.Serializable]
public class Stock
{
    public string name;
    public int quantity;
    public Sprite icon;

    public Stock(string name, int quantity, Sprite icon)
    {
        this.name = name;
        this.quantity = quantity;
        this.icon = icon;
    }
}