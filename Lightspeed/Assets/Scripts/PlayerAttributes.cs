using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public int lifes;
    [HideInInspector] public int points;

    [HideInInspector] public GameObject powerUp;

    public GameObject[] hearts;

    void Update()
    {
        if(lifes < 1)
        {
            Destroy(hearts[0].gameObject);
        } else if (lifes < 2)
        {
            Destroy(hearts[1].gameObject);
        } else if (lifes < 3)
        {
            Destroy(hearts[2].gameObject);
        }
    }

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

        Debug.Log("Player " + (this.GetComponent<PlayerMovement>().playerIndex + 1) + " : lifes: " + lifes);

        if (lifes == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
