using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    public bool isReady = false;

    float deathCooldown = 0f;

    bool isFlap = false;

    public bool godMode = false;

    MiniGameManager gameManager = null;

    private void Awake()
    {
        isDead = false;
        deathCooldown = 1;
        Debug.Log("플레이어 Awake");
    }

    private void Start()
    {
        Debug.Log("플레이어 Start");
        gameManager = MiniGameManager.Instance;

        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("NOT FOUNDED ANIMATOR");

        if (_rigidbody == null)
            Debug.LogError("NOT FOUNDED RIGIDBODY");

     //   isReady = true;


    }

    private void Update()
    {
       // if (!isReady) return;
        if (isDead)
        {
           // Debug.Log("너 죽음");

            if (!gameManager.isGameOver && deathCooldown <= 0)
            {
                //isReady = false;
                gameManager.GameOver();
                //this.gameObject.SetActive(false);
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;
        if (isDead) return;

        animator.SetInteger("isDead", 1);
        isDead = true;
        deathCooldown = 1f;

    }

}
