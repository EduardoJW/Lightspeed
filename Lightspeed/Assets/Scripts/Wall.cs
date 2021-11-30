using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject bananaPrefab;
    public Sprite spriteWall;


    private int wallSize = 20;
    private float dt;

    private float[] velocities;
    private int[] velocityFactors;
    private List<Transform> walls;
    private int indexPath;
    private int velocityIndex;
    private bool[] isHorizontal;
    private SpriteRenderer spriteRenderer;


    private GameObject wall;
    private GameObject banana;
    public Renderer Sprite;

    PlayerMovement playerScript;
    PlayerAttributes playerAttributes;
    private List<Vector3> path;

    private List<Transform> segments = new List<Transform>();

    private void growWall()
    {
        //wall Object
        wall = Instantiate(this.wallPrefab);
        spriteRenderer = wall.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteWall;
        if (this.GetComponent<PlayerAttributes>().powerUpActive[0] == true)
        {
            wall.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
            wall.tag = "WallGhost";
        }
        else
        {
            wall.tag = "Wall";
        }


        //WallList
        walls.Add(wall.transform);
    }
    public void activateGhost()
    {

        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
            walls[i].tag = "WallGhost";
        }

    }
    public void deactivateGhost()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            walls[i].tag = "Wall";
        }
    }

    public void activateBanana()
    {
        banana = Instantiate(this.bananaPrefab);
        banana.transform.position = walls[1].transform.position;
        banana.transform.rotation = walls[1].transform.rotation;
        banana.transform.RotateAround(walls[1].position, Vector3.forward, 90);
    }
    public void deactivateBanana()
    {
        Destroy(banana);
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
                if (velocityIndex > 0)
                    walls[i].localScale = new Vector3(1, 1.4f, 1);
                else
                    walls[i].localScale = new Vector3(1, 1, 1);
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

        velocities = playerScript.velocities;
        velocityFactors = new int[4] { 200, 200, 150, 50 };
        isHorizontal = new bool[wallSize];

        walls = new List<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocityIndex = playerScript.velocityIndex;
        dt = Time.deltaTime;
        updateWall(dt);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "ItemBox" && collision.gameObject.tag != "WallGhost" && (this.GetComponent<PlayerAttributes>().powerUpActive[0] == false || collision.gameObject.tag == "OuterBarrier"))
        {
            if (collision.gameObject != walls[0].gameObject && collision.gameObject != walls[1].gameObject && collision.gameObject != walls[2].gameObject)
            {
                FindObjectOfType<AudioManager>().Play("Collision");
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
