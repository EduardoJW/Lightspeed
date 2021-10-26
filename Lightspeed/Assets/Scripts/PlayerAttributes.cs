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
        lifes = 3;
        points = 0;
        
    }

    void addPoint ()
    {
        points++;
    }

    void addPowerUp (GameObject power)
    {
        powerUp = power;
    }

    void removeLife ()
    {
        lifes--;

        if (lifes == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
