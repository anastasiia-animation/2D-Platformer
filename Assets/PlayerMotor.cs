using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    Vector2 direction;
    private bool canJump = true;
    private new Rigidbody2D rigidbody2D;
    public float speed = 5;
    public float jumpForce = 5;
    public float maxSpeed = 10;
    public float stoppingForce = 10;
    public float MaxHealth = 5;
    public float Health = 5;
    public bool ConReciredamage = false;
    public float invincililitqTimer = 2;
    public float Coin = 0;
    public Coin cm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer(); 
        PlayerStopping();
        HandleMaxSpeed();
    }

    private void MovePlayer()
    { 
        rigidbody2D.AddForce(new Vector2(direction.x, 0) * speed);
    }

    private void HandleMaxSpeed()
    {
        if (rigidbody2D.linearVelocityX >= maxSpeed)
        {
            rigidbody2D.linearVelocityX = maxSpeed;
        }
        else if (rigidbody2D.linearVelocityX <= -maxSpeed)
        {
            rigidbody2D.linearVelocityX = -maxSpeed;
        }
    }

    private void PlayerStopping()
    {
        if (direction.x == 0 && rigidbody2D.linearVelocityX != 0)
        {
            rigidbody2D.AddForce(new Vector2(-rigidbody2D.linearVelocityX * stoppingForce, 0));
        
        }
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private void OnJump()
    {
        if (canJump)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            int v = cm.coinCount++;
            Destroy(collision.gameObject);
        }
    }
}



