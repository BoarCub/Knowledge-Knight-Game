﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class CharacterControl : MonoBehaviour
{

    private bool isMoving = false;
    private bool isInitialized = false;

    public float walkingSpeed = 6f;

    Rigidbody2D rb;
    CharacterAnimator animator;

    public List<Waypoint> waypoints;
    private Waypoint currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        /*waypoints = FindObjectsOfType<Waypoint>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimator>();
        animator.PlayIdleAnimation();
        
        StartCoroutine(DelayedWalk());*/
    }

    public IEnumerator DelayedWalk()
    {
        yield return new WaitForSeconds(1);
        WalkToWaypoint();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isInitialized)
        {
            waypoints = new List<Waypoint>();
            Waypoint[] waypointsTemporary = FindObjectsOfType<Waypoint>();
            foreach (Waypoint w in waypointsTemporary)
            {
                waypoints.Add(w);
            }

            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<CharacterAnimator>();
            animator.PlayIdleAnimation();

            StartCoroutine(DelayedWalk());

            isInitialized = true;
        }
        
        if(isMoving && rb.position.x >= currentWaypoint.transform.position.x)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y);
            rb.transform.position =  new Vector2 (currentWaypoint.transform.position.x,
                rb.transform.position.y);
            isMoving = false;
            animator.PlayIdleAnimation();
            currentWaypoint.ReachWaypoint();
        }

    }

    public IEnumerator LoseBattle()
    {
        yield return new WaitForSeconds(0.1f);
        animator.PlayDeathAnimation();
    }

    public IEnumerator WinBattle()
    {
        animator.PlayPointAnimation();
        yield return new WaitForSeconds(0.75f);
        animator.PlayWalkAnimation();
        WalkToWaypoint();
    }

    public void WalkToWaypoint()
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

            Waypoint wMax = waypointsToRight[0];
            
            for(int i = 1; i < waypointsToRight.Count; i++)
            {
                
                Waypoint w = waypointsToRight[i];

                if(wMax.transform.position.x - rb.transform.position.x >
                   w.transform.position.x - rb.transform.position.x)
                {
                    wMax = w;
                }
            }

            isMoving = true;

            currentWaypoint = wMax;
            rb.velocity = new Vector2(walkingSpeed, rb.velocity.y);

            animator.PlayWalkAnimation();

        }
    }
}