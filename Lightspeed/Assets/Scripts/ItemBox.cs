using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    private PlayerAttributes playerAttributes;
    public GameObject ItemGhost;
    public GameObject ItemBoost;
    public GameObject ItemBanana;
    
    private Renderer Sprite;

    private int numPowerUps = 3;
    private bool boxEnabled = true;
    private float disabledTimer = 0;

    GameObject createPowerUp() //deve ser aumentada manualmente conforme a lista de powerups cresce
    {
        GameObject obj = null;
        int itemIndex = (int) (numPowerUps * Random.value);
        Debug.Log("Chosen item: " + itemIndex);
        switch (itemIndex)
        {
            case 0:
                obj = Instantiate(ItemGhost);
                break;
            case 1:
                obj = Instantiate(ItemBoost);
                break;
            case 2:
                obj = Instantiate(ItemBanana);
                break;
        }
        return obj;
    }

    void Start()
    {
        Sprite = this.GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        float dt = Time.deltaTime;
        if (boxEnabled == false)
        {
            disabledTimer += dt;
            if(disabledTimer >= 2.0f)
            {
                EnableBox();
            }
        }
    }

    void DisableBox()
    {
        var tempColor = Sprite.material.color;
        tempColor.a = 0.3f;
        Sprite.material.color = tempColor;

        disabledTimer = 0;
        boxEnabled = false;
    }

    void EnableBox()
    {
        var tempColor = Sprite.material.color;
        tempColor.a = 1f;
        Sprite.material.color = tempColor;

        boxEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerAttributes = collision.gameObject.GetComponent<PlayerAttributes>();
        if (collision.gameObject.tag == "Player" && playerAttributes.powerUp == null && boxEnabled)
        {
            FindObjectOfType<AudioManager>().Play("Get_Power_Up");
            GameObject power;
            power = createPowerUp();
            playerAttributes.addPowerUp(power);
            DisableBox();
        }
    }
}
