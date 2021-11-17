using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    private PlayerAttributes playerAttributes;
    public GameObject ItemGhost;
    public GameObject Item1Prefab;
    public GameObject Item2Prefab;
    int numPowerUps = 0; //3

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
                obj = Instantiate(Item1Prefab);
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
            GameObject power;
            power = createPowerUp();
            playerAttributes.addPowerUp(power);
            Destroy(this.gameObject);
        }
    }
}
