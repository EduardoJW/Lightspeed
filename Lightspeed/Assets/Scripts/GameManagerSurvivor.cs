using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerSurvivor : MonoBehaviour
{
    public GameObject[] players;
    public GameObject floorTile;
    public GameObject barrier;
    public GameObject itemBox;
    public GameObject endGamePanel;
    public Text result;
    public int numberOfLives = 3;
    public int numberOfPlayers = 2;
    public GameObject[] playersHearts;

    private Transform transformBase;

    private int lastAliveIndex;
    private List<int> deadsIndex = new List<int>();
    public float timeRemaining = 180;
    public bool timeIsRunning = false;
    public Text displayTime;
	
	private bool isEndGame = false;
	
	public GameObject FadeImage;
	private Renderer Sprite;
	
	private bool isFadingOut;
	
	public float fadeSpeed;
	public float fadeCooldown;
	private float fadeTimer;
	private int fadeTimerInt;

	void setFadeVar(){
		Sprite = FadeImage.GetComponent<Renderer>();
		Sprite.enabled = true;
        isFadingOut = false;
		fadeTimer = 0.0f;
		fadeTimerInt = 0;
		
		var tempColor = Sprite.material.color;
		tempColor.a = 0.0f;
		Sprite.material.color = tempColor;
	}
	
	void checkTransition(){
		if (isFadingOut == false ){
			if (Input.GetKeyDown("space")){
				/* isFadingOut = true; */
				Time.timeScale = 1.0f;
				SceneManager.LoadScene("PlayerSelect");	
			}		
		}
		else if (isFadingOut == true){
			
			fadeTimer = fadeTimer + fadeSpeed * Time.deltaTime;
			fadeTimerInt = Mathf.FloorToInt(fadeTimer);
			
			var fadeTempInt = fadeTimerInt;
			fadeTempInt = fadeTempInt / (70/2);
			fadeTempInt = fadeTempInt * (72/2);
			
			var fadeTempFloat = (float)fadeTempInt;
			
			var tempColor = Sprite.material.color;
			tempColor.a = 0.0f+(fadeTempFloat/255.0f);
			Sprite.material.color = tempColor;
			
			
			if (fadeTimer >= 255.0f){
				
				fadeTimer = 0.0f;
				SceneManager.LoadScene("PlayerSelect");				
				
			}
		
		}
		
		
		
	}
    // Start is called before the first frame update
    void Start()
    {
		setFadeVar();	
		
        floor();

        outerBarriers();

        itemBoxes();

        var numberOfPlayersTemp = PlayerPrefs.GetInt("playerNumber");
		
		if (numberOfPlayersTemp != null) {
			numberOfPlayers = numberOfPlayersTemp;
			Debug.Log(numberOfPlayers.ToString());
		}
		

        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i].GetComponent<PlayerMovement>().enable = true;
            players[i].GetComponent<PlayerAttributes>().lives = numberOfLives;
            playersHearts[i].SetActive(true);
        }

        timeIsRunning = true;

    }
	void Update() {
		if (isEndGame == true) {
			if (Input.GetKeyDown("space")) {
			
				checkTransition();		
			}	
		}		
	}
    // Update is called once per frame
    void FixedUpdate()
    {
        verifyLives();
        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);

                displayTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            }
            else
            {
                Debug.Log("Time is up, ending match");
                timeRemaining = 0;
                timeIsRunning = false;
                endGame();
                
            }
        }

        
    }

    void endGame() 
    {
        int position = 2;
        if (deadsIndex.Count == numberOfPlayers)
            position = 1;
        else
            result.text = "1. Player " + (lastAliveIndex + 1) + "\n";

        for (int i = deadsIndex.Count; i > 0; i--)
        {
            result.text += position + ". Player " + (deadsIndex[i-1] + 1) + "\n";
            position++;
        }
		isEndGame = true;
        endGamePanel.SetActive(true);
    }

    void floor() 
    {
        GameObject tile;
        GameObject ground = GameObject.Find("Ground");

        for (float i = 0.32f; i < 7; i+=0.64f)
        {
            for (float j = 0.32f; j < 5; j+=0.64f)
            {
                tile = Instantiate(floorTile, transformBase);
                tile.transform.position += new Vector3(i, j, 0);
                tile.transform.SetParent(ground.GetComponent<Transform>());
            }
        }

        for (float i = -0.32f; i > -7; i-=0.64f)
        {
            for (float j = 0.32f; j < 5; j+=0.64f)
            {
                tile = Instantiate(floorTile, transformBase);
                tile.transform.position += new Vector3(i, j, 0);
                tile.transform.SetParent(ground.GetComponent<Transform>());
            }
        }

        for (float i = 0.32f; i < 7; i+=0.64f)
        {
            for (float j = -0.32f; j > -5; j-=0.64f)
            {
                tile = Instantiate(floorTile, transformBase);
                tile.transform.position += new Vector3(i, j, 0);
                tile.transform.SetParent(ground.GetComponent<Transform>());
            }
        }

        for (float i = -0.32f; i > -7; i-=0.64f)
        {
            for (float j = -0.32f; j > -5; j-=0.64f)
            {
                tile = Instantiate(floorTile, transformBase);
                tile.transform.position += new Vector3(i, j, 0);
                tile.transform.SetParent(ground.GetComponent<Transform>());
            }
        }

    }

    void outerBarriers()
    {
        GameObject barriers;

        //barreira da direita
        barriers = Instantiate(barrier);
        barriers.transform.position = new Vector3(7f, 0, 0);
        barriers.transform.RotateAround(barriers.transform.position, Vector3.forward, 90);
        barriers.transform.localScale = new Vector3(1, 18, 1);

        //barreira da esquerda
        barriers = Instantiate(barrier);
        barriers.transform.position = new Vector3(-7f, 0, 0);
        barriers.transform.RotateAround(barriers.transform.position, Vector3.forward, 90);
        barriers.transform.localScale = new Vector3(1, 18, 1);

        //barreira de cima
        barriers = Instantiate(barrier);
        barriers.transform.position = new Vector3(0, 3.85f, 0);
        barriers.transform.localScale = new Vector3(1, 22, 1);

        //barreira de baixo
        barriers = Instantiate(barrier);
        barriers.transform.position = new Vector3(0, -4.97f, 0);
        barriers.transform.localScale = new Vector3(1, 22, 1);
    }

    void itemBoxes()
    {
        GameObject itemBoxes;

        itemBoxes = Instantiate(itemBox);
        itemBoxes.transform.position = new Vector3(0, -1f, 0);
    }

    void verifyLives () 
    {
        int alive = 0;

        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (players[i].GetComponent<PlayerAttributes>().lives > 0)
            {
                alive++;
                lastAliveIndex = i;
            }
            else 
            {
                if (!deadsIndex.Contains(i))
                {
                    deadsIndex.Add(i);
                }
            }
        }

        if (alive <= 1)
        {
            Time.timeScale = 0;
            timeIsRunning = false;
            endGame();
        }

    }
	
	
}
