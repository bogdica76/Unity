using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerShipSetup : MonoBehaviour {
    private int playerHealth = 1;
	// Use this for initialization
	void Start () {
        string shipModel = GameObject.Find("ShopManager").GetComponent<ShopManager>().selectedShipModel;
        Ship selectedShip = GameObject.Find("ShopManager").GetComponent<ShopManager>().GetSelectedShip();
        GameObject shipBody = Resources.Load<GameObject>(shipModel);
        var ship = Instantiate(shipBody);
        ship.transform.parent = GameObject.Find("Player").transform;

        //setam nav mesh agent in functie de nava
        GetComponent<NavMeshAgent>().speed += selectedShip.speed * 0.25f;
        GetComponent<NavMeshAgent>().acceleration += selectedShip.acceleration * 0.5f;
        playerHealth += selectedShip.shield;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DecreaseHP()
    {
        playerHealth = playerHealth - 1;

        if (playerHealth == 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().EndGame();
        }
    }
}
