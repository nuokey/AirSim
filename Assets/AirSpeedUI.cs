using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirSpeedUI : MonoBehaviour
{
    public GameObject playerPlane;


    void Update()
    {
        transform.GetComponent<TextMesh>().text = "Air Speed:" + System.Convert.ToInt32(playerPlane.GetComponent<Plane>().airSpeed * 3.6).ToString() + "km/h";
    }
}
