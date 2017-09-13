using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatControllerScript : GenericEnemyControllerScript
{

    Rigidbody2D objectRigidBody;
    bool resettingVelocity;
    // Use this for initialization

    void Start()
    {
        playerGameObject = GameObject.Find("Player");
        health = 3;
        damageDone = 1;
        objectRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGameObject.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        else if (playerGameObject.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();
    }
    void FixedUpdate()
    {
        if (!resettingVelocity && objectRigidBody.velocity != Vector2.zero)
        {
            resettingVelocity = true;
            StartCoroutine(resetVelocity());
        }
        gameObject.transform.position = new Vector3(
            Mathf.Lerp(gameObject.transform.position.x, playerGameObject.transform.position.x, 0.02f),
            Mathf.Lerp(gameObject.transform.position.y, playerGameObject.transform.position.y, 0.02f));
    }

    IEnumerator resetVelocity()
    {
        yield return new WaitForSeconds(2f);
        objectRigidBody.velocity = Vector2.zero;
        resettingVelocity = false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Wall"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col.collider, true);
        }
    }
}
