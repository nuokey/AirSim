using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    public Vector3 playerOffset;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.position + playerOffset;
        transform.rotation = player.rotation;
    }
}
