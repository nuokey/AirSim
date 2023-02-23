using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float mouseHorizontalSens;
    float yRotation = 0f;

    public float mouseVerticalSens;
    float xRotation = 0f;

    public new Transform camera;

    private void Start()
    {
        Cursor.visible = false;
    }

    void CameraRotation()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseVerticalSens * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        float mouseX = Input.GetAxis("Mouse X") * mouseHorizontalSens * Time.deltaTime;
        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    void FixedUpdate()
    {
        CameraRotation();
        Cursor.visible = false;
    }
}
