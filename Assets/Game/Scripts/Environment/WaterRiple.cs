using UnityEngine;

public class WaterRipple : MonoBehaviour
{
    public ParticleSystem rippleEffect;

    public void CreateRipple(Vector3 position)
    {
        rippleEffect.transform.position = position;
        rippleEffect.Play();
    }
}