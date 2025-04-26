using System.Collections;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public void UpdateZPosition()
    {
        int currentValue = ProgressController.instance.currentValue;

        float zPosition = Mathf.Lerp(4f, 0f, (float)currentValue / ProgressController.instance.targetValue);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }
}
