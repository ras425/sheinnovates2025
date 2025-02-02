using UnityEngine;

public class nextbutton : MonoBehaviour
{
    public GameObject needle;  // Assign the needle GameObject in the Inspector
    public GameObject triggerZone;  // Assign the capsule GameObject in the Inspector
    public GameObject nextButton;  // Assign the Next button GameObject in the Inspector
    private bool gameActive = true;  // Declare the gameActive variable and initialize it

    void Update()
    {
        if (!gameActive) return;

        // Check if the needle is within the trigger zone
        if (triggerZone.GetComponent<Collider2D>().bounds.Contains(needle.transform.position))
        {
            nextButton.SetActive(true);  // Show the Next button
        }
    }
}
