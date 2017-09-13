using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleControllerScript : GenericEnemyControllerScript {

    Rigidbody2D objectRigidBody;
    Animator objectAnimator;
    // Use this for initialization

    void Start()
    {
        playerGameObject = GameObject.Find("Player");
        health = 5;
        damageDone = 2;
        objectRigidBody = GetComponent<Rigidbody2D>();
        objectAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        objectAnimator.SetFloat("vSpeed", objectRigidBody.velocity.y);
        if(objectRigidBody.velocity.y == 0)
        {
            objectRigidBody.velocity = Vector2.zero;
        }
        if (playerGameObject.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        else if (playerGameObject.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();
    }
}
