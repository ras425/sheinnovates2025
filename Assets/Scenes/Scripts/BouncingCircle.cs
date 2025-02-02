using UnityEngine;
using System.Collections.Generic;

public class BouncingCircle : MonoBehaviour
{
    public float moveSpeed = 1f;
    public bool useCustomPolygon = false; // Flag to toggle custom polygon behavior
    public List<Vector2> polygonVertices = new List<Vector2>(); // Define polygon vertices in the Inspector

    private Vector2 direction;

    private const float SCREEN_BOUND_X = 8f;
    private const float SCREEN_BOUND_Y = 4f;

    void Start()
    {
        // Random direction to start
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Optionally initialize the polygon (for visualization)
        if (useCustomPolygon)
        {
            InitializePolygonVisualization();
        }
    }

    void Update()
    {
        // Move the circle
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Get the current position of the circle
        Vector2 position = transform.position;

        // Check if custom polygon boundaries should be used
        if (useCustomPolygon)
        {
            // Check if the circle is outside the polygon and bounce if necessary
            CheckScreenBoundsAndReflectDerm(position);
        }
        else
        {
            // Use default screen bounds if not using custom polygon
            CheckScreenBoundsAndReflect(position);
        }

        // Update position
        transform.position = position;
    }

    // Default screen boundary logic
    private void CheckScreenBoundsAndReflect(Vector2 position)
    {
        if (Mathf.Abs(position.x) >= SCREEN_BOUND_X)
        {
            direction.x = -direction.x;
            position.x = Mathf.Sign(position.x) * (SCREEN_BOUND_X);
        }
        if (Mathf.Abs(position.y) >= SCREEN_BOUND_Y)
        {
            direction.y = -direction.y;
            position.y = Mathf.Sign(position.y) * SCREEN_BOUND_Y;
        }
    }

    private void CheckScreenBoundsAndReflectDerm(Vector2 position)
    {
        if (Mathf.Abs(position.x) >= SCREEN_BOUND_X - 4)
        {
            direction.x = -direction.x;
            position.x = Mathf.Sign(position.x) * (SCREEN_BOUND_X - 4);
        }
        if (Mathf.Abs(position.y) >= SCREEN_BOUND_Y)
        {
            direction.y = -direction.y;
            position.y = Mathf.Sign(position.y) * SCREEN_BOUND_Y;
        }
    }

    // Check if the circle is outside the polygon using ray-casting or a point-in-polygon test
    private bool IsOutsidePolygon(Vector2 position)
    {
        int intersections = 0;
        int n = polygonVertices.Count;

        // Iterate over each edge of the polygon
        for (int i = 0; i < n; i++)
        {
            Vector2 vertex1 = polygonVertices[i];
            Vector2 vertex2 = polygonVertices[(i + 1) % n]; // Wrap around to the first point

            // Check if a ray from the point to the right intersects the edge
            if (IsRayIntersectingEdge(position, vertex1, vertex2))
            {
                intersections++;
            }
        }

        // If the number of intersections is odd, the point is inside the polygon
        return intersections % 2 == 0;
    }

    // Check if a ray from the point to the right intersects with the edge
    private bool IsRayIntersectingEdge(Vector2 point, Vector2 vertex1, Vector2 vertex2)
    {
        // Ensure vertex1 is below vertex2 to make the ray horizontal
        if (vertex1.y > vertex2.y)
        {
            Vector2 temp = vertex1;
            vertex1 = vertex2;
            vertex2 = temp;
        }

        // If the point is below or above the edge, no intersection
        if (point.y == vertex1.y || point.y == vertex2.y)
        {
            point.y += Mathf.Epsilon; // Slightly adjust to prevent horizontal edge overlap
        }

        if (point.y > vertex2.y || point.y < vertex1.y)
        {
            return false;
        }

        // Check if the point is to the left of the edge
        float xIntersection = vertex1.x + (point.y - vertex1.y) * (vertex2.x - vertex1.x) / (vertex2.y - vertex1.y);
        return point.x < xIntersection;
    }

    // Reflect the circle's movement off the polygon edge it crossed
    private void ReflectOffPolygon(Vector2 position)
    {
        int n = polygonVertices.Count;
        for (int i = 0; i < n; i++)
        {
            Vector2 edgeStart = polygonVertices[i];
            Vector2 edgeEnd = polygonVertices[(i + 1) % n];

            // If the circle crosses the edge, reflect off it
            if (IsIntersectingEdge(position, edgeStart, edgeEnd))
            {
                Vector2 edgeNormal = GetEdgeNormal(edgeStart, edgeEnd);
                direction = Vector2.Reflect(direction, edgeNormal);
                break; // Only reflect off the first edge the circle hits
            }
        }
    }

    // Calculate the normal of the edge (perpendicular vector)
    private Vector2 GetEdgeNormal(Vector2 edgeStart, Vector2 edgeEnd)
    {
        Vector2 edge = edgeEnd - edgeStart;
        return new Vector2(-edge.y, edge.x).normalized; // Perpendicular vector
    }

    // Check if the circle's path intersects with an edge
    private bool IsIntersectingEdge(Vector2 circlePosition, Vector2 edgeStart, Vector2 edgeEnd)
    {
        // Simplified check for now - needs to consider circle radius and the edge geometry
        // Placeholder for a more advanced intersection test
        float tolerance = 0.1f; // Tolerance for detection (adjust based on circle size)
        return Mathf.Abs(CrossProduct(edgeStart, edgeEnd, circlePosition)) < tolerance;
    }

    // Simple cross-product to determine which side of the edge the point is
    private float CrossProduct(Vector2 start, Vector2 end, Vector2 point)
    {
        return (end.x - start.x) * (point.y - start.y) - (end.y - start.y) * (point.x - start.x);
    }

    // Optional: Visualize the custom polygon boundaries (debugging)
    private void InitializePolygonVisualization()
    {
        // Use a LineRenderer to draw the polygon shape for visualization (optional)
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = polygonVertices.Count + 1; // Close the polygon
        lineRenderer.loop = true;

        for (int i = 0; i < polygonVertices.Count; i++)
        {
            lineRenderer.SetPosition(i, polygonVertices[i]);
        }

        // Connect last vertex to the first
        lineRenderer.SetPosition(polygonVertices.Count, polygonVertices[0]);
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
    }
}
