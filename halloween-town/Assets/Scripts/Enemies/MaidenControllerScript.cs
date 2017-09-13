using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidenControllerScript : GenericEnemyControllerScript {
    Rigidbody2D objectRigidBody;
    Animator objectAnimator;
    bool vulnerable;
    // Use this for initialization

    void Start()
    {
        playerGameObject = GameObject.Find("Player");
        health = 100f;
        damageDone = 1;
        objectRigidBody = GetComponent<Rigidbody2D>();
        objectAnimator = GetComponent<Animator>();
        vulnerable = false;
        objectAnimator.SetInteger("SpiritNum", 4);
    }

        // Update is called once per frame
    void Update () {
		if(!vulnerable)
        {
            health = 100f;
        }
        objectRigidBody.velocity = Vector2.zero;
    }
    void RemoveOrb()
    {
        objectAnimator.SetInteger("SpiritNum", objectAnimator.GetInteger("SpiritNum") - 1);
        if(objectAnimator.GetInteger("SpiritNum") == 0)
        {
            vulnerable = true;
            health = 0.1f;
        }
    }
    void OnDestroy()
    {
        if (GameObject.Find("VariableManager"))
            {
                if (GameObject.Find("VariableManager").GetComponent<VariableManager>().edelStage > 0)
                    GameObject.Find("VariableManager").GetComponent<VariableManager>().edelQuest = true; }
    }
}
