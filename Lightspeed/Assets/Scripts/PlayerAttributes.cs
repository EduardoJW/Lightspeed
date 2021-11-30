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

	// 0: Ghost; 1: Boost; 2: Banana
	[HideInInspector] public bool[] powerUpActive = new bool[3] { false, false, false };
	private float[] powerUpTimer = new float[3] {0.0f, 0.0f, 0.0f };
	private float[] powerUpSpeed = new float[3] {20.0f, 20.0f, 20.0f};
	private float[] powerUpCooldown = new float[3] {100.0f, 100.0f, 28.0f};
	private int powerUpIndex = 0;
	
	public GameObject SpriteObject;
	public Renderer Sprite;

    public GameObject[] hearts;

	private string[] powerUpControlNames = new string[4] {"PowerUp_P1", "PowerUp_P2", "PowerUp_P3", "PowerUp_P4"};

    // Start is called before the first frame update
    void Start()
    {
		Sprite = SpriteObject.GetComponent<Renderer>();
        points = 0;   
    }
	
	void FixedUpdate(){
		
		float dt = Time.deltaTime;

		if (powerUpActive[powerUpIndex] == true) 
		{
			powerUpTimer[powerUpIndex] = powerUpTimer[powerUpIndex] + powerUpSpeed[powerUpIndex] * dt;

			if (powerUpTimer[powerUpIndex] > powerUpCooldown[powerUpIndex]) 
			{
				powerUpTimer[powerUpIndex] = 0;
				powerUpActive[powerUpIndex] = false;
				powerUp = null;

				switch (powerUpIndex)
				{
					case 0:
						deactivateGhost();
						break;
					case 1:
						deactivateBoost();
						break;
					case 2:
						deactivateBanana();
						break;
				}				
			}
		}

		verifyPowerUpActivation();
	}

	public void activateGhost()
	{
		var tempColor = Sprite.material.color;
		tempColor.a = 0.3f;
		Sprite.material.color = tempColor;

		this.GetComponent<Wall>().activateGhost();
	}

	public void deactivateGhost()
	{
		var tempColor = Sprite.material.color;
		tempColor.a = 1f;
		Sprite.material.color = tempColor;

		this.GetComponent<Wall>().deactivateGhost();
	}

	public void activateBoost()
	{
		this.GetComponent<PlayerMovement>().velocityIndex++; 
	}	

	public void deactivateBoost()
	{
		this.GetComponent<PlayerMovement>().velocityIndex = 0; 
	}

	public void activateBanana()
	{
		this.GetComponent<Wall>().activateBanana();
	}

	public void deactivateBanana()
	{
		this.GetComponent<Wall>().deactivateBanana();
	}

	public void addPoint ()
    {
        points++;
        Debug.Log("Player points: " + points);
    }

    public void addPowerUp (GameObject power)
    {
        powerUp = power;
        //Debug.Log("Player " + this.GetComponent<PlayerMovement>().playerIndex + " : got Power-up");
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

	bool isPowerUpActive ()
	{
		for (int i = 0; i < powerUpActive.Length; i++)
		{
			if (powerUpActive[i]) 
			{
				return true;
			}
		} 

		return false;
	}

	void verifyPowerUpActivation() 
	{
		if (!isPowerUpActive() && Input.GetAxis(powerUpControlNames[this.GetComponent<PlayerMovement>().playerIndex]) > 0 && powerUp != null)
        {
			if (powerUp.name == "ItemGhost(Clone)")
			{
                activateGhost();
				powerUpIndex = 0;
			}
			else if (powerUp.name == "ItemBoost(Clone)")
			{
				activateBoost();
				powerUpIndex = 1;
			}
			else if (powerUp.name == "ItemBanana(Clone)")
			{
				activateBanana();
				powerUpIndex = 2;
			}

			powerUpActive[powerUpIndex] = true;
			powerUpTimer[powerUpIndex] = 0.0f;
        }
	}
}
