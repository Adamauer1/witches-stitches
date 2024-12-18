using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHolder : MonoBehaviour
{
    [SerializeField] private string crystalName; 
    [SerializeField] private GameObject crystalPrefab;
    private void Start()
    {
        Debug.Log(GameManager.instance.crystalCheck[crystalName]);
        if (GameManager.instance.crystalCheck[crystalName])
        {
            crystalPrefab.SetActive(true);
        }
    }
}
