using UnityEngine;

public class UIHarbor : MonoBehaviour
{
    public GameObject harborSell, harborUpgrade, harborRelic;

    private string [] harborSellDialogueArray, harborUpgradeDialogueArray, harborRelicDialogueArray;
    public void RunHarborSell() {
        PanelOpener pn = harborSell.GetComponent<PanelOpener>();
        pn.OpenPanel();

    }

    public void RunHarborUpgrade() {
        PanelOpener pn = harborUpgrade.GetComponent<PanelOpener>();
        pn.OpenPanel();
    }

    public void RunHarborRelic() {
        PanelOpener pn = harborRelic.GetComponent<PanelOpener>();
        pn.OpenPanel();
    }

    public void CloseHarborSell() {
        PanelCloser pn = harborSell.GetComponent<PanelCloser>();
        pn.ClosePanel();
    }

    public void CloseHarborUpgrade() {
        PanelCloser pn = harborUpgrade.GetComponent<PanelCloser>();
        pn.ClosePanel();
    }

    public void CloseHarborRelic() {
        PanelCloser pn = harborRelic.GetComponent<PanelCloser>();
        pn.ClosePanel();
    }
}
