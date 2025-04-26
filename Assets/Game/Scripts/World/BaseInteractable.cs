using UnityEngine;
using System;

public abstract class BaseInteractable : MonoBehaviour
{
    public GameObject interactionTarget;
    // Removing KeyCode dependency
    //public KeyCode interactionKey = KeyCode.E;
    //public float radius = 4f;
    
    protected bool isInRange = false;
    protected GameObject currentInteractor = null;
    protected bool hasInteracted = false;
    
    // Event that UI can subscribe to for showing/hiding the interaction button
    public static event Action<BaseInteractable, bool> OnInteractableStatusChanged;
    
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
        // Remove key press check - will be handled by UI button instead
        // if (isInRange && currentInteractor != null && Input.GetKeyDown(interactionKey))
        // {
        //     HandleInteraction(currentInteractor);
        // }
    }

    // Call this method from UI button
    public virtual void TriggerInteraction()
    {
        if (isInRange && currentInteractor != null)
        {
            HandleInteraction(currentInteractor);
        }
    }

    // Public method to check if an object can interact
    public bool CanInteract(GameObject potentialInteractor)
    {
        return isInRange && currentInteractor == potentialInteractor;
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
        Debug.Log($"{interactor.name} is in range. Use interaction button to interact with {gameObject.name}.");
        
        // Notify UI system that an interactable is available
        OnInteractableStatusChanged?.Invoke(this, true);
    }

    protected virtual void ExitInteractionRange(GameObject interactor)
    {
        isInRange = false;
        currentInteractor = null;
        hasInteracted = false;
        Debug.Log($"{interactor.name} left interaction range of {gameObject.name}.");
        
        // Notify UI system that an interactable is no longer available
        OnInteractableStatusChanged?.Invoke(this, false);
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