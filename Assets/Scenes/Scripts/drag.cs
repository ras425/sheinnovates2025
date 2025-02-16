using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private float zCoordinate;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    void OnMouseDown()
    {
        zCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() + offset;
        transform.position = newPosition;
        
        // Only update the line if the new position is different enough
        if (lineRenderer.positionCount == 0 || Vector3.Distance(newPosition, lineRenderer.GetPosition(lineRenderer.positionCount - 1)) > 0.1f)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
        }
    }

}
