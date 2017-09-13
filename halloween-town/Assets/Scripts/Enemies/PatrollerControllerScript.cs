using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerControllerScript : GenericEnemyControllerScript {

    public Transform Patroller_RightTop;
    public Transform Patroller_RightBot;
    public float movementSpeed = 1000f;

    public Transform groundCheckTL;
    public Transform groundCheckBR;

    Rigidbody2D objectRigidBody;
    Animator objectAnimator;

    bool grounded = false;
    public LayerMask whatIsGround;
    public LayerMask whatToCollide;

    // Use this for initialization
    void Start () {
        playerGameObject = GameObject.Find("Player");
        health = 5;
        damageDone = 3;
        objectRigidBody = GetComponent<Rigidbody2D>();
        objectAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        grounded = Physics2D.OverlapArea(groundCheckTL.position, groundCheckBR.position, whatIsGround);
        objectAnimator.SetBool("Grounded", grounded);
        //left side turning to right side

        if (grounded && !beingHit)
        {
            if (!facingRight)
            {
                objectRigidBody.velocity = new Vector2(-movementSpeed, objectRigidBody.velocity.y);
                if (Physics2D.OverlapPoint(Patroller_RightTop.position, whatToCollide)
                    || !Physics2D.OverlapPoint(Patroller_RightBot.position, whatToCollide))
                {
                    Flip();
                }
            }
            //RS turning to LS
            else
            {
                objectRigidBody.velocity = new Vector2(movementSpeed, objectRigidBody.velocity.y);

                if (Physics2D.OverlapPoint(Patroller_RightTop.position, whatToCollide)
                    || !Physics2D.OverlapPoint(Patroller_RightBot.position, whatToCollide))
                {
                    Flip();
                }
            }
        }
        if (beingHit)
            beingHit = false;
        objectAnimator.SetFloat("Speed", Mathf.Abs(objectRigidBody.velocity.x));
        objectAnimator.SetFloat("vSpeed", objectRigidBody.velocity.y);
    }

}
