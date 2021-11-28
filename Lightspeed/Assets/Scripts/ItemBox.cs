using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    private PlayerAttributes playerAttributes;
    public GameObject ItemGhost;
    public GameObject ItemBoost;
    public GameObject Item2Prefab;
    private int numPowerUps = 2; 

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
                obj = Instantiate(Item2Prefab);
                break;
        }
        return obj;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerAttributes = collision.gameObject.GetComponent<PlayerAttributes>();
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Get_Power_Up");
            GameObject power;
            power = createPowerUp();
            playerAttributes.addPowerUp(power);
            Destroy(this.gameObject);
        }
    }
}
