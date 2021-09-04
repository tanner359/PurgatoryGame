using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public float speed = 1f;
    [Range(0f, 1f)]
    public float deadZone = 0.00f;

    private Player_Inputs input;
    private Rigidbody2D rb;

    public Vector2 direction;

    public void OnEnable()
    {
        if (input == null)
        {
            input = new Player_Inputs();
        }
        input.Player.Movement.performed += Movement;
        input.Player.Movement.canceled += Movement;
        input.Player.Movement.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Movement(InputAction.CallbackContext obj)
    {
        float xValue = obj.ReadValue<Vector2>().x;
        float yValue = obj.ReadValue<Vector2>().y;

        if (xValue < -deadZone && transform.localScale.x > 0)
        {
            Flip();
        }
        else if (xValue > deadZone && transform.localScale.x < 0)
        {
            Flip();
        }

        if (xValue > deadZone || xValue < -deadZone || yValue > deadZone || yValue < -deadZone) 
        {
            direction = obj.ReadValue<Vector2>();
        }
        else
        {
            direction = Vector2.zero;
        }
        
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2((direction.x * speed), rb.velocity.y);
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }
}
