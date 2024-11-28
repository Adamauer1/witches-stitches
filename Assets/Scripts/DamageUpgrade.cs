using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : MonoBehaviour, IPlayerUpgrade
{
    public void ApplyUpgrade(){
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.ApplyDamageUpgrade();
        GetComponentInParent<UpgradeController>().upgradeSelected = true;
    }
}
