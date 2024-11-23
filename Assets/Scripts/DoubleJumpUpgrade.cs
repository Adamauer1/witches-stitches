using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUpgrade : ScriptableObject, IPlayerUpgrade
{
    public string upgradeLabel = "Double Jump";

    public void ApplyUpgrade(){
        Debug.Log(upgradeLabel);
    }
}
