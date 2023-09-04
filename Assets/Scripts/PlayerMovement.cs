using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float JumpSpeed = 500f;

    private Rigidbody2D _rigidbody;

    private bool _isGrounded = false;
    
    #region Input Variables
    private float _XInput;
    private bool _isJumpPressed = false;
    #endregion

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    // every frame
    void Update()
    {
        // Key board A/D or Left/Right arrow
        // Controller stick Left/Right??
        _XInput = Input.GetAxisRaw("Horizontal");
        

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

        if (_isJumpPressed && _isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
        }
        _isJumpPressed = false;
    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _isGrounded = true;
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