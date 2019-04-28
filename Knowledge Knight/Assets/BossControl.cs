using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class BossControl : MonoBehaviour
{

    private bool isMoving = false;
    public bool isRunningAway = true;

    public float runningSpeed = 12f;

    private Rigidbody2D rb;
    private CharacterAnimator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimator>();
    }

    public void DialogueFinished()
    {
        if (isRunningAway)
        {
            rb.transform.localScale = new Vector2(rb.transform.localScale.x
            * -1, rb.transform.localScale.y);
            rb.velocity = new Vector2(runningSpeed, rb.velocity.y);
            animator.PlayRunningAnimation();
            StartCoroutine(DestroyObject());
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(10);
        Destroy(this);
    }

}
