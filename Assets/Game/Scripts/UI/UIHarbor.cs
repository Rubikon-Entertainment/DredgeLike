using UnityEngine;

public class UIHarbor : MonoBehaviour
{
    // Singleton instance
    public static UIHarbor Instance { get; private set; }

    public GameObject gameMenu;

    // Serialized fields for UI panels
    [SerializeField] private GameObject harborSell;
    [SerializeField] private GameObject harborUpgrade;
    [SerializeField] private GameObject harborRelic;

    // Dialogue arrays (if needed)
    private string[] harborSellDialogueArray, harborUpgradeDialogueArray, harborRelicDialogueArray;

    // Ensure only one instance of UIHarbor exists
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep the object alive across scenes
        }
        else
        {
            Debug.LogWarning("Multiple instances of UIHarbor detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    // Open Harbor Sell Panel
    public void RunHarborSell()
    {
        if (harborSell != null)
        {
            PanelOpener pn = harborSell.GetComponent<PanelOpener>();
            pn?.OpenPanel();
        }
        else
        {
            Debug.LogWarning("Harbor Sell panel is not assigned.");
        }
    }

    // Open Harbor Upgrade Panel
    public void RunHarborUpgrade()
    {
        if (harborUpgrade != null)
        {
            PanelOpener pn = harborUpgrade.GetComponent<PanelOpener>();
            pn?.OpenPanel();
        }
        else
        {
            Debug.LogWarning("Harbor Upgrade panel is not assigned.");
        }
    }

    // Open Harbor Relic Panel
    public void RunHarborRelic()
    {
        if (harborRelic != null)
        {
            PanelOpener pn = harborRelic.GetComponent<PanelOpener>();
            pn?.OpenPanel();
        }
        else
        {
            Debug.LogWarning("Harbor Relic panel is not assigned.");
        }
    }

    // Close Harbor Sell Panel
    public void CloseHarborSell()
    {
        if (harborSell != null)
        {
            PanelCloser pn = harborSell.GetComponent<PanelCloser>();
            pn?.ClosePanel();
        }
        else
        {
            Debug.LogWarning("Harbor Sell panel is not assigned.");
        }
    }

    // Close Harbor Upgrade Panel
    public void CloseHarborUpgrade()
    {
        if (harborUpgrade != null)
        {
            PanelCloser pn = harborUpgrade.GetComponent<PanelCloser>();
            pn?.ClosePanel();
        }
        else
        {
            Debug.LogWarning("Harbor Upgrade panel is not assigned.");
        }
    }

    // Close Harbor Relic Panel
    public void CloseHarborRelic()
    {
        if (harborRelic != null)
        {
            PanelCloser pn = harborRelic.GetComponent<PanelCloser>();
            pn?.ClosePanel();
        }
        else
        {
            Debug.LogWarning("Harbor Relic panel is not assigned.");
        }
    }

    public void StartHarborInteraction(string interactionType)
    {
        if(gameMenu != null)
        {
            gameMenu.SetActive(false);
        }
        
        switch(interactionType.ToLower())
        {
            case "relic":
                RunHarborRelic();
                break;
            case "sell":
                RunHarborSell();
                break;
            case "upgrade":
                RunHarborUpgrade();
                break;
            default:
                Debug.LogWarning("Unknown harbor interaction type: " + interactionType);
                break;
        }
    }

    public void SellAll(){

    }

    public void Upgrade(){
        PlayerController.Instance.SweemSpeedLevelUp();
    }


    
}