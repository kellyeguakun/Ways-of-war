using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    //Min and  Max Distance mouse can move
    private const float Y_ANGLE_MIN = -0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    //Current Mouse position
    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
   

    private void start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 3, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;

        camTransform.LookAt(lookAt.position);
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            distance += 0.2f;

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            distance -= 0.2f;

        }


    }

}
