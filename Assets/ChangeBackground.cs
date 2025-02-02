using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChangeBackground : MonoBehaviour
{
    public GameObject Untilted1;
    public GameObject Untilted2;

    // void Start(){

    // }

    // void Update(){

    // }

    public void BackgroundChanger(){

        Untilted1.SetActive(false);
        Untilted2.SetActive(true);
        
    }
}
