// using UnityEngine;

// public class NextButtonVisibility : MonoBehaviour
// {
//     public GameObject nextButton;

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         Debug.Log("test");
//         if (other.CompareTag("needle"))
//         {
//             nextButton.SetActive(true);
//         }
//     }
// }



using UnityEngine;

public class NextButtonVisibility : MonoBehaviour
{
    public GameObject nextButton;
    public GameObject needle; // Assign this in the inspector
    public float checkInterval = 1.0f; // How often to check, in seconds
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0;
            CheckProximity();
        }
    }

    void CheckProximity()
    {
        if (Vector3.Distance(transform.position, needle.transform.position) < 0.5f) // Check if needle is close enough
        {
            Debug.Log("Needle is close to trigger area.");
            nextButton.SetActive(true);
        }
    }
}
