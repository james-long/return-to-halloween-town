using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCollision : MonoBehaviour {

    GameObject player;
    Transform objectTransform;
    PlayerControllerScript playerController;
	// Use this for initialization
	void Start () {
        objectTransform = GetComponent<Transform>();
        player = objectTransform.parent.gameObject;
        playerController = player.GetComponent<PlayerControllerScript>();
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && playerController.state != "stealth")
        {
            int colDamage = (int)col.gameObject.GetComponent<GenericEnemyControllerScript>().damageDone;
            player.SendMessage("GetHit", new Vector2(col.gameObject.transform.position.x, colDamage));
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("BouncyBlock"))
        {
            player.GetComponent<PlayerSkillManager>().doubleJumped = false;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
