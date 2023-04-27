using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Rigidbody2D rb;
    private Touch touch;
    private float moveSpeed;

    void start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
    }
    void update()
    {
        if (Input.touchCount> 0)
        {
            touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Moved)
            {
                rb.velocity = (touch.deltaPosition.y > 0)
                    ? Vector2.up * moveSpeed
                    : - Vector2.up * moveSpeed;
            }
        }
    }
}
