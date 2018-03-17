﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float runSpeed = 8.0f;
    [SerializeField] float rotationSpeed = 5.0f;

    public static bool lockMovement = false;

    private float currentSpeed;
    private Transform m_Cam;
    private Vector3 m_vertical;
    private Vector3 m_horizontal;

    private void Awake()
    {
        //Gets the main camera and stores it in a variable
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning("Warning: no main camera found.", gameObject);            
        }

        //Sets the current speed to the initial movement speed
        currentSpeed = moveSpeed;

        //Gets the vertical direction relative to the camera
        m_vertical = m_Cam.transform.forward;
        m_vertical.y = 0;
        m_vertical = Vector3.Normalize(m_vertical);

        //Gets the horizontal direction relative to the camera using the vertical vector
        m_horizontal = Quaternion.Euler(new Vector3(0, 90, 0)) * m_vertical;
        m_horizontal = Vector3.Normalize(m_horizontal);

        //Makes main character face towards the player when game starts
        Quaternion idleLookDirection = Quaternion.LookRotation(-m_vertical);
        transform.rotation = idleLookDirection;
    }

    private void FixedUpdate()
    {
       if(Input.GetButton("Sprint"))
            currentSpeed = runSpeed;
       else currentSpeed = moveSpeed;

       //Process movement if allowed to move (ex: cutscenes, tutorials, etc)
       if(!lockMovement)
            Move();
    }

    private void Move()
    {
        //Calculates how much to move vertically and horizontally as well as what direction is the player facing
        Vector3 vMovement = m_vertical * Time.deltaTime * currentSpeed * Input.GetAxisRaw("Vertical");
        Vector3 hMovement = m_horizontal * Time.deltaTime * currentSpeed * Input.GetAxisRaw("Horizontal");
        Vector3 movement = hMovement + vMovement;

        //No need to process movement/rotation if the player is not moving
        if (movement != Vector3.zero)
        {   
            //Determines which direction to face the player according to the movement
            Quaternion lookDirection = Quaternion.LookRotation(movement.normalized);

            //Rotates and moves the player accordingly
            transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * rotationSpeed);
            transform.position += movement;
        }

        
    }
}