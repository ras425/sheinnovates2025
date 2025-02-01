using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircle : MonoBehaviour
{
    public float moveSpeed = 10f; // Speed of the cursor movement
    public GameObject circlePrefab; // Reference to the bouncing circle prefab
    private List<GameObject> circles = new List<GameObject>(); // List to hold the circles
    public float explosionRadius = 3.0f; // Increased explosion radius for easier detection

    void Start()
    {
        SpawnCircles(10); // Spawn 10 circles at the start
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
            // Random spawn position within screen bounds (world coordinates)
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
            circles.Add(circle);
        }
    }

    // Check if the cursor is near any circle and destroy it when Enter is pressed
    void ExplodeNearbyCircle()
    {
        // Store destroyed circles to remove them later
        List<GameObject> destroyedCircles = new List<GameObject>();

        // Iterate backward through the list to avoid skipping
        for (int i = circles.Count - 1; i >= 0; i--)
        {
            GameObject circle = circles[i];

            // If the circle is null (destroyed), skip it
            if (circle == null)
                continue;

            // Get the cursor position in world space
            Vector2 cursorWorldPos = transform.position;

            // Calculate the distance between the cursor and the circle
            float distanceToCursor = Vector2.Distance(cursorWorldPos, circle.transform.position);

            // If the distance is less than or equal to the explosion radius, destroy the circle
            if (distanceToCursor <= explosionRadius)
            {
                Debug.Log("Circle Exploded at: " + circle.transform.position);
                Destroy(circle); // Destroy the circle
                destroyedCircles.Add(circle); // Add to the list for later cleanup
            }
        }

        // Remove destroyed circles from the list after the loop
        foreach (GameObject destroyedCircle in destroyedCircles)
        {
            circles.Remove(destroyedCircle);
        }
    }
}
