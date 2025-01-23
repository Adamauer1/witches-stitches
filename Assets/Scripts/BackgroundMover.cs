using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public Transform player;
    public float speed = 0.1f;
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        if (player == null)
        {
            return;
        }
        lastPlayerPosition = player.position;
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }
        
        Vector3 newPosition = player.position - lastPlayerPosition;
        transform.position += new Vector3(-(newPosition.x) * speed, 0, 0);
        lastPlayerPosition = player.position;
    }
}
