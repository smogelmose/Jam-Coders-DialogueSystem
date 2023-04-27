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
    public int MaxArrow = 1;

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
            transform.position += Vector3.down * delta * MaxArrow;
        }
        else transform.position += Vector3.up * delta * MaxArrow;

        temp = value;
        if (value == 1)
        {
            SceneManager.LoadScene(10);
        }
    }

}
