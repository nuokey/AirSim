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

    public Rigidbody rb;
    public CapsuleCollider cc;

    public float walkSpeed;

    public float drag;

    private Ray ray = new Ray();

    void Control()
    {
        try
        {
            if (transform.parent.tag == "Vehicle")
            {
                if (Input.GetKey(KeyCode.E))
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 2, transform.localPosition.z);
                    transform.SetParent(transform.parent.parent);
                    rb.useGravity = true;
                    cc.enabled = true;
                }
            }
        }
        catch
        {
            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Surface")
        {
            rb.AddForce(-rb.velocity * drag);

            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(transform.forward * walkSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-transform.forward * walkSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-transform.right * walkSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(transform.right * walkSpeed);
            }
        }
        
    }

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

    void Use()
    {
        ray.origin = transform.position;
        ray.direction = camera.eulerAngles;
        Debug.DrawRay(transform.position, camera.eulerAngles);
    }

    void FixedUpdate()
    {
        CameraRotation();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Control();
        Use();
    }
}
