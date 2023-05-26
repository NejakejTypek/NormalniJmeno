using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    public float cameraSpeedVertical;

    public float cameraLimit;
    public float verticalCameraLimit;

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -cameraLimit)
                transform.position = new Vector3(transform.position.x - Time.deltaTime * cameraSpeed, transform.position.y, -10);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < cameraLimit)
                transform.position = new Vector3(transform.position.x + Time.deltaTime * cameraSpeed, transform.position.y, -10);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.y < verticalCameraLimit)
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * cameraSpeedVertical, -10);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y > -verticalCameraLimit)
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * cameraSpeedVertical, -10);
        }
    }
}
