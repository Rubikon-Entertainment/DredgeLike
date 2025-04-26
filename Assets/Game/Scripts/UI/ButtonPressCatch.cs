using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonPressCatch : MonoBehaviour
{
    public Button interactionButton;
    
    // HashSet to store all available interactables
    private HashSet<BaseInteractable> availableInteractables = new HashSet<BaseInteractable>();
    private GameObject player;

    void OnEnable()
    {
        // Subscribe to the interactable status change event
        BaseInteractable.OnInteractableStatusChanged += HandleInteractableStatusChanged;
    }
    
    void OnDisable()
    {
        // Unsubscribe when disabled
        BaseInteractable.OnInteractableStatusChanged -= HandleInteractableStatusChanged;
    }

    void Start()
    {
        // Make sure we have references
        if (interactionButton == null)
        {
            interactionButton = GetComponent<Button>();
        }

        // Find player reference (assuming it has a "Player" tag)
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Add the onClick listener
        if (interactionButton != null)
        {
            interactionButton.onClick.AddListener(OnInteractionButtonClicked);
        }
        
        // Initially hide the button until interactables are in range
        if (interactionButton != null)
        {
            interactionButton.gameObject.SetActive(false);
        }
    }
    
    void HandleInteractableStatusChanged(BaseInteractable interactable, bool isAvailable)
    {
        if (interactable != null)
        {
            if (isAvailable && interactable.CanInteract(player))
            {
                availableInteractables.Add(interactable);
            }
            else
            {
                availableInteractables.Remove(interactable);
            }
            
            // Update button visibility based on available interactables
            UpdateButtonVisibility();
        }
    }
    
    void UpdateButtonVisibility()
    {
        if (interactionButton != null)
        {
            // Only show the button if there are available interactables
            interactionButton.gameObject.SetActive(availableInteractables.Count > 0);
        }
    }

    void OnInteractionButtonClicked()
    {
        // Interact with all available interactables
        foreach (BaseInteractable interactable in new HashSet<BaseInteractable>(availableInteractables))
        {
            if (interactable != null)
            {
                interactable.TriggerInteraction();
            }
        }
    }
}