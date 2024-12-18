using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerUpgrade
{
    public float GetUpgradeCost();
    public void ApplyUpgrade();
}
