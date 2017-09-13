using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour {

    public bool feliQuest = false;
    public bool ellieQuest = false;
    public bool umbraQuest = false;
    public bool cyanideQuest = false;
    public bool edelQuest = false;
    public int feliStage = 0;
    public int ellieStage = 0;
    public int umbraStage = 0;
    public int cyanideStage = 0;
    public int edelStage = 0;
    public int systemStage = 0;
    //cyanide
    public int mushroomsKilled = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(mushroomsKilled >= 7)
        {
            cyanideQuest = true;
            if (cyanideStage < 2)
                cyanideStage = 2;
        }
        if (edelQuest && edelStage < 2)
            edelStage = 2;
	}

    void IncrementFeliStage()
    {
        if (feliStage == 1)
        {
            feliQuest = true;
            GameObject.Find("Player").SendMessage("enableSpellButton", 0, SendMessageOptions.RequireReceiver);
            systemStage = 2;
            GameObject.Find("Player").SendMessage("startSystemDialogue");
        }
        if (feliStage < 2)
        {
            feliStage++;
        }
    }

    void IncrementEllieStage()
    {
        if (ellieStage == 0)
        {
            GameObject.Find("Player").SendMessage("enableAttacks");
            systemStage = 1;
            GameObject.Find("Player").SendMessage("startSystemDialogue");
        }
        if (ellieStage == 1)
        {
            ellieQuest = true;
            GameObject.Find("Player").SendMessage("enableSpellButton", 1);
            systemStage = 3;
            GameObject.Find("Player").SendMessage("startSystemDialogue");
        }
        if (ellieStage < 2)
            ellieStage++;
    }
    void IncrementCyanideStage()
    {
        if (cyanideStage < 1)
            cyanideStage++;
        if (cyanideStage == 2)
        {
            GameObject.Find("Player").SendMessage("enableSpellButton", 3);
            systemStage = 5;
            GameObject.Find("Player").SendMessage("startSystemDialogue");
        }
        if (cyanideStage < 3 && cyanideQuest)
            cyanideStage++;
    }
    void IncrementEdelStage()
    {
        if (edelStage < 1)
            edelStage++;
        if (edelStage == 2)
        {
            GameObject.Find("Player").SendMessage("enableSpellButton", 2);
            systemStage = 4;
            GameObject.Find("Player").SendMessage("startSystemDialogue");
        }

        if (edelStage < 3 && edelQuest)
            edelStage++;
    }

    void IncrementUmbraStage()
    {
        if (umbraStage == 1)
        {
            umbraQuest = true;
            GameObject.Find("Player").SendMessage("buffStats");
        }
        if (umbraStage < 3)
            umbraStage++;
    }
}
