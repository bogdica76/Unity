using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    private int selectedShipIndex;    
    private readonly string[] ShipsModels = { "Saucer", "Imperator" };
    private Ship[] storeShips;
    public string selectedShipModel;

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

    public Ship GetSelectedShip() {
        return storeShips[selectedShipIndex];
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
        PlayerPrefs.SetInt(Constants.SelectedShipIndex, selectedShipIndex);
        GameObject.Find("ShipPreview").GetComponent<ShipPreview>().PreviewShip(selectedShipIndex);
        Ship selectedShip = storeShips[selectedShipIndex];
        selectedShipModel = selectedShip.model;
        shipName.text = selectedShip.model;
        speedLevel.text = selectedShip.speed.ToString();
        accelerationLevel.text = selectedShip.acceleration.ToString();
        shieldLevel.text = selectedShip.shield.ToString();
    }

    public void UpgradeSpeed() {
        PlayerData playerData = GameObject.Find("PlayerManager").GetComponent<PlayerData>();
        if (playerData.CanBuyUpgrade())
        {
            Ship selectedShip = storeShips[selectedShipIndex];
            selectedShip.UpSpeed();
            playerData.BuyUpgrade();
            SetupShipInfo();
        }
        else {
            GameObject.Find("InfoManager").GetComponent<InfoManager>().ShowInfo("Info", "You need 250 diamons for this upgrade.");
        }
    }

    public void UpgradeAcceleration()
    {
        PlayerData playerData = GameObject.Find("PlayerManager").GetComponent<PlayerData>();
        if (playerData.CanBuyUpgrade())
        {
            Ship selectedShip = storeShips[selectedShipIndex];
            selectedShip.UpAcceleration();
            playerData.BuyUpgrade();
            SetupShipInfo();
        }
        else
        {
            GameObject.Find("InfoManager").GetComponent<InfoManager>().ShowInfo("Info", "You need 250 diamons for this upgrade.");
        }
    }

    public void UpgradeShield()
    {
        PlayerData playerData = GameObject.Find("PlayerManager").GetComponent<PlayerData>();
        if (playerData.CanBuyUpgrade())
        {
            Ship selectedShip = storeShips[selectedShipIndex];
            selectedShip.UpShield();
            playerData.BuyUpgrade();
            SetupShipInfo();
        }
        else
        {
            GameObject.Find("InfoManager").GetComponent<InfoManager>().ShowInfo("Info", "You need 250 diamons for this upgrade.");
        }
    }
}
