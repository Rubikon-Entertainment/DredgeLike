using UnityEngine;

public class TappingTest : MonoBehaviour
{
    private TappingController tappingController;


    void Start()
    {
        tappingController = TappingController.instance;
        tappingController.GenerateNewPositions();
    }

}
