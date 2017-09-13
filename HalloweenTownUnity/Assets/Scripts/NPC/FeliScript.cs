using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeliScript : GenericNPCScript {

    public Transform newFeliLocation;
	// Use this for initialization
	void Start () {
        gameObject.transform.position = new Vector3(5440, -512, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void repositionFeli()
    {
        gameObject.transform.position = newFeliLocation.position;
    }
}
