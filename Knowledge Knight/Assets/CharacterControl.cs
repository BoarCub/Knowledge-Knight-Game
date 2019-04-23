using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class CharacterControl : MonoBehaviour
{

    private bool isMoving = false;
    private bool facingRight = true;
    private float targetX;

    public float walkingSpeed = 3f;

    Rigidbody2D rb;
    CharacterAnimator animator;

    private Waypoint[] waypoints;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = FindObjectsOfType<Waypoint>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimator>();
        animator.PlayIdleAnimation();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            WalkToWaypoint();
        }
        
        if(isMoving && rb.position.x >= targetX)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y);
            isMoving = false;
            animator.PlayIdleAnimation();
        }

    }

    void WalkToWaypoint()
    {

        List<Waypoint> waypointsToRight = new List<Waypoint>();

        foreach (Waypoint w in waypoints)
        {
            
            if(w.transform.position.x > rb.transform.position.x)
            {
                waypointsToRight.Add(w);
            }
        }

        if (waypointsToRight.Count > 0)
        {

            float x = waypointsToRight[0].transform.position.x;
            
            for(int i = 1; i < waypointsToRight.Count; i++)
            {
                
                float wX = waypointsToRight[i].transform.position.x;

                if(x - rb.transform.position.x >
                    wX - rb.transform.position.x)
                {
                    x = wX;
                }
            }

            facingRight = true;
            isMoving = true;

            targetX = x;
            rb.velocity = new Vector2(walkingSpeed, rb.velocity.y);

            animator.PlayWalkAnimation();

        }
    }
}