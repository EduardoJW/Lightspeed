using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject wallPrefab;

    private int wallSize = 20;
    private float dt;

    private float[] velocities;
    private int[] velocityFactors;
    private List<Transform> walls;
    private int indexPath;
    private int velocityIndex;

    PlayerMovement playerScript;
    private List<Vector3> path;

    private List<Transform> segments = new List<Transform>();

    private void growWall()
    {
        GameObject wall = Instantiate(this.wallPrefab);
        walls.Add(wall.transform);
    }

    private void updateWall(float dt)
    {
        path = playerScript.playerPath;


        if (walls.Count > 0)
        {
            for (int i = 1; i < walls.Count; i++)
            {
                indexPath = (int)(path.Count - velocityFactors[velocityIndex] * dt * (1 + i));
                //Debug.Log("indexpath: " + indexPath);
                if (playerScript.isVertical)
                    walls[i].position = path[indexPath] - new Vector3(0, playerScript.verticalDirection * 0.1f * dt, 0);
                else
                    walls[i].position = path[indexPath] - new Vector3(playerScript.horizontalDirection * 0.1f * dt, 0, 0);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = this.GetComponent<PlayerMovement>();

        velocities = new float[4] { 2.0f, 4.0f, 8.0f, 16.0f };
        velocityFactors = new int[4] { 600, 300, 150, 50 };
        velocityIndex = 0;


        walls = new List<Transform>();

        for (int i = 0; i < wallSize; i++)
        {
            growWall();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dt = Time.deltaTime;
        updateWall(dt);
    }
}
