using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QEAttack : MonoBehaviour {

    public Transform playerTransform;
    public Animator playerAnimator;
    public PlayerControllerScript playerController;
    List<Collider2D> thingsCollidedWith;
    float weaponDamage;
    // Use this for initialization
    void Start () {
        thingsCollidedWith = new List<Collider2D>();
        weaponDamage = 0.2f;
    }

    void OnEnable()
    {
        if (playerController.state == "stealth")
        {
            playerController.SendMessage("toggleStealthBuff");
        }
    }

    void OnColliderEnable()
    {
    }

    void OnColliderDisable()
    {
        thingsCollidedWith.Clear();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Enemy"))
        {
            if (thingsCollidedWith.Contains(col) == false)
            {
                playerAnimator.SendMessage("RecoverMana", 5f);
                col.SendMessage("GetHit", new Vector3(playerTransform.position.x, weaponDamage, 0f));
                thingsCollidedWith.Add(col);
            }
        }
    }
    // Update is called once per frame
    void Update ()
    {

	}
}
