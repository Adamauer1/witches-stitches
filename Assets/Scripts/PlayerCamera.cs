using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private CinemachineVirtualCamera cvc;

    private void Awake(){
        cvc = GetComponent<CinemachineVirtualCamera>();
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        cvc.Follow = player;
    }
}
