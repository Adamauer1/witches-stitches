using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOption : MonoBehaviour
{
    // public int upgrade = 100;
    public IPlayerUpgrade playerUpgrade;
    [SerializeField] UpgradeController upgradeController;


    public void SelectUpgrade(){
        
        // TextMeshProUGUI upgrade = GetComponentInChildren<TextMeshProUGUI>();
        // Upgrade upgrade = GetComponent<Upgrade>();
        // upgradeController.SelectUpgrade(playerUpgrade.upgrade);
        playerUpgrade.ApplyUpgrade();

    }
}
