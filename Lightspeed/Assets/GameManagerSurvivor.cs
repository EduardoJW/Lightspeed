using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSurvivor : MonoBehaviour
{
    public GameObject floorTile;

    private Transform transformBase;


    // Start is called before the first frame update
    void Start()
    {
        floor();
        
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
