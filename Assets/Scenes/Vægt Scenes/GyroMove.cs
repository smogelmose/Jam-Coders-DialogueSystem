using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroMove : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroSupported;
    private Quaternion rotFix;
    public Quaternion Gamer = new Quaternion (0f, 0f, 1f, 0f);
    public Quaternion Gamers = new Quaternion(0f, 0f, 0f, 1f);

    void Start()
    {
        gyroSupported = SystemInfo.supportsGyroscope;

        if (gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            // Set the rotation fix to take into account the screen orientation of the device
            if (Screen.orientation == ScreenOrientation.LandscapeLeft)
                rotFix = Gamer;

            if (Screen.orientation == ScreenOrientation.Portrait)
                rotFix = Gamers;

            // Make the image the child of the camera so it moves with it
            transform.parent = Camera.main.transform;
        }
    }

    void Update()
    {
        if (gyroSupported)
        {
            // Get the gyroscope data and apply the rotation fix
            Quaternion gyroAttitude = gyro.attitude * rotFix;

            // Apply the rotation to the image
            transform.localRotation = gyroAttitude;
        }
    }
}
