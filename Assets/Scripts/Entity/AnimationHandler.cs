using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController frontAnim;
    [SerializeField] private AnimatorOverrideController backAnim;
    [SerializeField] private AnimatorOverrideController sideAnim;

    [SerializeField] private GameObject frontSprite;
    [SerializeField] private GameObject backSprite;
    [SerializeField] private GameObject sideSprite;

    private SpriteRenderer sideRenderer;

    private static readonly int IsMove = Animator.StringToHash("IsMove"); // ��Ʈ������ ���ڷ� ��ȯ���༭ ���
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");   // ��? ���ϱ� �� ���ϴϱ�

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        sideRenderer = sideSprite.GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMove, obj.magnitude > .5f);
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }

    public void SetAnimationDirection(Vector2 direction)
    {
        if (animator == null) return;

        frontSprite.SetActive(false);
        backSprite.SetActive(false);
        sideSprite.SetActive(false);

        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) // ���Ʒ� ���� �켱
        {
            if (direction.y > 0)
            {
                animator.runtimeAnimatorController = backAnim;
                backSprite.SetActive(true);
            }
            else
            {
                animator.runtimeAnimatorController = frontAnim;
                frontSprite.SetActive(true);
            }
        }
        else
        {
            animator.runtimeAnimatorController = sideAnim;
            sideSprite.SetActive(true);
            sideRenderer.flipX = direction.x < 0;
        }
    }


}
