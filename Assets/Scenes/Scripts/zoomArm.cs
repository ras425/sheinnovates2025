using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zoomArm : MonoBehaviour
{
    public string sceneToLoad;
    public void MoveToArm(){
        Debug.Log("Sprite clicked, loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/


}
