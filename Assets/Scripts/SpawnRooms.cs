//WORKS BUT WITH ISSUES ON ROTATIONS AND DOUBLING UP  SPAWNPOINTS

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnRooms : MonoBehaviour
{
    
    public int someThreshold = 1;
    public int targetRooms = 10; // Number of rooms to generate
    public GameObject singleExitRoom;    // Dead end 
    public GameObject twoExitRoomCorner; // Corner room 
    public GameObject twoExitRoomOpp;    // Straight corridor room
    public GameObject threeExitRoom;     // T-shaped room
    public GameObject fourExitRoom;      // Cross-shaped room

    [SerializeField] private List<GameObject> rooms = new List<GameObject>();
    [SerializeField] private List<GameObject> possibleSpawnPoints = new List<GameObject>();
    

    void Start()
    {
        spawn();

    }
    void spawn()
    {
        for (int i = 0; i <= targetRooms; i++)
        {
            bool roomPlaced = false;
            int attempts = 0;

            while (!roomPlaced && attempts < 10) //avoid infinate loops with attempt count
            {

                GameObject newRoom = randomRoom();
                Vector3 spawnPos = getSpawnPos();
                Quaternion spawnRotation = Quaternion.identity;
                
                
                GameObject instantiatedRoom = Instantiate(newRoom, spawnPos, Quaternion.identity);
                rooms.Add(instantiatedRoom);
                addSpawnPoints(instantiatedRoom);
                roomPlaced = true;
                attempts++;
              
            }

            if (!roomPlaced)
            {
                Debug.LogWarning("Failed to place room after multiple attempts.");
            }
        }
    }

    void addSpawnPoints(GameObject room)
    {
        foreach (Transform child in room.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Spawnpoint"))
            {

                possibleSpawnPoints.Add(child.gameObject);
            }
        }
    }

    Vector3 getSpawnPos()
    {
        if (possibleSpawnPoints.Count == 0)
        {
            return Vector3.zero;
        }
        int rand = Random.Range(0, possibleSpawnPoints.Count);
        Vector3 temp = possibleSpawnPoints[rand].transform.position;
        possibleSpawnPoints.RemoveAt(rand);
        return temp;
    }


    GameObject randomRoom()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                return singleExitRoom;
            case 2:
                return twoExitRoomCorner;
            case 3:
                return threeExitRoom;
            case 4:
                return fourExitRoom;
            default:
                return null;
        }
    }
}