using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    private int selectedShipIndex;    
    private readonly string[] ShipsModels = { "Saucer", "Imperator" };
    private Ship[] storeShips;

    //public variables
    public Text speedLevel;
    public Text accelerationLevel;
    public Text shieldLevel;
    public Text shipName;
    // Use this for initialization
    void Awake() {
        DontDestroyOnLoad(this);
        storeShips = new Ship[ShipsModels.Length];
        selectedShipIndex = PlayerPrefs.GetInt(Constants.SelectedShipIndex);
        Debug.Log(selectedShipIndex);
        initializeShop();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void initializeShop() {
        for (int i = 0; i < ShipsModels.Length; i++) {
            storeShips[i] = new Ship(ShipsModels[i]);
        }
        SetupShipInfo();
    }

    public void GoRight() {
        if (selectedShipIndex == (ShipsModels.Length - 1) ) return;

        selectedShipIndex += 1;
        SetupShipInfo();
    }

    public void GoLeft() {
        if (selectedShipIndex == 0) return;

        selectedShipIndex -= 1;
        SetupShipInfo();
    }

    private void SetupShipInfo() {
        Ship selectedShip = storeShips[selectedShipIndex];
        shipName.text = selectedShip.model;
        speedLevel.text = selectedShip.speed.ToString();
        accelerationLevel.text = selectedShip.acceleration.ToString();
        shieldLevel.text = selectedShip.shield.ToString();
    }

    public void UpgradeSpeed() {
        Ship selectedShip = storeShips[selectedShipIndex];
        selectedShip.UpSpeed();
    }
}
