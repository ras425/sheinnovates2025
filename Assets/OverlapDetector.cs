using UnityEngine;

public class OverlapDetector : MonoBehaviour
{
    public GameObject nextButton; // Assign this in the inspector

    private void Start()
    {
        nextButton.SetActive(false); // Initially hide the button
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Capsule")) // Ensure the capsule has the tag "Capsule"
        {
            nextButton.SetActive(true); // Show the Next button
            Debug.Log("Capsule Entered");
        }
    }

}
