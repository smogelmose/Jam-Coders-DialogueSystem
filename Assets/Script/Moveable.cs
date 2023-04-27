using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Moveable : MonoBehaviour
{
    public float temp = 0;
    public Slider slider;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(OnChange);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnChange(float value)
    {
        float delta = Mathf.Abs(value -temp);
        if (temp > value)
        {
            transform.position += Vector3.down * delta * 1500;
        }
        else transform.position += Vector3.up * delta * 1500;

        temp = value;
        if (value == 1)
        {
            SceneManager.LoadScene(1);
        }
    }

}
