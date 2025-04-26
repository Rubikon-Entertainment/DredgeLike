using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]
public class InteractionButton : MonoBehaviour
{
    [Tooltip("Text to display on the interaction button")]
    public Text buttonText;
    [Tooltip("Default text to show on the button")]
    public string defaultText = "Interact";

    private Button button;
    private GameObject player;
    private HashSet<BaseInteractable> availableInteractables = new HashSet<BaseInteractable>();
    
    void OnEnable()
    {
        BaseInteractable.OnInteractableStatusChanged += HandleInteractableStatusChanged;
    }
    
    void OnDisable()
    {
        BaseInteractable.OnInteractableStatusChanged -= HandleInteractableStatusChanged;
    }
    
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleInteraction);
        
        // Assuming the player has a "Player" tag
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Hide button by default
        gameObject.SetActive(false);
        
        if (buttonText != null)
        {
            buttonText.text = defaultText;
        }
    }
    
    void Update()
    {
        // Update button visibility based on available interactions
        UpdateButtonVisibility();
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
            
            UpdateButtonVisibility();
        }
    }
    
    void UpdateButtonVisibility()
    {
        // Only show the button if there are available interactables
        gameObject.SetActive(availableInteractables.Count > 0);
    }
    
    void HandleInteraction()
    {
        BaseInteractable interactable = FindNearestInteractable();
        if (interactable != null)
        {
            interactable.TriggerInteraction();
        }
    }
    
    BaseInteractable FindNearestInteractable()
    {
        if (availableInteractables.Count == 0) return null;
        
        // Simple implementation - just returns the first available interactable
        // Could be improved to find the nearest one based on distance
        foreach (BaseInteractable interactable in availableInteractables)
        {
            if (interactable.CanInteract(player))
            {
                return interactable;
            }
        }
        
        return null;
    }
} 