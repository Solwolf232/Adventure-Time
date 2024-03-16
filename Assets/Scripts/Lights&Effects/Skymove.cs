using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skymove : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public float tileSizeX = 1.0f; // Width of one tile

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new offset based on time and speed
        float offset = Time.time * scrollSpeed;

        // Calculate the new position based on the offset
        float newPosition = Mathf.Repeat(offset, tileSizeX);

        // Move the object to create the scrolling effect
        transform.position = startPosition + Vector2.left * newPosition;
    }
}
