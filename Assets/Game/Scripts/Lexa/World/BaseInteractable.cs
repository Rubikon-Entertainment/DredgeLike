using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    public GameObject interactionTarget;
    public KeyCode interactionKey = KeyCode.E;
    //public float radius = 4f;
    
    protected bool isInRange = false;
    protected GameObject currentInteractor = null;
    protected bool hasInteracted = false;
    
    protected virtual void Start()
    {
        // Make sure we have a collider for interaction
        SphereCollider col = GetComponent<SphereCollider>();
        if (col == null)
        {
            col = gameObject.AddComponent<SphereCollider>();
        }
        
        // Set as trigger for more reliable detection
        //col.isTrigger = true;
        //col.radius = radius;
        
        // Add Rigidbody to ensure collision detection works
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // Won't be affected by physics
        }
    }

    protected virtual void Update()
    {
        // Check for key press when in range
        if (isInRange && currentInteractor != null && Input.GetKeyDown(interactionKey))
        {
            HandleInteraction(currentInteractor);
        }
    }

    protected abstract void DisplayInfo();

    protected virtual void OnTriggerEnter(Collider other)
    {
        // If no specific target is set, interact with any object that has a Rigidbody
        if (interactionTarget == null)
        {
            if (other.attachedRigidbody != null)
            {
                EnterInteractionRange(other.gameObject);
            }
        }
        // Otherwise only interact with the specific target
        else if (other.gameObject == interactionTarget || 
                 (other.attachedRigidbody != null && other.attachedRigidbody.gameObject == interactionTarget))
        {
            EnterInteractionRange(interactionTarget);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        // Reset interaction state when object leaves
        if (interactionTarget == null)
        {
            if (other.attachedRigidbody != null)
            {
                ExitInteractionRange(other.gameObject);
            }
        }
        else if (other.gameObject == interactionTarget ||
                (other.attachedRigidbody != null && other.attachedRigidbody.gameObject == interactionTarget))
        {
            ExitInteractionRange(interactionTarget);
        }
    }

    protected virtual void EnterInteractionRange(GameObject interactor)
    {
        isInRange = true;
        currentInteractor = interactor;
        hasInteracted = false;
        Debug.Log($"{interactor.name} is in range. Press {interactionKey} to interact with {gameObject.name}.");
    }

    protected virtual void ExitInteractionRange(GameObject interactor)
    {
        isInRange = false;
        currentInteractor = null;
        hasInteracted = false;
        Debug.Log($"{interactor.name} left interaction range of {gameObject.name}.");
    }

    protected virtual void HandleInteraction(GameObject interactor)
    {
        if (!hasInteracted)
        {
            DisplayInfo();
            Debug.Log($"Interacted with {gameObject.name}");
            hasInteracted = true;
        }
    }
} 