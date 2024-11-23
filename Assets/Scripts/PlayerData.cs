using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ScriptableObject
{
    public int currentHealth;
    public int maxHealth;
    public bool canDoubleJump;
    public int attackCoolDown;
    public int attackDamage;
    public int attackRange;
}
