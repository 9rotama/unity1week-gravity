using System;
using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("必要なコンポーネント")]
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject gameOverParticle;
    [SerializeField] private GameObject trailParticle;

    
    private float accelerationByTime = 0.01f;
    private float targetVelocityAtStart;
    private float targetVelocity;
    private Rigidbody2D rb;
    private const float power = 10f;
    public bool isGravityUpward;
    private const float outStageRangeUpper = 9f;
    private const float outStageRangeLower = -9f;
    private bool isGameOverFunctionExecuted;
    public bool IsFloating = false;
    private SpriteRenderer spriteRenderer;  
    
    private void Start()
    {
        accelerationByTime = 0.002f;
        targetVelocityAtStart = 3f;
        rb = GetComponent<Rigidbody2D>();
        targetVelocity = targetVelocityAtStart;
        isGravityUpward = false;
        SetGravity();
        isGameOverFunctionExecuted = false;
        BGMManager.Instance.Play(BGMPath.PLAY_BGM);
        IsFloating = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailParticle.SetActive(true);
    }


    public void Reset()
    {
        targetVelocity = targetVelocityAtStart;
        isGameOverFunctionExecuted = false;
        transform.position = new Vector3(0,0,0);
        rb.simulated = true;
        spriteRenderer.enabled = true;
        trailParticle.SetActive(true);

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
        if (gameManager.GameState == GameState.GameOver) return;
        Move();
        if (gameManager.GameState != GameState.Playing) return;
        IncreaseTargetVelocity();
    }
    
    private void SetGravity()
    {
        rb.gravityScale = isGravityUpward ? -3 : 3;
    }
    private void ChangeGravity()
    {
        isGravityUpward = !isGravityUpward;
        SetGravity();
        SEManager.Instance.Play(SEPath.CHANGE_GRAVITY);
        IsFloating = true;
    }

    private void Update()
    {
        if (gameManager.GameState == GameState.GameOver) return;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            ChangeGravity();
        }
        if ((transform.position.y is > outStageRangeUpper or < outStageRangeLower ) && !isGameOverFunctionExecuted)
        {
            isGameOverFunctionExecuted = true;
            OutStage();
        }
    }

    public void OutStage()
    {
        gameManager.playerOutStage();
        trailParticle.SetActive(false);

        GameObject particleObj = Instantiate(gameOverParticle);
        particleObj.transform.position = transform.position;
        spriteRenderer.enabled = false;
        rb.simulated = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        var stageObject = collision.gameObject.GetComponent<ObjectPart>();
        stageObject?.OnCollisionWithPlayer(this);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        var stageObject = collision.gameObject.GetComponent<ObjectPart>();
        stageObject?.OnCollisionWithPlayer(this);
    }


    
}
