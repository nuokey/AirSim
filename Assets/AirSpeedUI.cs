using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirSpeedUI : MonoBehaviour
{
    public GameObject playerPlane;
    public Rigidbody rb;

    void Start()
    {
        rb = playerPlane.GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.GetComponent<Text>().text = "Air Speed:" + System.Convert.ToInt32(Vector3.Dot(rb.velocity, transform.forward) * 3.6).ToString() + "km/h";
    }
}
