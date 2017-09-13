using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesController : MonoBehaviour {

    public Slider transitioningHealthSlider;
    public Slider healthSlider;

    public Slider previewManaSlider;
    public Slider manaSlider;

    float manaToBeUsed = 0;
    float playerHealth = 5;
    float playerMana = 30;
    float potentialCost = 0;

    Vector3 resLocation;

    public bool canCastQueuedSpell = true;
    PlayerControllerScript playerController;

	// Use this for initialization
	void Start () {
        healthSlider.maxValue = playerHealth;
        healthSlider.minValue = 0;
        healthSlider.value = healthSlider.maxValue;
        transitioningHealthSlider.maxValue = playerHealth;
        transitioningHealthSlider.minValue = 0;
        transitioningHealthSlider.value = transitioningHealthSlider.maxValue;
        previewManaSlider.maxValue = playerMana;
        previewManaSlider.minValue = 0;
        previewManaSlider.value = previewManaSlider.maxValue;
        manaSlider.maxValue = playerMana;
        manaSlider.minValue = 0;
        manaSlider.value = manaSlider.maxValue;
        playerController = gameObject.GetComponent<PlayerControllerScript>();
    }
	
    void storeRespawnLocation(Transform respawnLocation)
    {
        resLocation = new Vector3(respawnLocation.position.x, respawnLocation.position.y, 0);
    }

	// Update is called once per frame
	void Update () {
        if (transitioningHealthSlider.value > healthSlider.value)
        {
            transitioningHealthSlider.value -= 0.1f;
        }

        manaSlider.value += 0.01f;
        previewManaSlider.value += 0.01f;

        if (previewManaSlider.value > manaSlider.value - manaToBeUsed)
            previewManaSlider.value = manaSlider.value - manaToBeUsed;

        if (manaSlider.value >= manaToBeUsed)
            canCastQueuedSpell = true;
        else
            canCastQueuedSpell = false;
        if (healthSlider.value <= 0)
        {
            gameObject.transform.position = resLocation;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            healthSlider.value = healthSlider.maxValue;
            transitioningHealthSlider.value = transitioningHealthSlider.maxValue;
            manaSlider.value = manaSlider.maxValue;
            previewManaSlider.value = previewManaSlider.maxValue;
            gameObject.SendMessage("justRespawned");
            GameObject.Find("EnemyManager").SendMessage("respawnEnemies");     
        }
    }

    void Heal(float percent)
    {
        healthSlider.value += (percent / 100) * healthSlider.maxValue;
        transitioningHealthSlider.value += (percent / 100) * transitioningHealthSlider.maxValue;
    }

    void RecoverMana(float percent)
    {
        manaSlider.value += (percent / 100) * healthSlider.maxValue;
        previewManaSlider.value += (percent / 100) * transitioningHealthSlider.maxValue;
    }

    void playerWasHit(float amount)
    {
        if(playerController.state == "speed")
            healthSlider.value -= amount * 1.5f;
        else
            healthSlider.value -= amount;
    }

    void consumeMana()
    {
        if(manaToBeUsed <= manaSlider.value)
        {
            manaSlider.value -= manaToBeUsed;
        }
    }
    
    void resetManaToBeUsed()
    {
        manaToBeUsed = 0;
        previewManaSlider.value = manaSlider.value;
    }

    void resetManaPreview()
    {
        manaToBeUsed = 0;
        previewManaSlider.value = manaSlider.value;
    }

    void spellQueued(KeyCode spellKey)
    {
        if (spellKey == KeyCode.Q)
            potentialCost = 2f;
        else if (spellKey == KeyCode.W)
            potentialCost = 4.5f;
        else if (spellKey == KeyCode.E)
            potentialCost = 3.6f;
        else if (spellKey == KeyCode.R)
            potentialCost = 4.7f;
        if (potentialCost == manaToBeUsed)
            manaToBeUsed = manaSlider.maxValue - 5;
        else
            manaToBeUsed += potentialCost;
        previewManaSlider.value = manaSlider.value - manaToBeUsed;
    }

    void maxHealthIncreased(float amount)
    {
        playerHealth += amount;
        healthSlider.maxValue = playerHealth;
    }
}
