using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float JumpSpeed = 500f;

    public float timeOfLastGrounded = 0f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private bool _isGrounded = false;
    #region Input Variables
    private float _XInput;
    private bool _isJumpPressed = false;
    #endregion
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // Key board A/D or Left/Right arrow
        // Controller stick Left/Right??
        _XInput = Input.GetAxisRaw("Horizontal");

        if (_XInput == 0)
        {
            _animator.SetBool("IsWalking", false);
        }
        else
        {
            _animator.SetBool("IsWalking", true);
            if (_XInput > 0)
            {
                _sprite.flipX = true;
            }
            else
            {
                _sprite.flipX = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumpPressed = true;
        }
    }

    //Only happens every 0.02 seconds (unless changed)
    private void FixedUpdate()
    {
        //_rigidbody.AddForce(new Vector2(_XInput, 0f), ForceMode2D.Impulse);
        _rigidbody.velocity = new Vector2(_XInput * MoveSpeed * Time.deltaTime, _rigidbody.velocity.y);

        if (_isJumpPressed && (_isGrounded || Time.time - timeOfLastGrounded < 0.2f))
        {
            _rigidbody.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
            timeOfLastGrounded = 0f;
        }
        _isJumpPressed = false;
    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            Debug.Log(collision.contacts.Length);
           
            //Points of contact with whatever we are colliding with
            Vector2 collisionPoint = collision.contacts[0].point;
            //To get the direction To A From B = A - B
            Vector2 collisionDirection = collisionPoint - (Vector2)transform.position;
            //Makes the length/magnitude/distance of the vector 1. 
            collisionDirection.Normalize();
            float angle = Vector2.Angle(collisionDirection, Vector2.down);

            if (angle < 45f)
            {
                _isGrounded = true;
                timeOfLastGrounded = Time.time;
            }
            else
            {
                _isGrounded = false;
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _isGrounded = false;
        }
    }
}