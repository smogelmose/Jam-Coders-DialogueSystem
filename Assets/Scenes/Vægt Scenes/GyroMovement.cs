using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroMovement : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Quaternion rot;

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

            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            Vector3 gyroInput = -gyro.attitude.eulerAngles;
            gyroInput.z = 0f;
            transform.eulerAngles = gyroInput;

            Vector3 direction = new Vector3(Input.acceleration.x, Input.acceleration.y, 0f);
            float acceleration = 0.5f;
            transform.Translate(direction * acceleration);
        }
    }
}
