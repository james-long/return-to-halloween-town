using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritControllerScript : GenericEnemyControllerScript {
    Rigidbody2D objectRigidBody;
    // Use this for initialization

    void Start()
    {
        playerGameObject = GameObject.Find("Player");
        health = 0.5f;
        damageDone = 1;
        objectRigidBody = GetComponent<Rigidbody2D>();
        objectRigidBody.velocity = Vector2.zero;
    }

    void OnDestroy()
    {
        if (FindObjectOfType<MaidenControllerScript>())
            FindObjectOfType<MaidenControllerScript>().gameObject.SendMessage("RemoveOrb");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
