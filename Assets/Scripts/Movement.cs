using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float jumpAngle = -30f;
    [SerializeField] private CircleCollider2D groundcheckcollider;
    [SerializeField] private LayerMask groundLayers;
    private Vector3 Impulse;
    private float horizontal = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpAngle = jumpAngle * Mathf.Deg2Rad;
    }
    void FixedUpdate()
    {
        //currentVelocity = rb.velocity;
        Vector3 impulse;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow))
            horizontal = jumpForce / Mathf.Tan(jumpAngle);

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("l jumo");
            // make it an angle bitch
            //currentVelocity.x -= horizontal;
            //currentVelocity.y = jumpForce;
            impulse = new Vector3(-horizontal, jumpForce, 0f);
            rb.AddForce(impulse, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("r jump");
            //currentVelocity.x += horizontal;
            //currentVelocity.y = jumpForce;
            impulse = new Vector3(horizontal, jumpForce, 0f);
            rb.AddForce(impulse, ForceMode2D.Impulse);
        }

        //rb.velocity = currentVelocity;
    }
}
