using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public bool upgradeSelected = false;

    [SerializeField] Transform[] panelPositions;
    [SerializeField] GameObject[] playerUpgradePanels;
    
    private void Start(){
        ActivatePanels();
    }

    public void SelectUpgrade(int upgrade){
        
        // TextMeshProUGUI upgrade = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(upgrade);
        upgradeSelected = true;
    }

    public void ActivatePanels(){
        // foreach (Transform panel in panelPositions){
        //     // panel.GetComponent<UpgradeOption>().playerUpgrade = playerUpgrades[0];
        //     Instantiate(playerUpgradePanels[0], panel.position, Quaternion.identity, panel);
        // }
        for (int i = 0; i < panelPositions.Length; i++){
            Instantiate(playerUpgradePanels[i], panelPositions[i].position, Quaternion.identity, panelPositions[i]);
        }
    }
}
