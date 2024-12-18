using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class FlappySpawner : MonoBehaviour
{
    [SerializeField] private FlappyPipes flappyPipes;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float minHeight = -1f;
    [SerializeField] private float maxHeight = 2f;
    [SerializeField] private float verticalGap = 3f;
    [SerializeField] private float spawnDelay = 5f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnDelay, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        FlappyPipes pipes = Instantiate(flappyPipes, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        pipes.gap = verticalGap;
    }
}
