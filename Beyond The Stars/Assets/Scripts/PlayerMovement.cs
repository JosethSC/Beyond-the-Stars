using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Components
    protected Rigidbody2D r;
    #endregion
    #region Variables
    private Vector2 _direction;
    public float speed;
    public float jumpHeight;

    public bool isOnGround;
    private float _groundChecker = 0.05f;
    private Vector2 _groundCheckerOffset;
    public LayerMask groundLayer;

    #endregion

    private void Awake()
    {
        r = GetComponent<Rigidbody2D>();
        r.freezeRotation = true;
        _groundCheckerOffset = Vector2.up * -0.53f;
    }

    private void Update()
    {
        //Detects if the player is grounding
        isOnGround = Physics2D.OverlapCircle(r.position + _groundCheckerOffset, _groundChecker, groundLayer);
        //Gets updated once per frame
        float axisX = Input.GetAxisRaw("Horizontal");

        _direction = new Vector2(axisX, 0);

        if(isOnGround)
        {
            JumpHandler();
        }
    }
    private void FixedUpdate()
    {
        //Normalizes the dir of the player input and multiplies by speed
        r.velocity = new Vector2(_direction.normalized.x * speed, r.velocity.y);
    }

    public void JumpHandler()
    {
        if (Input.GetButtonDown("Jump"))
        {
            r.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }
}
