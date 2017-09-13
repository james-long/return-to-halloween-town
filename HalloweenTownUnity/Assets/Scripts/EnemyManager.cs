using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    int currentRoom = 0;
    public GameObject batPrefab;
    public GameObject patrollerPrefab;
    public GameObject candlePrefab;
    public GameObject ghostPrefab;
    public GameObject mushroomPrefab;
    public GameObject maidenPrefab;
    public GameObject spiritPrefab;

    public Transform activeRoom;

    Vector3[][] gameRooms;
    Vector3 vector;
    GameObject[] prefabsList;
	// Use this for initialization
	void Start () {
        prefabsList = new GameObject[7] { batPrefab, patrollerPrefab, candlePrefab, ghostPrefab, mushroomPrefab, maidenPrefab, spiritPrefab };
        gameRooms = new Vector3[24][];
        gameRooms[0] = new Vector3[0];
        gameRooms[1] = new Vector3[0];
        gameRooms[2] = new Vector3[6] { new Vector3(6, 21, 1), new Vector3(13, 21, 1), new Vector3(20, 21, 1),
                                        new Vector3(27, 21, 1), new Vector3(9, 24, 1), new Vector3(23, 24, 1) };
        gameRooms[3] = new Vector3[0];
        gameRooms[4] = new Vector3[5] { new Vector3(4, 5, 1), new Vector3(25, 12, 1),
                                        new Vector3(37, 11, 1), new Vector3(70, 11, 1),new Vector3(53, 5, 1)};
        gameRooms[5] = new Vector3[2] { new Vector3(24, 7.5f, 1), new Vector3(32, 6.5f, 1) };
        gameRooms[6] = new Vector3[3] { new Vector3(8, 6, 0), new Vector3(15, 15, 0), new Vector3(9, 28, 0) };
        gameRooms[7] = new Vector3[4] { new Vector3(9, 9, 1), new Vector3(16, 9, 1), new Vector3(8, 3, 0), new Vector3(19, 3, 0) };
        gameRooms[8] = new Vector3[0];
        gameRooms[9] = new Vector3[4] { new Vector3(9, 9, 1), new Vector3(16, 9, 1), new Vector3(8, 3, 0), new Vector3(19, 3, 0) };
        gameRooms[10] = new Vector3[0];
        gameRooms[11] = new Vector3[4] { new Vector3(9, 9, 1), new Vector3(16, 9, 1), new Vector3(8, 3, 0), new Vector3(19, 3, 0) };
        gameRooms[12] = new Vector3[0];
        gameRooms[13] = new Vector3[0];
        gameRooms[14] = new Vector3[0];
        gameRooms[15] = new Vector3[0];
        gameRooms[16] = new Vector3[5] {new Vector3(3,13,2), new Vector3(10, 13, 2), new Vector3(17, 13, 2),
                                        new Vector3(25, 13, 2), new Vector3(10,4,3)};
        gameRooms[17] = new Vector3[5] {new Vector3(4, 13,2), new Vector3(19, 13, 2), new Vector3(11, 6, 3),
                                        new Vector3(14, 9, 3), new Vector3(17,6,3)};
        gameRooms[18] = new Vector3[3] {new Vector3(3, 6,2), new Vector3(14, 6, 2), new Vector3(21, 6, 2)};
        gameRooms[19] = new Vector3[4] {new Vector3(3, 2, 2), new Vector3(3, 11, 3), new Vector3(7, 16, 3), new Vector3(14, 19, 3)};
        gameRooms[20] = new Vector3[0];
        gameRooms[21] = new Vector3[5] { new Vector3(12, 50, 4), new Vector3(22, 50, 4), new Vector3(31, 50, 4),
                                        new Vector3(6, 58, 4),new Vector3(24, 58, 4) };
        gameRooms[22] = new Vector3[5] { new Vector3(9, 66, 5), new Vector3(11, 66, 6), new Vector3(9, 48, 6),
                                        new Vector3(9, 30, 6), new Vector3(9, 19, 6)};
        gameRooms[23] = new Vector3[0];

    }
	
	// Update is called once per frame
	void Update () {
    }

    void inRoom(int roomNumber)
    {
        if(roomNumber != currentRoom)
        {
            destroyPreviousEnemies();
            moveActiveRoom(roomNumber);
            instantiateEnemies(roomNumber);
            currentRoom = roomNumber;
        }
    }

    void respawnEnemies()
    {
        moveActiveRoom(currentRoom);
        destroyPreviousEnemies();
        instantiateEnemies(currentRoom);
    }

    void instantiateEnemies(int roomNumber)
    {
       for (int i = 0; i < gameRooms[roomNumber].Length; i++)
       {
           vector = gameRooms[roomNumber][i];
           {
               Instantiate(prefabsList[(int)vector.z], new Vector3(activeRoom.position.x + vector.x * 64 + 64,
                   activeRoom.position.y - vector.y * 64 - 64, 0), Quaternion.Euler(0, 0, 0), activeRoom);
           }
       }
}

    void destroyPreviousEnemies()
    {
        foreach (Transform child in activeRoom)
        {
            Destroy(child.gameObject);
        }
    }

    void moveActiveRoom(int roomNumber)
    {
        activeRoom.position = new Vector3
            (GameObject.Find(roomNumber.ToString()).GetComponent<BoxCollider2D>().bounds.min.x,
            GameObject.Find(roomNumber.ToString()).GetComponent<BoxCollider2D>().bounds.max.y, 0);
    }
}
