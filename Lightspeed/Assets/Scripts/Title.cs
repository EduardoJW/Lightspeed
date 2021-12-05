using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
	
	public GameObject FadeImage;
	private Renderer Sprite;
	
	private bool isFadingOut;
	
	public float fadeSpeed;
	public float fadeCooldown;
	private float fadeTimer;
	private int fadeTimerInt;
    // Start is called before the first frame update
    void Start()
    {
		Sprite = FadeImage.GetComponent<Renderer>();
		Sprite.enabled = true;
        isFadingOut = false;
		fadeTimer = 0.0f;
		fadeTimerInt = 0;
		
		var tempColor = Sprite.material.color;
		tempColor.a = 0.0f;
		Sprite.material.color = tempColor;
		
		
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingOut == false ){
			if (Input.GetKeyDown("space")){
				isFadingOut = true;
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
}
