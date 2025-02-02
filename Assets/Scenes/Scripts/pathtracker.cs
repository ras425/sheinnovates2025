// using UnityEngine;

// public class pathtracker : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }


using UnityEngine;
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
}
