using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCtrl : MonoBehaviour
{
    public float speed = 3; // Speed multiplier for the boat's movement

    float force; // Forward/backward force input
    float dir; // Left/right direction input
    Rigidbody rb; // Reference to the Rigidbody component of the boat

    private void Start()
    {
        // Get the Rigidbody component attached to the boat
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is used for physics-related updates
    void FixedUpdate()
    {
        // Smoothly transition the forward/backward force based on input (Vertical axis)
        force = Mathf.SmoothStep(force, -Input.GetAxis("Vertical"), Time.deltaTime * 10);

        // Smoothly transition the left/right direction based on input (Horizontal axis)
        dir = Mathf.SmoothStep(dir, Input.GetAxis("Horizontal"), Time.deltaTime * 10);

        // Calculate the forward/backward velocity
        Vector3 velocity = new Vector3(  speed * force, 0,0);

        // Apply the calculated velocity to the Rigidbody, transforming it into the boat's local direction
        rb.linearVelocity = rb.transform.TransformDirection(velocity);

        // Calculate the angular velocity for turning (rotation around the Y-axis)
        Vector3 angularVector1 = new Vector3(0, dir * speed, 0);

        // Apply the calculated angular velocity to the Rigidbody
        rb.angularVelocity = angularVector1;
    }
} 

