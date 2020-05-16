using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public GameObject cardPanel;

    public void A_CardPanelOpener(bool state)
    {
        cardPanel.GetComponent<Animator>().SetBool("isPanelOpen", state);
    }
}
