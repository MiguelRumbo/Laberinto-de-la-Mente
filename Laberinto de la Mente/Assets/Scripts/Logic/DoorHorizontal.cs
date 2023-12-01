using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHorizontal : MonoBehaviour
{
    public bool doorOpen = false;
    public float doorOpenAngle = -90f;
    public float doorCloseAngle = 0.0f;
    public float smooth = 3.0f;

    public void ChangeDoorState()
    {
        doorOpen = !doorOpen;
    }

    void Update()
    {
        if(doorOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(-90, doorOpenAngle, -90);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(-90, doorCloseAngle, -90);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
        }
    }
}
