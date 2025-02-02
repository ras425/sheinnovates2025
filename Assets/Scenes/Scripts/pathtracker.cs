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
                break;  // Break after the first match is found
            }
        }
    }

    Debug.Log($"Current Matches: {matchCount}");

    // Trigger scene change based on match count
    if (matchCount >= requiredMatches && !sceneLoaded)
    {
        sceneLoaded = true;
        LoadNextScene();
    }
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
