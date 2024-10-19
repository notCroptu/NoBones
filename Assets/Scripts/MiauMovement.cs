using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiauMovement : MonoBehaviour
{
    // A = jump left
    // > = jump right
    // hold gives more strength
    private float _holdingTime = 0f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A) && _holdingTime > 0f)
        {
            Debug.Log("jump");
            _rb.AddForce(new Vector2(100f, 100f * _holdingTime), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            _holdingTime += Time.fixedDeltaTime;
        }
    }
}
