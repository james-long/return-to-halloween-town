using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllieScript : GenericNPCScript {

    System.Random random = new System.Random();
    // Use this for initialization
    void Start () {
        gameObject.transform.position = new Vector3(4608, -832, 0);
        gameObject.transform.localScale = new Vector3(-1, 1, 1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void repositionEllie()
    {
        int randomval = random.Next(0,2);
        gameObject.transform.position = new Vector3(10304, -1216 - randomval*64*12, 0);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
