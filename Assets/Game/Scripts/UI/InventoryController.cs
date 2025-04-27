using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour
{
    public static InventoryController instance;
    public int FishMax, RecourcesMax, WoodNow, MetalNow, ScrapsNow, Coins, UpgradeCount;
    public TMP_Text CoinCounterText, FishCounterText, WoodCounterText, ScrapsCounterText, MetalCounterText;

    public List<Fish> arrayFish;
    

    private void Awake()
    {
        if(instance == null )
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else{
            Destroy(gameObject);
        }
    }
    void Start() {
        UpgradeCount = 0;
        FishMax = 10 + UpgradeCount * 2;
        RecourcesMax = 3 + UpgradeCount * 2;
        WoodNow = 0;
        MetalNow = 0;
        ScrapsNow = 0;
        Coins = 0; 
        

        // arrayFish.push( fish = new Fish("NewFish", _, 10, 5, 35, _));   
        // Debug.Log(arrayFish[0]); 
    }

   public int GetWoodNow()
   {    return(WoodNow);   }

   public int GetMetalNow()
   {    return(MetalNow);   }

   public int GetScrapsNow()
   {    return(ScrapsNow);   }

   public int GetCoins()
   {    return(Coins);   }

   public void AddWood(int x)
   {    if (WoodNow < RecourcesMax) {
    WoodNow += x;
   }   }

   public void AddMetal(int x)
   {    if (MetalNow < RecourcesMax) {
    MetalNow += x;
   }   }

   public void AddScraps(int x)
   {    if (ScrapsNow < RecourcesMax) {
    ScrapsNow += x;
   }   }

   public void AddCoins(int x)
   {    
        Coins += x;
   }   

   public void Upgrade()
   {    
        UpgradeCount++;
   } 

   void Update() {
    FishMax = 10 + UpgradeCount * 2;
    RecourcesMax = 3 + UpgradeCount * 2;
    CoinCounterText.text = Coins.ToString();
    FishCounterText.text = 0.ToString() + " / " + FishMax.ToString();
    WoodCounterText.text = WoodNow.ToString() + " / " + RecourcesMax.ToString();
    ScrapsCounterText.text = ScrapsNow.ToString() + " / " + RecourcesMax.ToString();
    MetalCounterText.text = MetalNow.ToString() + " / " + RecourcesMax.ToString();
   }
}
