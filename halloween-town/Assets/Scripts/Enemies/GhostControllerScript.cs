using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControllerScript : GenericEnemyControllerScript {

    Rigidbody2D objectRigidBody;
    bool resettingVelocity;
    // Use this for initialization

    void Start () {
        playerGameObject = GameObject.Find("Player");
        health = 10;
        damageDone = 3;
        objectRigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerGameObject.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        else if (playerGameObject.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();
    }
    void FixedUpdate()
    {
        if(!resettingVelocity && objectRigidBody.velocity != Vector2.zero)
        {
            resettingVelocity = true;
            StartCoroutine(resetVelocity());
        }
        gameObject.transform.position = new Vector3(
            Mathf.Lerp(gameObject.transform.position.x, playerGameObject.transform.position.x, 0.01f),
            Mathf.Lerp(gameObject.transform.position.y, playerGameObject.transform.position.y, 0.01f));
    }

    IEnumerator resetVelocity()
    {
        yield return new WaitForSeconds(1f);
        objectRigidBody.velocity = Vector2.zero;
        resettingVelocity = false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Wall"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col.collider, true);
        }
    }
}
