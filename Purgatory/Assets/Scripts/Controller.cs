using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public float speed = 1f;

    private Player_Inputs input;
    private Rigidbody2D rb;

    private Vector2 direction;

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
        direction = obj.ReadValue<Vector2>();
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
