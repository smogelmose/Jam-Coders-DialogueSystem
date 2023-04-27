using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Clicknext : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        int nextSceneIndex =SceneManager.GetActiveScene().buildIndex +1;
        if (Input.GetMouseButton(0)) // 
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
