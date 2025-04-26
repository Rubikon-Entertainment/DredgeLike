using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "Scriptable Objects/Fish")]
public class Fish : ScriptableObject
{
    string fishName;
    Sprite sprite;
    float weight;
    int rarity, rewardAmount;
    AudioClip catchSound;

    public int GetPrice() {
        return((int)(weight * rewardAmount));
    }
}
