using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour {

    BoxCollider2D boundaryManager;
    Transform playerPosition;
    public GameObject boundary;

    // Use this for initialization
    void Start () {
        boundaryManager = GetComponent<BoxCollider2D>();
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        updateBoundary();
	}

    void updateBoundary()
    {
        if (playerPosition.position.x > boundaryManager.bounds.min.x &&
            playerPosition.position.x < boundaryManager.bounds.max.x &&
            playerPosition.position.y > boundaryManager.bounds.min.y &&
            playerPosition.position.y < boundaryManager.bounds.max.y)
            boundary.SetActive(true);
        else
            boundary.SetActive(false);
    }
}
