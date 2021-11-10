using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [HideInInspector] public int lifes;
    [HideInInspector] public int points;

    [HideInInspector] public GameObject powerUp;


    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        
    }

    public void addPoint ()
    {
        points++;
        Debug.Log("Player points: " + points);
    }

    public void addPowerUp (GameObject power)
    {
        powerUp = power;
        Debug.Log("Player " + this.GetComponent<PlayerMovement>().playerIndex + " : got Power-up");
    }

    public void removeLife ()
    {
        lifes--;

        Debug.Log("Player " + this.GetComponent<PlayerMovement>().playerIndex + " : lifes: " + lifes);

        if (lifes == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
