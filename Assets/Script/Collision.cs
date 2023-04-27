using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Collision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Circle")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject == null)
        {
            SceneManager.LoadScene("Syringe");
        }
    }
    
}