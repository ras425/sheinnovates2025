using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    void OnMouseDown()
    {
        // Load the Arm scene when clicked
        SceneManager.LoadScene("Arm");
    }
}