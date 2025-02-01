using UnityEngine;

public class BouncingCircle : MonoBehaviour
{
    public float moveSpeed = 1f; // Slower speed for circles (change to your preferred speed)
    private Vector2 direction; // Direction the circle is moving in

    void Start()
    {
        // Assign a random direction for the circle's movement
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        // Move the circle in the given direction
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Bounce off the screen edges (assuming the camera is orthographic)
        if (transform.position.x <= -8f || transform.position.x >= 8f) {
            direction.x = -direction.x; // Reverse horizontal direction
        }
        if (transform.position.y <= -4f || transform.position.y >= 4f) {
            direction.y = -direction.y; // Reverse vertical direction
        }
    }
}
