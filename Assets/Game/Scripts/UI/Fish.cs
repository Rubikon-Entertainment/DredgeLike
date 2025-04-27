using System;
using System.Collections;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Fish", menuName = "Scriptable Objects/Fish")]
public class Fish : ScriptableObject
{
    public string fishName;
    public GameObject prefab;
    public float weight;
    public int rarity, rewardAmount;
    public AudioClip catchSound;

    public Fish()
    {
        
    }

    public int GetPrice() {
        return((int)(weight * rewardAmount));
    }
}
