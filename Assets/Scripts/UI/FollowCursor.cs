using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    // Store the position of the image
    private Vector3 imagePosition;

    // Store the position of the mouse
    private Vector3 mousePosition;

    void Start()
    {
        // Initialize the position of the image
        imagePosition = transform.position;
    }

    void Update()
    {
        // Get the current mouse position
        mousePosition = Input.mousePosition;

        // Update the position of the image to follow the cursor
        imagePosition.x = mousePosition.x;
        transform.position = imagePosition;
    }
}
