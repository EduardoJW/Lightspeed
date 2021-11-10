using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSurvivor : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject floorTile;
    public int numberOfLifes = 3;
    public int numberOfPlayers = 2;

    private GameObject[] players;

    private Transform transformBase;


    // Start is called before the first frame update
    void Start()
    {
        floor();

        players = new GameObject[2] {player1, player2};

        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i].GetComponent<PlayerMovement>().playerIndex = i;
            players[i].GetComponent<PlayerAttributes>().lifes = numberOfLifes;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void floor() 
    {
        GameObject tile;
        GameObject ground = GameObject.Find("Ground");

        for (float i = 0.32f; i < 7; i+=0.64f)
        {
            for (float j = 0.32f; j < 5; j+=0.64f)
            {
                tile = Instantiate(floorTile, transformBase);
                tile.transform.position += new Vector3(i, j, 0);
                tile.transform.SetParent(ground.GetComponent<Transform>());
            }
        }

        for (float i = -0.32f; i > -7; i-=0.64f)
        {
            for (float j = 0.32f; j < 5; j+=0.64f)
            {
                tile = Instantiate(floorTile, transformBase);
                tile.transform.position += new Vector3(i, j, 0);
                tile.transform.SetParent(ground.GetComponent<Transform>());
            }
        }

        for (float i = 0.32f; i < 7; i+=0.64f)
        {
            for (float j = -0.32f; j > -5; j-=0.64f)
            {
                tile = Instantiate(floorTile, transformBase);
                tile.transform.position += new Vector3(i, j, 0);
                tile.transform.SetParent(ground.GetComponent<Transform>());
            }
        }

        for (float i = -0.32f; i > -7; i-=0.64f)
        {
            for (float j = -0.32f; j > -5; j-=0.64f)
            {
                tile = Instantiate(floorTile, transformBase);
                tile.transform.position += new Vector3(i, j, 0);
                tile.transform.SetParent(ground.GetComponent<Transform>());
            }
        }

    }
}
