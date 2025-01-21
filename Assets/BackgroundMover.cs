using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera.
    public float moveSpeed = 5f; // Speed at which the image moves.
    public Vector2 mapBoundsMin; // Minimum (x, y) bounds of the map.
    public Vector2 mapBoundsMax; // Maximum (x, y) bounds of the map.

    private Vector3 targetPosition;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Start by aligning the image behind the camera.
        AlignBehindCamera();
    }

    void Update()
    {
        MoveImage();
    }

    void AlignBehindCamera()
    {
        // Set the initial position of the image slightly behind the camera.
        Vector3 cameraPosition = mainCamera.transform.position;
        targetPosition = new Vector3(cameraPosition.x, cameraPosition.y - 1, transform.position.z);
        transform.position = targetPosition;
    }

    void MoveImage()
    {
        // Move the image downwards based on the moveSpeed.
        Vector3 newPosition = transform.position;
        newPosition.y -= moveSpeed * Time.deltaTime;

        // Clamp the position to ensure it stays within map bounds.
        newPosition.x = Mathf.Clamp(newPosition.x, mapBoundsMin.x, mapBoundsMax.x);
        newPosition.y = Mathf.Clamp(newPosition.y, mapBoundsMin.y, mapBoundsMax.y);

        // Update the position.
        transform.position = newPosition;
    }
}
