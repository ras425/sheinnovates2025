using UnityEngine;

public class needletouch : MonoBehaviour
{
    
    // public CustomButtonVisibility nextButtonVisibility;  // Assign this in the inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("needle")) 
        {
            Debug.Log("hmm");
            // nextButtonVisibility.SetVisible(true);
        }
    }


}
