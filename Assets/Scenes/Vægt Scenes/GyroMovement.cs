using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroMovement : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Quaternion rot;
    public Quaternion Rotation = new Quaternion(0, 0, 1, 0);

    private void Start()
    {
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            rot = Rotation;

            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            Vector2 direction = new Vector2(Input.acceleration.x, Input.acceleration.y);
            float acceleration = 0.5f;
            transform.Translate(direction * acceleration);
        }
    }
}
