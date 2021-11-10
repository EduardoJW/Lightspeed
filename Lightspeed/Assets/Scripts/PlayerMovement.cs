using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public List<Vector3> playerPath;
    [HideInInspector] public int horizontalDirection;
    [HideInInspector] public int verticalDirection;
    [HideInInspector] public bool isVertical;
    public int playerIndex;
 
    private string[] horizontalControlNames = new string[4] {"Horizontal_P1", "Horizontal_P2", "Horizontal_P3", "Horizontal_P4"};
    private string[] verticalControlNames = new string[4] {"Vertical_P1", "Vertical_P2", "Vertical_P3", "Vertical_P4"};
 
    private float dt;
    private float velocityPlayer;
    private float[] velocities;
    private int velocityIndex;
 
    private float xPlayer;
    private float yPlayer;
 
    private bool oldIsVertical = false;
    private int oldHorizontalDirection = 1;
    private int oldVerticalDirection = 1;
 
    private Vector3[] startingPositions = new Vector3[4] {new Vector3(-3, 3, 0), new Vector3(-3, -3, 0), new Vector3(3, 3, 0), new Vector3(3, -3, 0)};
    private Vector3 startingPosition;
 
 
    // Start is called before the first frame update
    void Start()
    {
        velocities = new float[1] { 2.0f };
        velocityIndex = 0;
 
        horizontalDirection = 1;
        verticalDirection = 1;
        isVertical = false;
 
        playerPath = new List<Vector3>();
 
        startingPosition = startingPositions[playerIndex];
        this.transform.position = startingPosition;
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        dt = Time.deltaTime;
 
        velocityPlayer = velocities[velocityIndex] * dt;
 
        if (isVertical)
        {
            if (Input.GetAxis(horizontalControlNames[playerIndex]) > 0)
            {
                horizontalDirection = 1;
                isVertical = false;
            }
            else if (Input.GetAxis(horizontalControlNames[playerIndex]) < 0)
            {
                horizontalDirection = -1;
                isVertical = false;
            }
 
            xPlayer = 0;
            yPlayer = velocityPlayer * verticalDirection;
        }
        else
        {
            if (Input.GetAxis(verticalControlNames[playerIndex]) > 0)
            {
                verticalDirection = 1;
                isVertical = true;
            }
            else if (Input.GetAxis(verticalControlNames[playerIndex]) < 0)
            {
                verticalDirection = -1;
                isVertical = true;
            }
 
            xPlayer = velocityPlayer * horizontalDirection;
            yPlayer = 0;
        }
 
        changeDirection();
 
        this.transform.position += new Vector3(xPlayer, yPlayer, 0);
        playerPath.Add(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
 
    }
 
    void changeDirection()
    {
        if (isVertical != oldIsVertical)
        {
            rotationSprite();
        }
 
        oldIsVertical = isVertical;
        oldHorizontalDirection = horizontalDirection;
        oldVerticalDirection = verticalDirection;
    }
 
    void rotationSprite()
    {
        float z = 0;
 
        if (oldIsVertical)
        {
            if (oldVerticalDirection == 1)
            {
                if (horizontalDirection == -1)
                {
                    z = 90;
                }
                else if (horizontalDirection == 1)
                {
                    z = 270;
                }
            }
            else if (oldVerticalDirection == -1)
            {
                if (horizontalDirection == -1)
                {
                    z = 270;
                }
                else if (horizontalDirection == 1)
                {
                    z = 90;
                }
            }
        }
        else
        {
            if (oldHorizontalDirection == -1)
            {
                if (verticalDirection == -1)
                {
                    z = 90;
                }
                else if (verticalDirection == 1)
                {
                    z = 270;
                }
            }
            else if (oldHorizontalDirection == 1)
            {
                if (verticalDirection == -1)
                {
                    z = 270;
                }
                else if (verticalDirection == 1)
                {
                    z = 90;
                }
            }
        }
 
        this.transform.Rotate(0, 0, z);
    }
 
    public void resetPosition()
    {
        Start();
    }
}
 

