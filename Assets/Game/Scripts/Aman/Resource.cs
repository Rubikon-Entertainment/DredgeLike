using System.Collections;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 direction;
    private ResourceCatchingController resourceCatchingController;

    private void Start()
    {
        direction = Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;
        resourceCatchingController = ResourceCatchingController.instance;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (transform.position.x < -10 || transform.position.x > 10)
        {
            gameObject.SetActive(false);
        }
    }

    public void Catch()
    {
        Debug.Log("Resource caught: " + gameObject.name);
        //resourceCatchingController.CatchResource(gameObject);
        Drag();
    }

    public void Drag()
    {
        StartCoroutine(DragToHarpoon());
    }

    private IEnumerator DragToHarpoon()
    {
        Vector3 harpoonPosition = ResourceCatchingController.instance.transform.position;
        harpoonPosition.y = 0;
        while (Vector3.Distance(transform.position, harpoonPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, harpoonPosition, speed * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
