using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERAttack : MonoBehaviour {

    PlayerControllerScript playerController;
    PlayerSkillManager playerSkillManager;
    Collider2D objectCollider;
    List<Collider2D> thingsCollidedWith;
    float weaponDamage;
    SpriteRenderer selfRenderer;

    // Use this for initialization
    void Start () {
        playerController = FindObjectOfType<PlayerControllerScript>();
        playerSkillManager = FindObjectOfType<PlayerSkillManager>();
        objectCollider = GetComponent<Collider2D>();
        thingsCollidedWith = new List<Collider2D>();
        weaponDamage = 0.2f;
        selfRenderer = GetComponent<SpriteRenderer>();
        if (playerController.state == "stealth")
        {
            playerController.SendMessage("toggleStealthBuff");
        }
        StartCoroutine(ERSpell());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger == false && col.CompareTag("Enemy"))
        {
            if (thingsCollidedWith.Contains(col) == false)
            {
                playerSkillManager.SendMessage("RecoverMana", 5f);
                col.SendMessage("GetHit", new Vector3(gameObject.transform.position.x, weaponDamage, 0f));
                thingsCollidedWith.Add(col);
            }
        }
    }

    IEnumerator ERSpell()
    {
        for (int i = 0; i < 15; i++)
        {
            objectCollider.enabled = true;
            yield return new WaitForSeconds(0.1f);
            objectCollider.enabled = false;
            thingsCollidedWith.Clear();
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
