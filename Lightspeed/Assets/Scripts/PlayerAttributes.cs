using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public int lives;
    [HideInInspector] public int points;

    public GameObject powerUp = null;
	
	public bool isGhost = false;
	public float ghostTimer = 0.0f;
	public float ghostSpeed;
	public float ghostCooldown;
	public bool hasItem = true;
	
	public GameObject SpriteObject;
	public Renderer Sprite;

    public GameObject[] hearts;

    // Start is called before the first frame update
    void Start()
    {
		Sprite = SpriteObject.GetComponent<Renderer>();
        points = 0;
		
		ghostSpeed = 20.0f;
		ghostCooldown = 100.0f;
		
		//activateGhost();
        
    }
	
	void FixedUpdate(){
		
		float dt = Time.deltaTime;
		if (isGhost == true) {
			ghostTimer = ghostTimer + ghostSpeed * dt;
			if (ghostTimer > ghostCooldown) {
				deactivateGhost();
			}
		}
		
	}
	public void deactivateGhost(){
		ghostTimer = 0;
		isGhost = false;
		var tempColor = Sprite.material.color;
		tempColor.a = 1f;
		Sprite.material.color = tempColor;
		this.GetComponent<Wall>().deactivateGhost();
		powerUp = null;
	}
	public void activateGhost(){
		isGhost = true;
		ghostTimer = 0;

		var tempColor = Sprite.material.color;
		tempColor.a = 0.3f;
		Sprite.material.color = tempColor;
		
		this.GetComponent<Wall>().activateGhost();
	
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
        lives--;

        Debug.Log("Player " + (this.GetComponent<PlayerMovement>().playerIndex + 1) + " : lives: " + lives);

        Destroy(hearts[lives].gameObject);

        if (lives == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
