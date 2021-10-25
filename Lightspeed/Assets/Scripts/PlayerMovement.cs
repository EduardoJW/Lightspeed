using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public List<Vector3> playerPath = new List<Vector3>();
    public int horizontalDirection;
    public int verticalDirection;
    public bool isVertical;

    private float dt;
    private float velocityPlayer;
    private float[] velocities;
    private int velocityIndex;

    private float xPlayer;
    private float yPlayer;


    // Start is called before the first frame update
    void Start()
    {
        velocities = new float[1]{2.0f};
        velocityIndex = 0;

        horizontalDirection = 1;
        verticalDirection = 1;
        isVertical = false; 
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dt = Time.deltaTime;

        velocityPlayer = velocities[velocityIndex]*dt;

        if (isVertical)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                horizontalDirection = 1;
                isVertical = false;
            } 
            else if (Input.GetAxis("Horizontal") < 0)
            {
                horizontalDirection = -1;
                isVertical = false;
            }

            xPlayer = 0;
            yPlayer = velocityPlayer*verticalDirection;
                
        } 
        else
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                verticalDirection = 1;
                isVertical = true;
            } 
            else if (Input.GetAxis("Vertical") < 0)
            {
                verticalDirection = -1;
                isVertical = true;
            }            

            xPlayer = velocityPlayer*horizontalDirection;
            yPlayer = 0;
                
        }
            
        this.transform.position += new Vector3(xPlayer, yPlayer, 0);
        playerPath.Add(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
        
    }
}
