using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float sideForce;
    private Vector3 currentVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        currentVelocity = rb.velocity;

        if (Input.GetButtonDown("leftJump"))
        {
            // make it an angle bitch
            currentVelocity.x -= sideForce;
            currentVelocity.y = jumpForce;
        }

        if (Input.GetButtonDown("rightJump"))
        {
            currentVelocity.x += sideForce;
            currentVelocity.y = jumpForce;
        }

        rb.velocity = currentVelocity;
    }
}
