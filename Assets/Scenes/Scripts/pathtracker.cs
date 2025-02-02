/*using UnityEngine;
using UnityEngine.SceneManagement;

public class PathTracker : MonoBehaviour
{
    public string nextSceneName;
    private bool isFollowingPath = false;
    private int totalCollisions = 0;
    private int requiredCollisions;
    private bool sceneLoaded = false;  // Flag to check if the scene has already been loaded

    void Start()
    {
        Debug.Log("start");
        requiredCollisions = 5;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "needle")
        {
            isFollowingPath = true;
            totalCollisions++;
            Debug.Log("Entered Path Segment");
        }
        Debug.Log("test");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "needle")
        {
            isFollowingPath = false;
            Debug.Log("Exited Path Segment");
        }
    }

    void Update()
    {
        Debug.Log(totalCollisions);
        if (totalCollisions >= requiredCollisions && isFollowingPath && !sceneLoaded)
        {
            Debug.Log("Path Completed Successfully");
            sceneLoaded = true;  // Set flag to true to prevent multiple loading
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        Debug.Log("next");
        SceneManager.LoadScene(nextSceneName);
    }
}*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class PathTracker : MonoBehaviour
{
    public string nextSceneName;
    public GameObject needle;
    private LineRenderer pathLineRenderer;  // This is your dotted line renderer
    private LineRenderer needleLineRenderer;
    private int requiredMatches = 5; // Adjust this based on your path length and accuracy requirements
    private int matchCount = 0;
    private bool sceneLoaded = false;

    void Start()
    {
        if (needle != null) 
        {
            needleLineRenderer = needle.GetComponent<LineRenderer>();
        }
        pathLineRenderer = GetComponent<LineRenderer>();

        if (pathLineRenderer == null || needleLineRenderer == null) 
        {
            Debug.LogError("LineRenderer components are not properly assigned!");
        }

        Debug.Log("Start");
    }

/*
    void Update()
    {
        if (pathLineRenderer != null && needleLineRenderer != null)
        {
            CheckPathAlignment();
            if (matchCount >= requiredMatches && !sceneLoaded)
            {
                Debug.Log("Path Completed Successfully");
                sceneLoaded = true;
                LoadNextScene();
            }
        }
    }

    void CheckPathAlignment()
    {
    matchCount = 0;

    Debug.Log($"Path Points: {pathLineRenderer.positionCount}, Needle Points: {needleLineRenderer.positionCount}");

    // Check if positions are calculated and list a few
    if (needleLineRenderer.positionCount > 0 && pathLineRenderer.positionCount > 0) {
        Debug.Log($"Sample Needle Position: {needleLineRenderer.GetPosition(0)}");
        Debug.Log($"Sample Path Position: {pathLineRenderer.GetPosition(0)}");
    }

    for (int i = 0; i < needleLineRenderer.positionCount; i++)
    {
        for (int j = 0; j < pathLineRenderer.positionCount; j++)
        {
            float distance = Vector3.Distance(needleLineRenderer.GetPosition(i), pathLineRenderer.GetPosition(j));
            if (i < 10 && j < 10) {  // Limiting to first 10 comparisons
                Debug.Log($"Distance between Needle [{i}] and Path [{j}]: {distance}");
            }

            if (distance < 0.1f)  // Adjust this threshold if necessary
            {
                matchCount++;
                break;  // Breaks out of the inner loop if a close enough match is found
            }
        }
    }

    Debug.Log($"Current Matches: {matchCount}");
    }
*/

void Update()
{
    if (pathLineRenderer != null && needleLineRenderer != null)
    {
        CheckPathAlignment();
    }
}

void CheckPathAlignment()
{
    matchCount = 0;

    for (int i = 0; i < needleLineRenderer.positionCount; i++)
    {
        Vector3 needlePointWorld = needleLineRenderer.transform.TransformPoint(needleLineRenderer.GetPosition(i));

        for (int j = 0; j < pathLineRenderer.positionCount; j++)
        {
            Vector3 pathPointWorld = pathLineRenderer.transform.TransformPoint(pathLineRenderer.GetPosition(j));
            float distance = Vector3.Distance(needlePointWorld, pathPointWorld);

            if (distance < 0.1f)  // Adjust this threshold as necessary
            {
                matchCount++;
                break;  // Break after first match found to avoid duplicates
            }
        }
    }

    Debug.Log($"Current Matches: {matchCount}");
}


    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
        Debug.Log("Loading Next Scene");
    }


    void OnDrawGizmos()
    {
        if (needleLineRenderer != null)
        {
            for (int i = 0; i < needleLineRenderer.positionCount; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(needleLineRenderer.transform.TransformPoint(needleLineRenderer.GetPosition(i)), 0.05f);
            }
        }

        if (pathLineRenderer != null)
        {
            for (int j = 0; j < pathLineRenderer.positionCount; j++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(pathLineRenderer.transform.TransformPoint(pathLineRenderer.GetPosition(j)), 0.05f);
            }
        }
    }
}
