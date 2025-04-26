using UnityEngine;

public class ResourceCatchingTest : MonoBehaviour
{

    private ResourceCatchingController resourceCatchingController;

    void Start()
    {
        resourceCatchingController = ResourceCatchingController.instance;
        resourceCatchingController.GenerateNewPositions();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootAtMousePosition();
        }
    }

    private void ShootAtMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = (worldPosition - resourceCatchingController.transform.position).normalized;
        direction.y = 0;
        resourceCatchingController.ShootHarpoon(direction);
    }


}
