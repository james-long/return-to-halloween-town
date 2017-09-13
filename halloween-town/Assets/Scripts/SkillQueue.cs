using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillQueue : MonoBehaviour {

    Image imageReference;

    public Sprite option0;
    public Sprite option1;
    public Sprite option2;
    public Sprite option3;
    public Sprite nullImage;

    // Use this for initialization
    void Start () {
        imageReference = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void UpdateImage(int imageToUpdateTo)
    {
        switch(imageToUpdateTo)
        {
            case 0:
                imageReference.sprite = option0;
                break;
            case 1:
                imageReference.sprite = option1;
                break;
            case 2:
                imageReference.sprite = option2;
                break;
            case 3:
                imageReference.sprite = option3;
                break;
            case 4:
                imageReference.sprite = nullImage;
                break;
            default: break;
        }
    }
}
