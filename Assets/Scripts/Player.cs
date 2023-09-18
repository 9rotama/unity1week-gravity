using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("必要なコンポーネント")]
    [SerializeField]
    private GameManager gameManager;
    
    private float accelerationByTime = 0.01f;
    private float targetVelocityAtStart;
    private float targetVelocity;
    private Rigidbody2D rb;
    private const float power = 10f;
    private bool isGravityUpward;
    
   
    private void Start()
    {
        accelerationByTime = 0.005f;
        targetVelocityAtStart = 3f;
        rb = GetComponent<Rigidbody2D>();
        targetVelocity = targetVelocityAtStart;
        isGravityUpward = false;
        SetGravity();
    }

    private void IncreaseTargetVelocity()
    {
        targetVelocity += accelerationByTime;
    }

    private void Move()
    {
        float x = (targetVelocity - rb.velocity.x) * power;
        rb.AddForce(new Vector2(x, 0), ForceMode2D.Force);
    }
    
    private void FixedUpdate()
    {
        if (gameManager.GameState != GameState.Playing) return;
        Move();
        IncreaseTargetVelocity();
    }
    
    private void SetGravity()
    {
        rb.gravityScale = isGravityUpward ? -5 : 5;
    }
    private void ChangeGravity()
    {
        isGravityUpward = !isGravityUpward;
        SetGravity();
    }

    private void Update()
    {
        if (gameManager.GameState != GameState.Playing) return;
        if (Input.GetMouseButtonDown(0))
        {
            ChangeGravity();
        }

    }
}
