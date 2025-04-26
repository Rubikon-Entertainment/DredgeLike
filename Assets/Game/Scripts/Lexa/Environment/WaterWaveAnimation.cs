using UnityEngine;

public class WaterWaveAnimation : MonoBehaviour
{
    public Material waterMaterial;
    public float speed = 0.5f;

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, Time.time * speed);
        waterMaterial.SetTextureOffset("_MainTex", offset);
    }
}