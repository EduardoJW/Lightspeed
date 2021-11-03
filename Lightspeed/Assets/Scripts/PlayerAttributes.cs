using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [HideInInspector] public int lifes;
    [HideInInspector] public int points;

    [HideInInspector] public GameObject powerUp;

    int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        lifes = 3;
        points = 0;
        
    }

    // Debug
    void FixedUpdate() {

        if (count > 100)
        {
            addPoint();
            count = 0;
        } 
        else
        {
            count++;
        }       
    }

    void addPoint ()
    {
        points++;
        Debug.Log("Player points: " + points);
    }

    void addPowerUp (GameObject power)
    {
        powerUp = power;
    }

    public void removeLife ()
    {
        lifes--;

        Debug.Log("Player lifes: " + lifes);

        if (lifes == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
