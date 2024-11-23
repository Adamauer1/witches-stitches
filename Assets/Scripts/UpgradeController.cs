using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public bool upgradeSelected = false;

    [SerializeField] GameObject[] optionPanels;
    [SerializeField] GameObject[] playerUpgrades;
    [SerializeField] float[] test;
    
    public void SelectUpgrade(int upgrade){
        
        // TextMeshProUGUI upgrade = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(upgrade);
        upgradeSelected = true;
    }

    public void ActivatePanels(){
        foreach (var panel in optionPanels){
            // panel.GetComponent<UpgradeOption>().playerUpgrade = playerUpgrades[0];
            // Instantiate(playerUpgrades[0],optionPanels.po)
        }
    }
}
