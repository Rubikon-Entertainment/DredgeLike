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
    [Tooltip("Text to show when no interactions are available")]
    public string unavailableText = "No Interaction";
    [Tooltip("Button color when interactions are available")]
    public Color activeColor = Color.white;
    [Tooltip("Button color when no interactions are available")]
    public Color inactiveColor = new Color(0.7f, 0.7f, 0.7f, 0.5f);

    private Button button;
    private GameObject player;
    private HashSet<BaseInteractable> availableInteractables = new HashSet<BaseInteractable>();
    private Image buttonImage;
    
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
        buttonImage = GetComponent<Image>();
        button.onClick.AddListener(HandleInteraction);
        
        // Assuming the player has a "Player" tag
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Always show button, but update its interactability state
        UpdateButtonState();
        
        if (buttonText != null)
        {
            buttonText.text = defaultText;
        }
    }
    
    void Update()
    {
        // Update button state based on available interactions
        UpdateButtonState();
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
            
            UpdateButtonState();
        }
    }
    
    void UpdateButtonState()
    {
        bool hasInteractions = availableInteractables.Count > 0;
        
        // Always show the button, but update its interactability
        button.interactable = hasInteractions;
        
        // Update button text
        if (buttonText != null)
        {
            buttonText.text = hasInteractions ? defaultText : unavailableText;
        }
        
        // Update button color
        if (buttonImage != null)
        {
            buttonImage.color = hasInteractions ? activeColor : inactiveColor;
        }
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
