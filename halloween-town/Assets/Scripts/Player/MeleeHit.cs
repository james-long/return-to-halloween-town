using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour {

    public Transform playerTransform;
    List<Collider2D> thingsCollidedWith;
    float weaponDamage;
    public Sprite defaultWeapon;
    public Sprite betterWeapon;
    public SpriteRenderer weaponRenderer;
    public Animator playerAnimator;
    public PlayerControllerScript playerController;

    void Awake()
    {
        thingsCollidedWith = new List<Collider2D>();
    }

    void Start()
    {
        thingsCollidedWith = new List<Collider2D>();
        weaponDamage = 1f;
        playerController = playerAnimator.gameObject.GetComponent<PlayerControllerScript>();
    }

    void OnEnable()
    {
        changeWeaponType(playerAnimator.GetInteger("ActiveWeaponType"));
        weaponDamage = playerAnimator.gameObject.GetComponent<PlayerSkillManager>().weaponDamage;
        if(playerController.state == "stealth")
        {
            playerController.SendMessage("disableStealthSoon");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.isTrigger == false && col.CompareTag("Enemy"))
        {
            if(thingsCollidedWith.Contains(col) == false)
            {
                playerAnimator.SendMessage("RecoverMana", 10f);
                col.SendMessage("GetHit", new Vector3(playerTransform.position.x, weaponDamage, 
                    playerAnimator.gameObject.GetComponent<PlayerSkillManager>().weaponKnockback));
                thingsCollidedWith.Add(col);
            }            
        }
    }

    void changeWeaponType(int type)
    {
        if (type == 0)
            weaponRenderer.color = Color.blue;
        else if (type % 10 == 1)
            weaponRenderer.color = Color.red;
        else if (type % 10 == 2)
            weaponRenderer.color = Color.cyan;
        else if (type % 10 == 4)
            weaponRenderer.color = Color.gray;
        if(type > 10)
        {
            weaponRenderer.sprite = betterWeapon;
        }
        else if (type < 10)
        {
            weaponRenderer.sprite = defaultWeapon;
        }
    }
    // Use this for initialization
    

	void OnDisable()
    {
        thingsCollidedWith.Clear();
    }
	// Update is called once per frame
	void Update () {

	}
}
