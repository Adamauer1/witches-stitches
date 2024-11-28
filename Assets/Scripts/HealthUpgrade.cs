using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour, IPlayerUpgrade
{

    public void ApplyUpgrade(){
        PlayerHealth player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        player.maxHealth += 5;
        player.currentHealth += 5;
        player.UpdateHealthText();
        GetComponentInParent<UpgradeController>().upgradeSelected = true;
    }
}
