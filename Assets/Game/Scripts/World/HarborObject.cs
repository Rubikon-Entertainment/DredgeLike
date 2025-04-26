using UnityEngine;

public class HarborObject : BaseInteractable
{
    [Header("Harbor Details")]
    public Transform harborTarget;
    [SerializeField] private string harborName;
    [SerializeField] private string harborDescription;
    
    protected override void DisplayInfo()
    {
        Debug.Log($"Harbor Name: {harborName ?? gameObject.name}");
        Debug.Log($"Description: {harborDescription}");
    }

    protected override void HandleInteraction(GameObject interactor)
    {
        if (!hasInteracted)
        {
            DisplayInfo();
            Debug.Log($"Interacted with {gameObject.name}");
            hasInteracted = true;
            CameraController.Instance.ChangeTargetWithMode(harborTarget, "Harbor");
            PlayerController.Instance.SweemToHarbor(harborTarget);
        }
    }
}
