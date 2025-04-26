using UnityEngine;

public class Circle : MonoBehaviour
{
    public TappingController tappingController;

    private void Start()
    {
        tappingController = TappingController.instance;
    }

    private void OnMouseDown() 
    {
        tappingController.Tap(this);
    }
}
