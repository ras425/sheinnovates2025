using UnityEngine;

public class BouncingCircle : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Vector2 direction;
    private const float SCREEN_BOUND_X = 8f;
    private const float SCREEN_BOUND_Y = 4f;

    void Start()
    {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        // Move the circle
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Get current position
        Vector2 position = transform.position;

        // Bounce off screen edges
        if (Mathf.Abs(position.x) >= SCREEN_BOUND_X)
        {
            direction.x = -direction.x;
            // Clamp position to prevent getting stuck outside bounds
            position.x = Mathf.Sign(position.x) * SCREEN_BOUND_X;
        }
        if (Mathf.Abs(position.y) >= SCREEN_BOUND_Y)
        {
            direction.y = -direction.y;
            // Clamp position to prevent getting stuck outside bounds
            position.y = Mathf.Sign(position.y) * SCREEN_BOUND_Y;
        }

        // Update position
        transform.position = position;
    }
}