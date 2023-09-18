using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
   
    private void Start()
    {
        accelerationByTime = 0.005f;
        targetVelocityAtStart = 3f;
        rb = GetComponent<Rigidbody2D>();
        targetVelocity = targetVelocityAtStart;
    }

    private void IncreaseTargetVelocity()
    {
        targetVelocity += accelerationByTime;
    }

    private void MoveForward()
    {
        rb.AddForce(Vector2.right*((targetVelocity-rb.velocity.x) * power), ForceMode2D.Force);
    }
    
    private void FixedUpdate()
    {
        if (gameManager.GameState == GameState.Playing)
        {
            MoveForward();
            IncreaseTargetVelocity();
        }

        Debug.Log(rb.velocity);
    }

}
