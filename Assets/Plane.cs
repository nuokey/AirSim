using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float engineForce;
    public float throttle;
    public float eleronsTorque;
    public float upDownTorque;
    public float leftRightTorque;

    public Rigidbody rb;

    public float aerodynamicForce;
    void FixedUpdate()
    {
        var fwdSpeed = Vector3.Dot(rb.velocity, transform.forward);
        var fwdEleronTorque = Vector3.Dot(new Vector3(0, 0, eleronsTorque), transform.forward);


        if (Input.GetKey(KeyCode.UpArrow)) {
            if (throttle < 1)
            {
                throttle += 0.01f;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (throttle > 0)
            {
                throttle -= 0.01f;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeTorque(new Vector3(upDownTorque, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeTorque(new Vector3(-upDownTorque, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeTorque(new Vector3(0, 0, eleronsTorque));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeTorque(new Vector3(0, 0, -eleronsTorque));
        }
        if (Input.GetKey(KeyCode.J))
        {
            rb.AddRelativeTorque(new Vector3(0, -leftRightTorque, 0));
        }
        if (Input.GetKey(KeyCode.L))
        {
            rb.AddRelativeTorque(new Vector3(0, leftRightTorque, 0));
        }

        

        rb.AddForce(transform.forward * engineForce * throttle);
        rb.AddForce(transform.up * aerodynamicForce * fwdSpeed);
    }
}
