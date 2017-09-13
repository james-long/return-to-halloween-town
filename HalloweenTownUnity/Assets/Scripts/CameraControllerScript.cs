using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour {

    public Transform playerPosition;
    public Transform cameraPosition;
    public float lerpValue = 1f;
    BoxCollider2D boundaryObject;
    BoxCollider2D cameraCollider;
    public GameObject enemyManager;
    int previousRoom = 0;
    int roomEntered;

	// Use this for initialization
	void Start () {
        cameraCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(GameObject.Find("Boundary"))
            boundaryObject = GameObject.Find("Boundary").GetComponent<BoxCollider2D>();
        if(int.TryParse(boundaryObject.gameObject.transform.parent.gameObject.name, out roomEntered))
        {
            enemyManager.SendMessage("inRoom", roomEntered);
            if(previousRoom != roomEntered)
            {
                playerPosition.gameObject.GetComponent<PlayerResourcesController>().SendMessage("storeRespawnLocation",
                    playerPosition);
                previousRoom = roomEntered;
            }
        }
        /*cameraPosition.position = new Vector3(
            Mathf.Clamp(playerPosition.position.x, boundaryObject.bounds.min.x + cameraCollider.size.x / 2,
                boundaryObject.bounds.max.x - cameraCollider.size.x / 2),
            Mathf.Clamp(playerPosition.position.y, boundaryObject.bounds.min.y + cameraCollider.size.y / 2,
                boundaryObject.bounds.max.y - cameraCollider.size.y / 2), -10f);*/

        //smoothly moving camera
        cameraPosition.position = new Vector3(
            Mathf.Clamp(Vector3.Lerp(cameraPosition.position, playerPosition.position, lerpValue).x,
                boundaryObject.bounds.min.x + cameraCollider.size.x / 2,
                boundaryObject.bounds.max.x - cameraCollider.size.x / 2),
            Mathf.Clamp(Vector3.Lerp(cameraPosition.position, playerPosition.position, lerpValue).y,
                boundaryObject.bounds.min.y + cameraCollider.size.y / 2,
                boundaryObject.bounds.max.y - cameraCollider.size.y / 2), -10f);
    }
}
