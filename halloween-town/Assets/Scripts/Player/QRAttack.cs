using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRAttack : MonoBehaviour {

    public float speed;
    PlayerSkillManager playerSkillManager;
    PlayerControllerScript playerController;
    List<Collider2D> thingsCollidedWith;
    Collider2D objectCollider;
    Rigidbody2D objectRigidBody;
    float weaponDamage;
    // Use this for initialization
    void Start () {
        playerController = FindObjectOfType<PlayerControllerScript>();
        playerSkillManager = FindObjectOfType<PlayerSkillManager>();
        objectCollider = GetComponent<Collider2D>();
        thingsCollidedWith = new List<Collider2D>();
        weaponDamage = 30f;
        objectRigidBody = GetComponent<Rigidbody2D>();
        if(playerController.gameObject.transform.localScale.x < 0)
        {
            speed = -speed;
            objectRigidBody.AddForce(new Vector2(-500f, 500f), ForceMode2D.Impulse);
        }
        else
        {
            objectRigidBody.AddForce(new Vector2(500f, 500f), ForceMode2D.Impulse);
        }
        if (playerController.state == "stealth")
        {
            playerController.SendMessage("toggleStealthBuff");
        }
        StartCoroutine(flickerHitbox());
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.isTrigger == false && col.CompareTag("Enemy"))
        {
            if (thingsCollidedWith.Contains(col) == false)
            {
                playerSkillManager.SendMessage("RecoverMana", 5f);
                col.SendMessage("GetHit", new Vector3(gameObject.transform.position.x, weaponDamage, 50f));
                thingsCollidedWith.Add(col);
            }
        }
    }
    
    IEnumerator flickerHitbox()
    {
        for (int i = 0; i < 10; i++)
        {
            objectCollider.enabled = true;
            yield return new WaitForSeconds(0.1f);
            objectCollider.enabled = false;
            thingsCollidedWith.Clear();
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }

    void Update () {
        //objectRigidBody.velocity = new Vector2(speed, objectRigidBody.velocity.y);
        objectRigidBody.MoveRotation(objectRigidBody.rotation + 10f);
    }
}
