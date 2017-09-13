using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    Text ownText;
    VariableManager variableManager;
    string currentNPC = "";
    public Sprite[] headSprites = new Sprite[21];
    public Image charaHeadImage;
    
    string[][] feli = new string[3][];
    string[][] ellie = new string[3][];
    string[][] umbra = new string[4][];
    string[][] cyanide = new string[4][];
    string[][] edel = new string[4][];
    string[][] system = new string[6][];

    int[][] feliH = new int[3][];
    int[][] ellieH = new int[3][];
    int[][] umbraH = new int[4][];
    int[][] cyanideH = new int[4][];
    int[][] edelH = new int[4][];
    int[][] systemH = new int[6][];

    GameObject playerObject;
    int textNum = 0;
	// Use this for initialization
	void Awake () {
        
        ownText = gameObject.GetComponent<Text>();
        ownText.text = "";
        variableManager = GameObject.Find("VariableManager").GetComponent<VariableManager>();
        playerObject = GameObject.Find("Player");


        feliH[0] = new int[14] { 0, 1, 1, 0, 3, 1, 1, 1, 1, 3, 1, 0, 1, 1 };
        feliH[1] = new int[4] {2,1,1,1};
        feliH[2] = new int[3] { 1, 0, 1 }; 
        ellieH[0] = new int[18] { 6, 6, 4, 4, 6, 7, 4, 7, 7, 4, 4, 4, 4, 4, 7, 7, 6, 4 };
        ellieH[1] = new int[8] { 4, 4, 4, 4, 7, 5, 7, 6 };
        ellieH[2] = new int[3] { 4, 4, 7 };

        cyanideH[0] = new int[12] { 15, 13, 13, 13, 13, 12, 12, 12, 13, 13, 13, 12 };
        cyanideH[1] = new int[2] {14,14};
        cyanideH[2] = new int[4] { 15, 13, 12, 13 };
        cyanideH[3] = new int[3] { 12, 15, 13 };

        //edel 16 17 :) 18 :( 19 :o
        edelH[0] = new int[13] {19,18,18,18,18,19,16,18,18,18,18,19,17}; 
        edelH[1] = new int[3] {19,19,18};
        edelH[2] = new int[4] {17,17,18,17};
        edelH[3] = new int[2] {17,17};

        //umbra 8 neut 9 :) 10 :( 11 :0
        umbraH[0] = new int[14] { 10,10,11,11,8,8,10,11,8,11,10,10,11,11};
        umbraH[1] = new int[21] { 9, 9, 9, 9, 8, 11, 9, 11, 11, 10, 10, 9, 9, 11, 11, 9, 10, 9, 10, 11, 9 };
        umbraH[2] = new int[15] { 9, 11, 10, 10, 10, 10, 10, 10, 10, 10, 10, 8, 9, 9, 10 };
        umbraH[3] = new int[1] { 10 };


        //system
        system[0] = new string[9] {"What happened?\n\n(hit S to continue)",
            "You were just out trick-or-treating, and suddenly you're transported to some new mysterious world.",
            "Something is definitely out of sorts here.",
            "This whole thing feels so ...",
            "...",
            "...cliche.",
            "Like you've seen this premise in some weird dating sim before or something.",
            "Well, no matter, you'd better get moving.",
            "(Use the arrow keys to move and A to jump)"}; //explain movement, npc interaction
        system[1] = new string[2] {"You learned a basic attack!",
            "Hit S to perform a basic attack."};
        system[2] = new string[5] {"You unlocked the fire spell!",
        "Hit Q to queue the spell up, and use SPACE to cast your queue.",
        "If you've learned a basic attack, the fire spell buffs its knockback.",
        "You can also combine it with other elements for interesting effects.",
        "...if you have any other elements, that is."};
        system[3] = new string[5] {"You unlocked the wind spell!",
        "Hit W to queue the spell up, and use SPACE to cast your queue.",
        "If you've learned a basic attack, the wind spell buffs its speed.",
        "You can also combine it with other elements for interesting effects.",
        "...if you have any other elements, that is."};
        system[4] = new string[5] {"You unlocked the water spell!",
        "We've been through this before, but hit E to queue the spell up and use SPACE to cast your queue.",
        "The water spell heals you at the cost of mana.",
        "You can also combine it with other elements for interesting effects.",
        "...but you already know that, right?"};
        system[5] = new string[5] {"You unlocked the dark spell!",
        "Again, hit E to queue the spell up and use SPACE to cast your queue.",
        "The dark spell increases your weapon's cast time but massively buffs its damage as well.",
        "You can also combine it with other elements for interesting effects.",
        "...but this is all old news, right?"};

        //feli
        feli[0] = new string[14] { "Oh? I've never seen you around here before, nya ~ !",
            "And a human, too! The last human who was here was bundles of fun.",
            "I'm so happy, nya ~ ! Will you have fun together with Feli too?",
            "...",
            "Hmm ~ ? Not much of a chatter, are you?",
            "That's okay! Feli doesn't mind as long as we can play together.",
            "Oh! To get to know each other a little better, why don't we play a game?",
            "I'll run away, and if you can find Feli, Feli will give you a prize!",
            "Sounds fun, doesn't it, nya ~ ?",
            "Ooor.. maybe you're scared to lose to a girl cuter than you ~ ?",
            "So how about it?",
            "I'll be hiding somewhere up there.",
            "Will you fight? Or will you perish like a dog ~ ?",
            ";3"};
        feli[1] = new string[4] {"Awwww, you found Feli, nya ~ !",
            "You're more fun than I expected!",
            "I guess I have to give you this then ~ I have no idea what it is but it's shiny!",
            "Come play with Feli again sometime, nya!"};
        feli[2] = new string[3] {"It was fun playing with you!",
            "If you haven't done so already, I think my friend Ellie needs some help back in the park.",
            "Do your best nya ~ !"};

        //ellie
        ellie[0] = new string[18] { "Oh my god, watch where you're going!",
            "Great, another human who doesn't know the authority of the Graves Family.",
            "As the next in line as the Violet Witch of Halloween Town, I have to educate you!",
            "My name is Ellie Graves, the daughter of one of the most powerful families around these parts!",
            "Respect my authority!",
            "Wh-what?",
            "I thought even a dumb human like you would be able to understand what I said.",
            "Ugh. Well I gueeeeeess there a way you can make up for it.",
            "Listen here! I.. uh, kind of... misplaced one of the ingredients for my potion.",
            "If you can get it back for me, I shall reward you greatly!",
            "...",
            "Wait, you don't have anything to defend yourself?",
            "How did you even get here?",
            "Well, I guess I can teach you a bit of self-defence.",
            "I-It's not a big deal or anything.",
            "...",
            "Well, what are you waiting for? Hurry up and go!",
            "The ingredient should be in one of the caves to the right."};
        ellie[1] = new string[8] {"Hey there.",
            "You're ... kinda slow, aren't you?",
            "I got tired of waiting, so I just flew down here and got the ingredient myself.",
            "...",
            "W-well, I do appreciate you working so hard to get here, at least.",
            "On the honor of the Graves family, I present you with this reward.",
            "Wh-what do you mean you already have something like it? You should be grateful that the Violet Witch is giving you such a gracious gift!",
            "Now off with you! Someone like me has no time to loiter in a place like this." };
        ellie[2] = new string[3] {"No, seriously, no loitering around here.",
            "This place is pretty dangerous.",
            "I- Not that I'm too concerned about someone like you or anything, though."};

        cyanide[0] = new string[12] {"Oh? What is such a pretty lady like you doing in a place like this?",
            "And that delicious smell... could you be a human?",
            "It's been so long since I've tasted the blood of a human...",
            "Haha, I like that expression you're making.",
            "Don't worry, I won't take a bite out of you. Unless it's consensual, of course ; )",
            "Oh, I do apologize, but I must take my leave. As a matter of fact, I am visiting a good friend of mine at the moment.",
            "...",
            "Well, actually, I do have a favour to ask.",
            "It seems I've misplaced something of mine. It would be lovely if you could assist me in finding this object?",
            "Perhaps I lost it to the left, where all those mushrooms were milling around. That seems like an adequate place to begin searching.",
            "Come back after you've ... removed ... some of them, and we'll see if you've found my item.",
            "It's very important, so you should get going."};
        cyanide[1] = new string[2] {"I don't think you have the item I'm looking for.",
            "Come back to me after you get rid of some more mushrooms."};
        cyanide[2] = new string[4] {"Oh, it's you again.",
            "It is rather shameful to admit, but it seems that I had the item all this time, haha!",
            "It appears as though you have found something quite interesting, however. Perhaps you wouldn't mind keeping that as compensation?",
            "Well, I can't keep my friend waiting. It was a pleasure to meet you."};
        cyanide[3] = new string[3] { "That wall to the right...",
            "It feels like you can almost walk right through it, but it's completely solid.",
            "A passageway for ghosts, perhaps? Haha!"};

        //edel
        edel[0] = new string[13] {"W-whoa! Where did you come from?",
            "Ugh, this day just keeps getting worse and worse.",
            "First my garden gets ransacked by some strange creature, and now a human wanders into my forest?",
            "The forest spirits won't even listen to my messages...",
            "Sorry for being so blunt, but could you please leave? I'm not in a very good mood right now.",
            "Wait wait wait. Since you're here already, do you mind helping me with something?",
            "You see, there's this creature that showed up this morning.",
            "I tried asking it to leave, but straight-up ignored me and started eating all my plants!",
            "I took so much care in growing them, and even managed to finally fill up all the flowerpots I was gifted by the last human who was here...",
            "And... well, it's a mess now.",
            "Even my friend over there couldn't take care of it...",
            "Maybe there's some trick behind the creature's high defense?",
            "If you could take care of it for me, I'd be much in your debt!"};
        edel[1] = new string[3] { "That creature doesn't listen to reason at all!",
        "It caused some weird spirits to show up all around my garden as well...",
        "What a pest!"};
        edel[2] = new string[4] {"Wow! Thank you so much, my garden looks livelier already!",
            "I don't have much to give you, but here's one of the products of my plants!",
            "I'm sorry I can't reward you with more, but that weird creature destroyed most of my things...",
            "Come visit again soon!"};
        edel[3] = new string[2] {"Byebye!",
            "Come again soon!"};

        //umbra
        umbra[0] = new string[14] {"A..ahh! P-please don't attack me...",
            "I-I know I'm a ghost and all... but it's still scary if someone tries to attack me...",
            "O-oh... um...",
            "I didn't mean.. to um... tell you what to do...",
            "If you really want to... you can attack me... I guess...",
            "...",
            "S-sorry... I guess you wouldn't really want to talk to someone like me...",
            "Oh! Wait a second.. um, you're a human, right?",
            "I.. was just wondering... if you could... uh, do me a huge favor?",
            "Y-you don't have to if you don't want to! But um... it would really help out...",
            "Some of the other ghosts were bullying me... since I used to be a human unlike them...",
            "They took one of my books that one of my dear friends gave me! I-I'd be really grateful if you could get it back for me...",
            "S-sorry if it sounds like I'm forcing you! You really don't have to if you don't want to...",
            "I'll go look for it as well!"
            };
        umbra[1] = new string[21] {"Oh.. hey, there you are!",
            "I found my book all the way up here...",
            "It wasn't too hard to get, though...",
            "Since I'm a ghost and I can, uh...",
            "Fly...",
            "What? You... you jumped all the way up here?",
            "I... I appreciate the effort, at least...",
            "Um... I'll give you anything I can... just name it!",
            "Oh... um.. right...",
            "I don't really have anything on me right now",
            "Since everything just... um... falls out of my pockets...",
            "Well, I can at least buff your stats up a little bit...",
            "It won't hurt at all! Um... if... you'd like, of course...",
            "...",
            "There we go!",
            "Um... you're not from around here, right?",
            "You should probably head back to the clocktower soon ... if you want to go back home, that is.",
            "You should be able to reach the top now...",
            "You have to hurry though...!",
            "I-I'll meet you there to help you warp back home!",
            "...If you want..."};
        umbra[2] = new string[15] {"Hey...",
            "Um, you took a quite a while getting here...",
            "...",
            "I guess it's a bit hard without being able to fly...",
            "I... ah...",
            "The ... the clocktower just struck three.",
            "You were a bit too late... getting back up here...",
            "...",
            "You might be... stuck here for a while, I guess...",
            "... like, forever...",
            "I'm sorry I wasn't able to help out.",
            "...and hey, it's not all that bad...",
            "I'm sure you'll get used to living here in no time.",
            "...here in Halloween Town...",
            "[BAD END?]"};
        umbra[3] = new string[1] { "[BAD END?]" };
    }
	
	// Update is called once per frame
	void Update () {
	}

    void progressDialogue(string NPC)
    {
        textNum++;
        if (NPC == "Feli")
        {
            if (textNum == feli[variableManager.feliStage].Length)
            {
                if (variableManager.feliStage == 0)
                    GameObject.Find("Feli").SendMessage("repositionFeli");
                playerObject.SendMessage("resumeAllControls");

                gameObject.transform.parent.gameObject.SetActive(false);
                variableManager.SendMessage("IncrementFeliStage");
            }
            else
            {
                ownText.text = feli[variableManager.feliStage][textNum];
                charaHeadImage.sprite = headSprites[feliH[variableManager.feliStage][textNum]];
            }
                
        }
        if (NPC == "Ellie")
        {
            if (textNum == ellie[variableManager.ellieStage].Length)
            {
                if (variableManager.ellieStage == 0)
                    GameObject.Find("Ellie").SendMessage("repositionEllie");
                playerObject.SendMessage("resumeAllControls");

                gameObject.transform.parent.gameObject.SetActive(false);
                variableManager.SendMessage("IncrementEllieStage");
            }
            else
            {
                ownText.text = ellie[variableManager.ellieStage][textNum];
                charaHeadImage.sprite = headSprites[ellieH[variableManager.ellieStage][textNum]];
            }
            }
        if (NPC == "Umbra")
        {
            if (textNum == umbra[variableManager.umbraStage].Length)
            {
                if (variableManager.umbraStage == 0)
                    GameObject.Find("Umbra").SendMessage("repositionUmbra", 1);
                if (variableManager.umbraStage == 1)
                    GameObject.Find("Umbra").SendMessage("repositionUmbra", 2);
                playerObject.SendMessage("resumeAllControls");

                gameObject.transform.parent.gameObject.SetActive(false);
                variableManager.SendMessage("IncrementUmbraStage");
            }
            else
            {
                ownText.text = umbra[variableManager.umbraStage][textNum];
                charaHeadImage.sprite = headSprites[umbraH[variableManager.umbraStage][textNum]];
            }
        }
        if (NPC == "Cyanide")
        {
            if (textNum == cyanide[variableManager.cyanideStage].Length)
            {
                playerObject.SendMessage("resumeAllControls");
                gameObject.transform.parent.gameObject.SetActive(false);
                variableManager.SendMessage("IncrementCyanideStage");
            }
            else
            {
                ownText.text = cyanide[variableManager.cyanideStage][textNum];
                charaHeadImage.sprite = headSprites[cyanideH[variableManager.cyanideStage][textNum]];
            }
        }
        if (NPC == "Edel")
        {
            if (textNum == edel[variableManager.edelStage].Length)
            {
                playerObject.SendMessage("resumeAllControls");

                gameObject.transform.parent.gameObject.SetActive(false);
                variableManager.SendMessage("IncrementEdelStage");
            }
            else
            {
                ownText.text = edel[variableManager.edelStage][textNum];
                charaHeadImage.sprite = headSprites[edelH[variableManager.edelStage][textNum]];
            }
        }
        if(NPC == "System")
        {
            if (textNum == system[variableManager.systemStage].Length)
            {
                playerObject.SendMessage("resumeAllControls");
                gameObject.transform.parent.gameObject.SetActive(false);
                playerObject.SendMessage("DialogueNotPossible");
            }
            else
            {
                ownText.text = system[variableManager.systemStage][textNum];
                charaHeadImage.sprite = headSprites[20];
            }
        }
    }

    void startDialogue(string NPC)
    {
        GameObject.Find("EnemyManager").SendMessage("destroyPreviousEnemies");
        textNum = 0;
        currentNPC = NPC;
        if(NPC == "Feli")
        {
            ownText.text = feli[variableManager.feliStage][textNum];
            charaHeadImage.sprite = headSprites[feliH[variableManager.feliStage][textNum]];
            charaHeadImage.color = Color.white;
        }
        if (NPC == "Ellie")
        {
            ownText.text = ellie[variableManager.ellieStage][textNum];
            charaHeadImage.sprite = headSprites[ellieH[variableManager.ellieStage][textNum]];
            charaHeadImage.color = Color.white;
        }
        if (NPC == "Umbra")
        {
            ownText.text = umbra[variableManager.umbraStage][textNum];
            charaHeadImage.sprite = headSprites[umbraH[variableManager.umbraStage][textNum]];
            charaHeadImage.color = Color.white;
        }
        if (NPC == "Cyanide")
        {
            ownText.text = cyanide[variableManager.cyanideStage][textNum];
            charaHeadImage.sprite = headSprites[cyanideH[variableManager.cyanideStage][textNum]];
            charaHeadImage.color = Color.white;
        }
        if (NPC == "Edel")
        {
            ownText.text = edel[variableManager.edelStage][textNum];
            charaHeadImage.sprite = headSprites[edelH[variableManager.edelStage][textNum]];
            charaHeadImage.color = Color.white;
        }
        if(NPC == "System")
        {
            ownText.text = system[variableManager.systemStage][textNum];
            charaHeadImage.sprite = headSprites[20];
            charaHeadImage.color = Color.gray;
        }
    }
}
