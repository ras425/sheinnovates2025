using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircle : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject circlePrefab;
    private List<GameObject> circles = new List<GameObject>();
    public float explosionRadius = 3.0f;

    void Start()
    {
        SpawnCircles(10);
    }

    void Update()
    {
        // Cursor movement using arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Check if the player presses Space to explode nearby circle
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExplodeNearbyCircle();
        }

        // Clean up null entries and check for game end
        CleanupAndCheckGameEnd();
    }

    void SpawnCircles(int count)
    {
        circles.Clear(); // Clear any existing circles
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
            circle.name = $"Circle_{i}"; // Give each circle a unique name
            circles.Add(circle);
        }
        Debug.Log($"Spawned {count} circles");
    }

    void ExplodeNearbyCircle()
    {
        Vector2 cursorWorldPos = transform.position;
        GameObject nearestCircle = null;
        float nearestDistance = float.MaxValue;

        // Find the nearest circle within explosion radius
        foreach (GameObject circle in new List<GameObject>(circles)) // Create a copy of the list to iterate
        {
            if (circle != null)
            {
                float distance = Vector2.Distance(cursorWorldPos, circle.transform.position);
                if (distance <= explosionRadius && distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestCircle = circle;
                }
            }
        }

        // Destroy the nearest circle if found
        if (nearestCircle != null)
        {
            Debug.Log($"Destroying circle '{nearestCircle.name}' at position: {nearestCircle.transform.position}");
            circles.Remove(nearestCircle);
            Destroy(nearestCircle);
        }
        else
        {
            Debug.Log($"No circle found within explosion radius {explosionRadius} at position {cursorWorldPos}");
        }
    }

    void CleanupAndCheckGameEnd()
    {
        // Remove null entries
        int beforeCount = circles.Count;
        circles.RemoveAll(circle => circle == null);
        int afterCount = circles.Count;
        
        if (beforeCount != afterCount)
        {
            Debug.Log($"Cleaned up {beforeCount - afterCount} null entries. Remaining circles: {afterCount}");
        }

        // Check if game is complete
        if (circles.Count == 0)
        {
            Debug.Log("All circles destroyed! Game Complete!");
        }
    }

    void PrintCircleInfo()
    {
        Debug.Log($"=== Circle Status ===");
        Debug.Log($"Total circles in list: {circles.Count}");
        for (int i = 0; i < circles.Count; i++)
        {
            if (circles[i] != null)
            {
                Debug.Log($"Circle {i}: {circles[i].name} at position {circles[i].transform.position}");
            }
            else
            {
                Debug.Log($"Circle {i}: NULL REFERENCE");
            }
        }
    }

    void DestroyAllCircles()
    {
        Debug.Log("Destroying all remaining circles");
        foreach (GameObject circle in new List<GameObject>(circles))
        {
            if (circle != null)
            {
                circles.Remove(circle);
                Destroy(circle);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw explosion radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);

        // Draw lines to all active circles
        if (circles != null)
        {
            Gizmos.color = Color.yellow;
            foreach (GameObject circle in circles)
            {
                if (circle != null)
                {
                    Gizmos.DrawLine(transform.position, circle.transform.position);
                }
            }
        }
    }
}