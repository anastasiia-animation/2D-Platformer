using JetBrains.Annotations;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    Vector2 direction;
    public float dashForce = 10;
    public float dashTime = 0.5f;
    private bool canJump = true;
    private new Rigidbody2D rigidbody2D;
    public float speed = 5;
    public float jumpForce = 7;
    public float maxSpeed = 10;
    public float stoppingForce = 10;
    public float MaxHealth = 5;
    public float Health = 5;
    public bool ConReciredamage = false;
    public float invincililitqTimer = 2;
    public float Coin = 0;
    public Coin cm;
    private Animator _animator;
    private bool _canJump = true;
    private bool _isDashing = false;

    private int _jumpCount = 0;
    private int maxJumpCount = 2;

    private float initXscale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        initXscale = transform.localScale.x;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer(); 
        PlayerStopping();
        HandleMaxSpeed();
        if(direction.x != 0)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }

        if(direction.x > 0)
        {
            transform.localScale = new Vector3(initXscale, transform.localScale.y, transform.localScale.z);
        }
        else if(direction.x < 0)
        {
            transform.localScale = new Vector3(-1 * initXscale, transform.localScale.y, transform.localScale.z);
        }
    }

    private void MovePlayer()
    { 
        rigidbody2D.AddForce(new Vector2(direction.x, 0) * speed);
    }

    private void HandleMaxSpeed()
    {
        if(_isDashing)
        {
            return;
        }
        //Handle Max Speed
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
        if (_canJump)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpCount++;
            if(_jumpCount >= maxJumpCount)
            {
                _canJump = false;
            }
        }
       
    }

    private void OnDash()
    {
        if(_isDashing) return;
        _isDashing = true;
        rigidbody2D.AddForce(new Vector2(direction.x * dashForce, 0), ForceMode2D.Impulse);
        StartCoroutine(ResetDash(1));

    }

    IEnumerator ResetDash(float timeToRest)
    {
        yield return new WaitForSeconds(timeToRest);
        _isDashing = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        _canJump = true;
        _jumpCount = 0;
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
