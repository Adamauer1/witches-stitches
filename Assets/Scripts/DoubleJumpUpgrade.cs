using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUpgrade : MonoBehaviour, IPlayerUpgrade
{
    public string upgradeLabel = "Double Jump";

    public void ApplyUpgrade(){
        Debug.Log(upgradeLabel);
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.SetDoubleJump(true);
        GetComponentInParent<UpgradeController>().upgradeSelected = true;
    }
}
