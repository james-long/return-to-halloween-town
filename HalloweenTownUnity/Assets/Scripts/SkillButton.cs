using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {

    Image imageReference;

    public Sprite downImage;
    public Sprite originalImage;


	// Use this for initialization
	void Start () {
        imageReference = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {

    }

    void ButtonDown()
    {
        imageReference.sprite = downImage;
    }

    void ButtonUp()
    {
        imageReference.sprite = originalImage;
    }
}
