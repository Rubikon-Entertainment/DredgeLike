using UnityEngine;

public class BoatBuoyancy : MonoBehaviour
{
    public Transform[] floaters; // Points on the boat that interact with water
    public float waterLevel = 0f;
    public float buoyancyForce = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        foreach (Transform floater in floaters)
        {
            if (floater.position.y < waterLevel)
            {
                float force = Mathf.Abs(floater.position.y - waterLevel) * buoyancyForce;
                rb.AddForceAtPosition(Vector3.up * force, floater.position, ForceMode.Force);
            }
        }
    }
}