using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;


    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MoveMentDirection { get { return movementDirection; } }  // 이동하는 방향 지정

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection {  get { return lookDirection; } } // 바라보는 방향 지정

    private Vector2 knockback = Vector2.zero;  // 넉백 방향 가져옴
    private float knockbackDuration = 0.0f;

    // 애니메이션
    protected AnimationHandler AnimationHandler;   

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        AnimationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movement(Vector2 direction)        // 넉백을 이용해야된다면, 기존 이동방햐으이 힘을 줄여주고 넉백의 힘을 넣어주겠다.
    {
        direction = direction * 5;
        if(knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        } 
        _rigidbody.velocity = direction;
        AnimationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Mathf.Rad2Deg 라디안 -> 도
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        AnimationHandler?.SetAnimationDirection(direction);

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }
}
