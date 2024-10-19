using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private float minjumpForce;
    [SerializeField] private float maxjumpForce;
    private float differenceJumpForce;
    private float differenceHorizontal;
    //how much time it takes to reach the maximum jumpforce
    [SerializeField] private float maxHoldTime = 1f;
    [SerializeField] private float jumpAngle = 30f;
    [SerializeField] private CapsuleCollider2D groundCheckCollider;
    [SerializeField] private LayerMask groundLayers;
    private Vector3 currentVelocity;
    private float horizontal = 0f;
    private float delta1 = 0f;
    private float delta2 = 0f;
    private bool jump1 = false;
    private bool jump2 = false;
    [SerializeField] private int jumpNum = 3;
    private int jumpNum1 = 0;
    private int jumpNum2 = 0;
    void Start()
    {
        float minHoriz;
        horizontal = maxjumpForce / Mathf.Tan(jumpAngle);
        minHoriz = minjumpForce / Mathf.Tan(jumpAngle);
        differenceHorizontal = horizontal - minHoriz;
        horizontal -= differenceHorizontal;
        differenceJumpForce = maxjumpForce - minjumpForce;
        maxjumpForce -= differenceJumpForce;
        rb = GetComponent<Rigidbody2D>();
        jumpAngle = jumpAngle * Mathf.Deg2Rad;
    }
    void Update()
    {
        UpdateGroundState();

        if (Input.GetKeyDown(KeyCode.A))
            jumpNum1 ++;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            jumpNum2 ++;

        if (jumpNum1 <= jumpNum)
        {
            Debug.Log($"jumpNum1: {jumpNum1}");
            if (Input.GetKey(KeyCode.A))
                delta1 += Time.deltaTime;

            if (Input.GetKeyUp(KeyCode.A))
                jump1 = true;
        }


        if (jumpNum2 <= jumpNum)
        {
            Debug.Log($"jumpNum2: {jumpNum2}");
            if (Input.GetKey(KeyCode.RightArrow))
                delta2 += Time.deltaTime;

            if (Input.GetKeyUp(KeyCode.RightArrow))
                jump2 = true;
        }
    }
    void FixedUpdate()
    {
        currentVelocity = rb.velocity;
        //Vector3 impulse;

        if (jump1)
        {
            Debug.Log("l jump");
            // make it an angle bitch
            Mathf.Clamp(delta1, 0f, maxHoldTime);
            currentVelocity.x = -((horizontal * delta1 / maxHoldTime) + differenceHorizontal);
            currentVelocity.y = (maxjumpForce * delta1 / maxHoldTime) + differenceJumpForce;
            delta1 = 0f;
            jump1 = false;
            rb.AddTorque(-Random.Range(jumpAngle, 360f), ForceMode2D.Impulse);
            //impulse = new Vector3(-horizontal, jumpForce, 0f);
            //rb.AddForce(impulse, ForceMode2D.Impulse);
        }

        if (jump2)
        {
            Debug.Log("r jump");
            Mathf.Clamp(delta1, 0f, maxHoldTime);
            currentVelocity.x = (horizontal * delta2 / maxHoldTime) + differenceHorizontal;
            currentVelocity.y = (maxjumpForce * delta2 / maxHoldTime) + differenceJumpForce;
            delta2 = 0f;
            jump2 = false;
            rb.AddTorque(Random.Range(jumpAngle, 360f), ForceMode2D.Impulse);
            //impulse = new Vector3(horizontal, jumpForce, 0f);
            //rb.AddForce(impulse, ForceMode2D.Impulse);
        }

        rb.velocity = currentVelocity;
    }
    private     void UpdateGroundState()
    {
        if (groundCheckCollider)
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.useLayerMask = true;
            contactFilter.layerMask = groundLayers;

            Collider2D[] results = new Collider2D[128];

            int n = Physics2D.OverlapCollider(groundCheckCollider, contactFilter, results);
            if (n > 0)
            {
                jumpNum1 = 0;
                jumpNum2 = 0;
            }
        }
    }
}
