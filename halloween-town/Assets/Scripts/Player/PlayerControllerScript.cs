using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour {

    public float defaultSpeed = 1000f; //maximum possible speed
    public float speedSpeed = 1000f;
    public float stealthSpeed = 1000f;
    float speedBeingUsed;
    public float jumpSpeed = 1000f;
    public float cutoffJumpSpeed = 1000f;
    public float cutoffFallSpeed = 1000f;
    bool inDialogue = false;
    bool canDialogue = false;
    string currentNPC = "";
    public string state = "default";

    bool facingRight = true;
    public bool grounded = false;
    bool canAttack = true;
    bool canControl = true;
    public bool invulnerable = false;

    public Text textbox;
    public Transform groundCheckTL;
    public Transform groundCheckBR;
    public LayerMask whatIsGround; //for masking, to figure out what character can land on
    public LayerMask whatIsGroundStealth;
    public GameObject meleeAttack;
    bool firstGround;

    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    VariableManager variableManager;

    public int weaponState = 0;

	// Use this for initialization
	void Start ()
    {
        firstGround = false;
        textbox.transform.parent.gameObject.SetActive(false);
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        variableManager = GameObject.Find("VariableManager").GetComponent<VariableManager>();
        meleeAttack.SetActive(false);
        speedBeingUsed = defaultSpeed;
    }

    void buffStats()
    {
        jumpSpeed = 1000f;
    }

    void startSystemDialogue()
    {
        currentNPC = "System";
        canDialogue = false;
        textbox.transform.parent.gameObject.SetActive(true);
        textbox.SendMessage("startDialogue", currentNPC);
        inDialogue = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if (canDialogue || inDialogue)
            {
                if (!inDialogue)
                {
                    textbox.transform.parent.gameObject.SetActive(true);
                    textbox.SendMessage("startDialogue", currentNPC);
                    inDialogue = true;
                }
                else
                    textbox.SendMessage("progressDialogue", currentNPC);
            }
            else
            {
                if (canAttack && playerAnimator.GetInteger("CastingSpellType") == 0)
                {
                    playerAnimator.SetBool("Attacking", true);
                    canAttack = false;
                }
            }
        }
        if(inDialogue)
        {
            gameObject.SendMessage("stopAllControls", true);
        }
    }

    void DialoguePossible(string NPC)
    {
        if (!inDialogue)
        {
            currentNPC = NPC;
            canDialogue = true;
        }
    }
    void DialogueNotPossible()
    {
        if (!inDialogue)
        {
            currentNPC = "";
            canDialogue = false;
        }
    }

    void activateWeapon()
    {
        meleeAttack.SetActive(true);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(state == "stealth")
            grounded = Physics2D.OverlapArea(groundCheckTL.position, groundCheckBR.position, whatIsGroundStealth);
        else if (state != "stealth")
            grounded = Physics2D.OverlapArea(groundCheckTL.position, groundCheckBR.position, whatIsGround);
        if (grounded && !firstGround)
        {
            firstGround = true;
            startSystemDialogue();
        }
        playerAnimator.SetBool("Grounded", grounded);
        if (canControl)
        {

            if (grounded)
            {

                //playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0);
            }

            if (playerRigidBody.velocity.y < cutoffFallSpeed)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, cutoffFallSpeed);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerRigidBody.velocity = new Vector2(speedBeingUsed, playerRigidBody.velocity.y);
                if (!facingRight)
                    Flip();
            }

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerRigidBody.velocity = new Vector2(-speedBeingUsed, playerRigidBody.velocity.y);
                if (facingRight)
                    Flip();
            }
            else
            {
                playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
            }


            if (Input.GetKey(KeyCode.A))
            {
                if (grounded && playerRigidBody.velocity.y == 0) //replace if double jump
                {
                    playerAnimator.SetBool("Grounded", false);
                    playerRigidBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                }
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                if (playerRigidBody.velocity.y > cutoffJumpSpeed)
                {
                    playerRigidBody.velocity = (new Vector2(playerRigidBody.velocity.x, cutoffJumpSpeed));
                }
            }
            
        }
        playerAnimator.SetFloat("Speed", Mathf.Abs(playerRigidBody.velocity.x));
        playerAnimator.SetFloat("vSpeed", playerRigidBody.velocity.y);
    }

    public void AttackFinished()
    {
        playerAnimator.SetBool("Attacking", false);
        meleeAttack.SetActive(false);
        StartCoroutine(cdAttack());
    }

    void toggleSpeedBuff()
    {
        if(state == "default" || state == "stealth")
        {
            speedBeingUsed = speedSpeed;
            Physics2D.IgnoreLayerCollision(10, 16, false);
            state = "speed";
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            speedBeingUsed = defaultSpeed;
            state = "default";
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void toggleStealthBuff()
    {
        speedBeingUsed = defaultSpeed;
        if (state == "default" || state == "speed")
        {
            speedBeingUsed = stealthSpeed;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.75f);
            Physics2D.IgnoreLayerCollision(10, 16, true);
            state = "stealth";
        }
        else
        {
            speedBeingUsed = defaultSpeed;
            GetComponent<SpriteRenderer>().color = Color.white;
            Physics2D.IgnoreLayerCollision(10, 16, false);
            state = "default";
        }
    }

    IEnumerator disableStealthSoon()
    {
        yield return new WaitForSeconds(0.21f);
        toggleStealthBuff();
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void GetHit(Vector2 enemyX_damage)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            playerAnimator.SetInteger("CastingSpellType", 0);
            playerAnimator.SetTrigger("GotHit");
            StartCoroutine(hitEffects());
            StartCoroutine(stopControl(0.2f));
            if (enemyX_damage.x <= GetComponent<Transform>().position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(200, 200);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-200, 200);
            }
            playerRigidBody.SendMessage("playerWasHit", (int)enemyX_damage.y);
            
        }
    }

    void justRespawned()
    {
        StopAllCoroutines();
        StartCoroutine(respawn());
    }

    IEnumerator respawn()
    {
        invulnerable = true;
        gameObject.SendMessage("stopAllControls", true);
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.75f);
        yield return new WaitForSeconds(1.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.SendMessage("resumeAllControls");
        invulnerable = false;
    }

    IEnumerator hitEffects()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.75f);
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        invulnerable = false;
    }

    IEnumerator cdAttack()
    {
        yield return new WaitForSeconds(0.05f);
        canAttack = true;
        playerAnimator.SetBool("canAttackAnimation", true);
    }

    IEnumerator stopControl(float seconds)
    {
        gameObject.SendMessage("stopAllControls", true);
        yield return new WaitForSeconds(seconds);
        gameObject.SendMessage("resumeAllControls");
    }

    void stopAllControls(bool cancelVelocityToo)
    {
        AttackFinished();
        canControl = false;
        canAttack = false;
        if(cancelVelocityToo)
        {
            playerRigidBody.velocity = (new Vector2(0, playerRigidBody.velocity.y));
        }
    }

    void stopAttacks()
    {
        canAttack = false;
    }

    void resumeAllControls()
    {
        inDialogue = false;
        canControl = true;
        canAttack = true;
    }

    void resumeAttacks()
    {
        canAttack = true;
    }
}
