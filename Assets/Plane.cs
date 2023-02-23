using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{   
    public float cruiseSpeed;
    public float maxSpeed;
    public float takeOffSpeed;

    public float drag;
    public float airPressureDefault;
    public float airDensity;
    public float cruiseHeight;
    public float eleronsTorque;
    public float upDownTorque;
    public float leftRightTorque;

    public float throttle;
    public float engineForce;

    public float aerodynamicCoef;
    public float aerodynamicAgnleCoef;

    public float airSpeed;
    public float verticalSpeed;
    public float verticalAirSpeed;

    public float aerodynamicForce;

    public Vector3 dragForce;

    public float verticalDragForce;

    public Rigidbody rb;

    public Vector3 playerOffset;

    float AerodynamicForce(float coef, float angleCoef, float speed, float verticalSpeed, float airDensity)
    {
        var angleForce = verticalSpeed  * verticalSpeed * angleCoef;

        if (verticalSpeed > 0)
        {
            angleForce *= -1;
        }

        var force = (coef) * speed * speed * airDensity + angleForce;
        return force;
    }

    Vector3 DragForce(float coef, Vector3 velocity)
    {
        var force = new Vector3(velocity.x * velocity.x, velocity.y * velocity.y, velocity.z * velocity.z) * -coef;
        return force;
    }

    float VerticalDragForce(float coef, float velocity)
    {
        var force = velocity * velocity * -coef;
        return force;
    }

    void Forces()
    {

    }

    void Control()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
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
    }

    private void Start()
    {
        var takeOffAngle = 5f;
        cruiseSpeed /= 3.6f;
        maxSpeed /= 3.6f;

        
        aerodynamicCoef = rb.mass * 9.81f / (cruiseSpeed * cruiseSpeed * airPressureDefault * Mathf.Exp(-0.029f * 9.81f / 300 * cruiseHeight / 8.31f));
        aerodynamicAgnleCoef = (rb.mass * 9.81f - takeOffSpeed * takeOffSpeed * aerodynamicCoef) / (Mathf.Asin(Mathf.Deg2Rad * takeOffAngle) * takeOffSpeed);
        engineForce = maxSpeed * maxSpeed * drag;
    }

    void FixedUpdate()
    {
        Control();
        airDensity = airPressureDefault * Mathf.Exp(-0.029f * 9.81f / 300 * transform.position.y / 8.31f);

        var fwdSpeed = Vector3.Dot(rb.velocity, transform.forward);
        verticalAirSpeed = Vector3.Dot(rb.velocity, transform.up);
        var fwdEleronTorque = Vector3.Dot(new Vector3(0, 0, eleronsTorque), transform.forward);

        aerodynamicForce = AerodynamicForce(aerodynamicCoef, aerodynamicAgnleCoef, fwdSpeed, verticalAirSpeed, airDensity);
        dragForce = DragForce(drag, rb.velocity);
        verticalDragForce = VerticalDragForce(drag, verticalAirSpeed);


        

        rb.AddForce(dragForce);

        rb.AddForce(transform.forward * engineForce * throttle);
        rb.AddForce(transform.up * aerodynamicForce);


        // rb.drag = defaultDrag + angleDragCoef * Mathf.Abs(transform.localRotation.x);
        airSpeed = fwdSpeed;
        verticalSpeed = rb.velocity.y;

    }
}
