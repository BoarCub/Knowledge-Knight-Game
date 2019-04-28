using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class EnemyControl : MonoBehaviour
{

    private bool isMoving = false;
    private bool facingRight = true;

    public float walkingSpeed = 6f;

    Rigidbody2D rb;
    CharacterAnimator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimator>();
    }

    public IEnumerator LoseBattle()
    {
        yield return new WaitForSeconds(0.1f);
        animator.PlayDeathAnimation();
    }

    public IEnumerator WinBattle()
    {
        animator.PlayPointAnimation();
        yield return new WaitForSeconds(1f);
        rb.transform.localScale = new Vector2(rb.transform.localScale.x * -1
            * Mathf.Sign(walkingSpeed), rb.transform.localScale.y);
        animator.PlayWalkAnimation();
        rb.velocity = new Vector2(walkingSpeed, rb.velocity.y);
    }

}