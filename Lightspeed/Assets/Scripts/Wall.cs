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
    private bool[] isHorizontal;
	

	private GameObject wall;
	public Renderer Sprite;

    PlayerMovement playerScript;
    PlayerAttributes playerAttributes;
    private List<Vector3> path;

    private List<Transform> segments = new List<Transform>();

    private void growWall()
    {
		//wall Object
        wall = Instantiate(this.wallPrefab);
		if (this.GetComponent<PlayerAttributes>().isGhost == true) {
			
			wall.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
			wall.tag = "WallGhost";
			
		} 
		else {
			wall.tag = "Wall";
		}
	
		
		//WallList
        walls.Add(wall.transform);
    }
	public void activateGhost(){
		
		for (int i = 0; i < walls.Count; i++)
        {
            walls[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
            walls[i].tag = "WallGhost";
        }
		
	}
	public void deactivateGhost(){
		for (int i = 0; i < walls.Count; i++)
        {
            walls[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            walls[i].tag = "Wall";
        }
	}
    private void updateWall(float dt)
    {
        path = playerScript.playerPath;

        if (walls.Count < wallSize && path.Count > velocityFactors[velocityIndex] * dt * (walls.Count + 1))
            growWall();

        if (walls.Count > 0)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                indexPath = (int)(path.Count - velocityFactors[velocityIndex] * dt * (1 + i));
                if ((isHorizontal[i] && walls[i].position.x == path[indexPath].x) || (!isHorizontal[i] && walls[i].position.y == path[indexPath].y))
                    walls[i].RotateAround(walls[i].position, Vector3.forward, 90);
                if (walls[i].position.x == path[indexPath].x)
                    isHorizontal[i] = false;
                else
                    isHorizontal[i] = true;
                walls[i].position = path[indexPath];
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = this.GetComponent<PlayerMovement>();
        playerAttributes = this.GetComponent<PlayerAttributes>();

        velocities = new float[4] { 2.0f, 4.0f, 8.0f, 16.0f };
        velocityFactors = new int[4] { 200, 300, 150, 50 };
        velocityIndex = 0;
        isHorizontal = new bool[wallSize];

        walls = new List<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dt = Time.deltaTime;
        updateWall(dt);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "ItemBox" && collision.gameObject.tag != "WallGhost" && this.GetComponent<PlayerAttributes>().isGhost == false)
        {
            if (collision.gameObject != walls[0].gameObject && collision.gameObject != walls[1].gameObject && collision.gameObject != walls[2].gameObject)
            {
                playerAttributes.removeLife();
                for (int i = walls.Count - 1; i >= 0; i--)
                {
                    Destroy(walls[i].gameObject);
                    walls.Remove(walls[i]);
                }
                playerScript.resetPosition();
            }
        }
    }
}
