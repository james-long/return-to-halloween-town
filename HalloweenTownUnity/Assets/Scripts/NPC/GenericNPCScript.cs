using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNPCScript : MonoBehaviour {
    public string npcName;
    GameObject playerGameObject;
    // Use this for initialization
    void Start()
    {
        playerGameObject = GameObject.Find("Player");
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.SendMessage("DialoguePossible", npcName);

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.SendMessage("DialogueNotPossible");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}