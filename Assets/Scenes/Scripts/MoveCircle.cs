using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircle : MonoBehaviour
{
    public float moveSpeed = 10f; // Speed of the cursor movement
    public GameObject circlePrefab; // Reference to the bouncing circle prefab
    private List<GameObject> circles = new List<GameObject>(); // List to hold the circles

    void Start()
    {
        SpawnCircles(20); // Spawn 20 circles at the start
    }

    void Update()
    {
        // Cursor movement using arrow keys
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }

        // Check if the player presses Enter to explode nearby circle
        if (Input.GetKeyDown(KeyCode.Return)) {
            ExplodeNearbyCircle();
        }
    }

    // Spawn random circles at random positions on the screen
    void SpawnCircles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Random spawn position within screen bounds
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
            circles.Add(circle);
        }
    }

    // Check if the cursor is near any circle and destroy it when Enter is pressed
    void ExplodeNearbyCircle()
    {
        foreach (GameObject circle in circles)
        {
            if (Vector2.Distance(transform.position, circle.transform.position) < 1.0f)  // Distance threshold
            {
                Destroy(circle); // Destroy the circle
                break; // Destroy only one circle at a time
            }
        }
    }
}
