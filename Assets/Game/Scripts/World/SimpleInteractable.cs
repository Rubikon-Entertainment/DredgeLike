using UnityEngine;
using UnityEngine.UI;

public class SimpleInteractable : BaseInteractable
{
    [Tooltip("UI Text element that will display the information")]
    public Text infoText;
    
    [TextArea(3, 5)]
    public string displayMessage = "This is an interactable object!";
    
    protected override void DisplayInfo()
    {
        if (infoText != null)
        {
            infoText.text = displayMessage;
            infoText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No info text UI element assigned to SimpleInteractable!");
        }
    }
    
    // Optional: Add a method to hide the text again
    public void HideInfo()
    {
        if (infoText != null)
        {
            infoText.gameObject.SetActive(false);
        }
    }
} 