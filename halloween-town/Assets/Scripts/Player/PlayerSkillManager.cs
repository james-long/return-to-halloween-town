using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour {

    Queue<KeyCode> spellQueue = new Queue<KeyCode>();
    GameObject[] spellButtons = new GameObject[4];

    public float QWJumpForce = 2000f;

    public GameObject button0;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject qButton1;
    public GameObject qButton2;

    public GameObject weapon;
    public float weaponDamage;
    public float weaponKnockback;
    public GameObject ERSkill;
    public GameObject QRSkill;
    public Transform QRSpawn;
    public Transform ERSpawn;
    public Collider2D QESkill;

    public bool spellHappening = false;
    PlayerResourcesController resourceController;
    Animator playerAnimator;
    Rigidbody2D playerRigidBody;
    PlayerControllerScript playerController;

    bool castingSpell = false;
    public bool doubleJumped = false;

    bool attacksUnlocked = false;

    bool QEhappening = false;

    // Use this for initialization
    void Start () {
        spellButtons[0] = button0;
        spellButtons[1] = button1;
        spellButtons[2] = button2;
        spellButtons[3] = button3;
        resourceController = GetComponent<PlayerResourcesController>();
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetInteger("ActiveWeaponType", 0);
        playerAnimator.SetBool("canAttackAnimation", true);
        weaponDamage = 1f;
        weaponKnockback = 200f;
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerController = gameObject.GetComponent<PlayerControllerScript>();
        QESkill.gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
            spellButtons[i].SetActive(false);
    }

    void enableSpellButton(int number)
    {
        spellButtons[number].SetActive(true);
    }
    void enableAttacks()
    {
        attacksUnlocked = true;
        playerController.SendMessage("resumeAttacks");
    }

    void canAttackAnimationFalse()
    {
        playerAnimator.SetBool("canAttackAnimation", false);
    }
    void canAttackAnimationTrue()
    {
        playerAnimator.SetBool("canAttackAnimation", true);
    }
    void FixedUpdate()
    {
        if(playerController.grounded)
        {
            doubleJumped = false;
        }
    }

    // Update is called once per frame
    void Update () {
        if(!attacksUnlocked)
        {
            playerController.SendMessage("stopAttacks");
        }
        if (!castingSpell)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (spellButtons[0].activeSelf)
                {
                    if (addToQueue(KeyCode.Q))
                    {
                        spellButtons[0].SendMessage("ButtonDown");
                        if (spellQueue.Count == 1)
                        {
                            qButton1.SendMessage("UpdateImage", 0, SendMessageOptions.RequireReceiver);
                        }
                        else
                        {
                            qButton2.SendMessage("UpdateImage", 0, SendMessageOptions.RequireReceiver);
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (spellButtons[1].activeSelf)
                {
                    if (addToQueue(KeyCode.W))
                    {
                        spellButtons[1].SendMessage("ButtonDown");
                        if (spellQueue.Count == 1)
                        {
                            qButton1.SendMessage("UpdateImage", 1);
                        }
                        else
                        {
                            qButton2.SendMessage("UpdateImage", 1);
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (spellButtons[2].activeSelf)
                {
                    if (addToQueue(KeyCode.E))
                    {
                        spellButtons[2].SendMessage("ButtonDown");
                        if (spellQueue.Count == 1)
                        {
                            qButton1.SendMessage("UpdateImage", 2);
                        }
                        else
                        {
                            qButton2.SendMessage("UpdateImage", 2);
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (spellButtons[3].activeSelf)
                {
                    if (addToQueue(KeyCode.R))
                    {
                        spellButtons[3].SendMessage("ButtonDown");
                        if (spellQueue.Count == 1)
                        {
                            qButton1.SendMessage("UpdateImage", 3);
                        }
                        else
                        {
                            qButton2.SendMessage("UpdateImage", 3);
                        }
                    }
                }
            }
        
            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (resourceController.canCastQueuedSpell && !spellHappening)
                {
                    if (spellQueue.Count == 1)
                    {
                        startCastingSpell(1);
                        gameObject.SendMessage("consumeMana");
                    }
                    else if (spellQueue.Count == 2)
                    {
                        if (spellQueue.Contains(KeyCode.Q) && spellQueue.Contains(KeyCode.W))
                        {
                            if (!doubleJumped)
                            {
                                startCastingSpell(2);
                                gameObject.SendMessage("consumeMana");
                            }
                        }
                        else if (spellQueue.Contains(KeyCode.Q) && spellQueue.Contains(KeyCode.E)
                            || spellQueue.Contains(KeyCode.W) && spellQueue.Contains(KeyCode.R))
                        {
                            if (!QEhappening)
                            {
                                startCastingSpell(1);
                                gameObject.SendMessage("consumeMana");
                            }
                        }
                        else 
                        {
                            startCastingSpell(1);
                            gameObject.SendMessage("consumeMana");
                        }
                    }
                    
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (spellButtons[0].activeSelf || spellButtons[1].activeSelf || spellButtons[2].activeSelf || spellButtons[3].activeSelf)
            {
                gameObject.SendMessage("resetManaPreview");
                spellQueue.Clear();
                for (int i = 0; i < 4; i++)
                {
                    if(spellButtons[i].activeSelf)
                        spellButtons[i].SendMessage("ButtonUp");
                }
                qButton1.SendMessage("UpdateImage", 4);
                qButton2.SendMessage("UpdateImage", 4);
            }
        }
        
    }

    bool addToQueue(KeyCode keyToAdd)
    {
        if (spellQueue.Count < 2)
        {
            spellQueue.Enqueue(keyToAdd);
            gameObject.SendMessage("spellQueued", keyToAdd);
            return true;
        }
        return false;
    }

    void startCastingSpell(int spellType)
    {
        playerAnimator.SetInteger("CastingSpellType", spellType);
        gameObject.SendMessage("stopAllControls", true);
        castingSpell = true;
    }

    void resumeAllControls()
    {
        castingSpell = false;
    }

    void stopAllControls(bool uselessBoolDontUseThis)
    {
        castingSpell = true;
    }

    void castOneSpell(KeyCode spell)
    {
        if(spell == KeyCode.Q)
        {
            if (playerAnimator.GetInteger("ActiveWeaponType") == 1)
            {
                changeWeaponProperties(0);
            }
            else
            {
                changeWeaponProperties(1);
            }
        }
        else if (spell == KeyCode.W)
        {
            if (playerAnimator.GetInteger("ActiveWeaponType") == 2)
            {
                changeWeaponProperties(0);
            }
            else
            {
                changeWeaponProperties(2);
            }
        }
        else if (spell == KeyCode.E)
        {
            playerAnimator.SendMessage("Heal", 10f);
        }
        else if (spell == KeyCode.R)
        {
            if (playerAnimator.GetInteger("ActiveWeaponType") == 4)
            {
                changeWeaponProperties(0);
            }
            else
            {
                changeWeaponProperties(4);
            }
        }        
    }

    void castTwoSpell(KeyCode spell1, KeyCode spell2)
    {
        if(spell1 == spell2)
        {
            if (spell1 == KeyCode.Q)
            {
                if (playerAnimator.GetInteger("ActiveWeaponType") == 11)
                {
                    changeWeaponProperties(0);
                }
                else
                {
                    changeWeaponProperties(11);
                }
            }
            else if (spell1 == KeyCode.W)
            {
                if (playerAnimator.GetInteger("ActiveWeaponType") == 22)
                {
                    changeWeaponProperties(0);
                }
                else
                {
                    changeWeaponProperties(22);
                }
            }
            else if (spell1 == KeyCode.E)
            {
                playerAnimator.SendMessage("Heal", 75f);
            }
            else if (spell1 == KeyCode.R)
            {
                if (playerAnimator.GetInteger("ActiveWeaponType") == 44)
                {
                    changeWeaponProperties(0);
                }
                else
                {
                    changeWeaponProperties(44);
                }
            }
        }
        else
        {
            if(spell1 == KeyCode.W || spell2 == KeyCode.W) //mobility
            {
                if(spell1 == KeyCode.Q || spell2 == KeyCode.Q) //WQ
                {
                    playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0);
                    playerRigidBody.AddForce(new Vector2(0f, QWJumpForce), ForceMode2D.Impulse);
                    doubleJumped = true;
                }
                if (spell1 == KeyCode.E || spell2 == KeyCode.E) //WE
                {
                    gameObject.SendMessage("toggleSpeedBuff");
                }
                if (spell1 == KeyCode.R || spell2 == KeyCode.R) //WR
                {
                    gameObject.SendMessage("toggleStealthBuff");
                }
            }
            else if(spell1 == KeyCode.E || spell2 == KeyCode.E)
            {
                if (spell1 == KeyCode.R || spell2 == KeyCode.R) //ER
                {
                    Instantiate(ERSkill, ERSpawn.position, ERSpawn.rotation);
                }
                if (spell1 == KeyCode.Q || spell2 == KeyCode.Q) //QE
                { 
                    StartCoroutine(QESpell());
                }
            }
            else if (spell1 == KeyCode.Q || spell2 == KeyCode.Q)
            {
                if (spell1 == KeyCode.R || spell2 == KeyCode.R) //QR
                {
                    Instantiate(QRSkill, QRSpawn.position, QRSpawn.rotation);
                }
            }
        }
    }



    IEnumerator QESpell()
    {
        QEhappening = true;
        QESkill.gameObject.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            QESkill.enabled = true;
            QESkill.SendMessage("OnColliderEnable");
            yield return new WaitForSeconds(0.1f);
            QESkill.enabled = false;
            QESkill.SendMessage("OnColliderDisable");
            yield return new WaitForSeconds(0.1f);
        }
        QESkill.gameObject.SetActive(false);
        QEhappening = false;
    }

    void changeWeaponProperties (int weaponType)
    {
        if(weaponType == 0)
        {
            playerAnimator.SetInteger("ActiveWeaponType", 0);
            weaponKnockback = 200f;
            weaponDamage = 1f;
        }
        if (weaponType == 1)
        {
            playerAnimator.SetInteger("ActiveWeaponType", 1);
            weaponKnockback = 400f;
            weaponDamage = 1f;
        }
        if (weaponType == 11)
        {
            playerAnimator.SetInteger("ActiveWeaponType", 11);
            weaponKnockback = 800f;
            weaponDamage = 1f;
        }
        if (weaponType == 2)
        {
            playerAnimator.SetInteger("ActiveWeaponType", 2);
            weaponKnockback = 200f;
            weaponDamage = 0.5f;
        }
        if (weaponType == 22)
        {
            playerAnimator.SetInteger("ActiveWeaponType", 22);
            weaponKnockback = 200f;
            weaponDamage = 0.75f;
        }
        if (weaponType == 4)
        {
            playerAnimator.SetInteger("ActiveWeaponType", 4);
            weaponKnockback = 200f;
            weaponDamage = 3f;
        }
        if (weaponType == 44)
        {
            playerAnimator.SetInteger("ActiveWeaponType", 44);
            weaponKnockback = 200f;
            weaponDamage = 6f;
        }
    }



    void resetAnimatorVariable()
    {
        playerAnimator.SetInteger("CastingSpellType", 0);
        gameObject.SendMessage("resumeAllControls");
        for (int i = 0; i < 4; i++)
        {
            if(spellButtons[i].activeSelf)
                spellButtons[i].SendMessage("ButtonUp");
        }
        qButton1.SendMessage("UpdateImage", 4);
        qButton2.SendMessage("UpdateImage", 4);
        playerAnimator.SendMessage("resetManaToBeUsed");
        if (spellQueue.Count == 1)
        {
            castOneSpell(spellQueue.Dequeue());
        }
        else if (spellQueue.Count == 2)
        {
            castTwoSpell(spellQueue.Dequeue(), spellQueue.Dequeue());
        }
        spellQueue.Clear();
    }
}
