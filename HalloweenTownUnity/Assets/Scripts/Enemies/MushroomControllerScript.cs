using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomControllerScript : GenericEnemyControllerScript {
    Rigidbody2D objectRigidBody;
    Animator objectAnimator;
    // Use this for initialization
    int jumpTimer;
    bool canIncrementJump;

    void Start()
    {
        playerGameObject = GameObject.Find("Player");
        health = 5;
        damageDone = 2;
        objectRigidBody = GetComponent<Rigidbody2D>();
        objectAnimator = GetComponent<Animator>();
        jumpTimer = 0;
        canIncrementJump = true;
    }

    // Update is called once per frame
    void Update () {
        if (playerGameObject.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        else if (playerGameObject.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();
        if (objectRigidBody.velocity.y == 0)
        {
            objectRigidBody.velocity = Vector2.zero;
        }
        objectAnimator.SetFloat("vSpeed", objectRigidBody.velocity.y);
    }
    void FixedUpdate()
    {
        if(objectRigidBody.velocity.y == 0 && canIncrementJump)
        {
            jumpTimer++;
        }
        if(jumpTimer >= 50)
        {
            StartCoroutine(Windup());
        }
    }
    IEnumerator Windup()
    {
        jumpTimer = 0;
        canIncrementJump = false;
        objectAnimator.SetBool("WindingUp", true);
        yield return new WaitForSeconds(1f);
        objectAnimator.SetBool("WindingUp", false);
        if (facingRight)
            objectRigidBody.AddForce(new Vector2(400, 400), ForceMode2D.Impulse);
        else if (!facingRight)
            objectRigidBody.AddForce(new Vector2(-400, 400), ForceMode2D.Impulse);
        canIncrementJump = true;
    }
    void OnDestroy()
    {
        if (GameObject.Find("VariableManager").GetComponent<VariableManager>().cyanideStage > 0)
            GameObject.Find("VariableManager").GetComponent<VariableManager>().mushroomsKilled ++;
    }
}
