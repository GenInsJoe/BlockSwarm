﻿//Author: Joe Bjorkman
//File: PlayerMovement.cs
//Last Updated: 7/28/2016
//Purpose: This is the basic movement script for the player object.
//  The player can only move horisontally. 
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Private Variables
    private Rigidbody2D rb2d;    
    
    // initualization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // The speed of side movement
        float speed = 0.2F;
        // Allow for keyboard movement
        float MoveHor = Input.GetAxis("Horizontal");

        // Allow for touchpad movement
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 TouchMov = Input.GetTouch(0).deltaPosition;
            MoveHor = TouchMov.x;
        }

        if(MoveHor > 0)
        {
            transform.Translate(speed, 0, 0);
        }
        else if (MoveHor < 0)
        {
            transform.Translate(-speed, 0, 0);
        }
        

    }
}