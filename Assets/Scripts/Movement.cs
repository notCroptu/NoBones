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
    private GameManager _gameManager;

    private float _riseMultiplier = 40f;
    private float _fallMultiplier = 60f;
    [SerializeField] Sprite[] sprites = new Sprite[8];
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        jumpAngle = jumpAngle * Mathf.Deg2Rad;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        float minHoriz;
        horizontal = maxjumpForce / Mathf.Tan(jumpAngle);
        minHoriz = minjumpForce / Mathf.Tan(jumpAngle);
        differenceHorizontal = horizontal - minHoriz;
        horizontal -= differenceHorizontal;
        differenceJumpForce = maxjumpForce - minjumpForce;
        maxjumpForce -= differenceJumpForce;
        rb = GetComponent<Rigidbody2D>();
    }
    private IEnumerator JumpAnimator(float yv)
    {
        int counter = 0;
        while (true)
        {
            sr.sprite = sprites[counter];
            counter++;
            if (counter >= sprites.Length) break;
            yield return new WaitForSeconds(0.1f);
        }
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
            //Debug.Log($"jumpNum1: {jumpNum1}");
            if (Input.GetKey(KeyCode.A) && delta1 < maxHoldTime) {
                delta1 += Time.deltaTime;
                _gameManager.UpdateLeftJumpBar(delta1);
            }
            if (Input.GetKeyUp(KeyCode.A))
                jump1 = true;
        }


        if (jumpNum2 <= jumpNum)
        {
            //Debug.Log($"jumpNum2: {jumpNum2}");
            if (Input.GetKey(KeyCode.RightArrow) && delta2 < maxHoldTime) {
                delta2 += Time.deltaTime;
                _gameManager.UpdateRightJumpBar(delta2);
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
                jump2 = true;
        }

        /*
        if (( rb.velocity.x < 0 ) && (transform.right.x > 0) )
        {
            transform.rotation = Quaternion.Euler( 0, 180, 0);
        }
        else if (( rb.velocity.x > 0 ) && (transform.right.x < 0) )
        {
            transform.rotation = Quaternion.identity;
        }*/
    }
    void FixedUpdate()
    {
        currentVelocity = rb.velocity;
        //Vector3 impulse;

        if (jump1)
        {
            //Debug.Log("l jump");
            // make it an angle bitch
            delta1 = Mathf.Clamp(delta1, 0f, maxHoldTime);
            currentVelocity.x = -((horizontal * delta1 / maxHoldTime) + differenceHorizontal);
            currentVelocity.y = (maxjumpForce * delta1 / maxHoldTime) + differenceJumpForce;
            StartCoroutine(JumpAnimator(delta1));
            delta1 = 0f;
            jump1 = false;
            rb.AddTorque(-Random.Range(jumpAngle, 360f), ForceMode2D.Impulse);
            _gameManager.UpdateLeftJumpBar(currentVelocity.y);
            //impulse = new Vector3(-horizontal, jumpForce, 0f);
            //rb.AddForce(impulse, ForceMode2D.Impulse);
        }

        if (jump2)
        {
            //Debug.Log("r jump");
            delta2 = Mathf.Clamp(delta2, 0f, maxHoldTime);
            currentVelocity.x = (horizontal * delta2 / maxHoldTime) + differenceHorizontal;
            currentVelocity.y = (maxjumpForce * delta2 / maxHoldTime) + differenceJumpForce;
            StartCoroutine(JumpAnimator(delta2));
            delta2 = 0f;
            jump2 = false;
            rb.AddTorque(Random.Range(jumpAngle, 360f), ForceMode2D.Impulse);
            _gameManager.UpdateRightJumpBar(currentVelocity.y);
            //impulse = new Vector3(horizontal, jumpForce, 0f);
            //rb.AddForce(impulse, ForceMode2D.Impulse);
        }

        if (rb.velocity.y > 0) rb.gravityScale = _riseMultiplier;
        else if (rb.velocity.y < 0) rb.gravityScale = _fallMultiplier;

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
