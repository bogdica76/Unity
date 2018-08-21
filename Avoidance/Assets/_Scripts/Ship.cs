using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ship {
    public string model;
    public int speed;
    public int acceleration;
    public int shield;

    public Ship(string aModel) {
        this.model = aModel;
        this.speed = PlayerPrefs.GetInt(aModel + Constants.SpeedKey);
        this.acceleration = PlayerPrefs.GetInt(aModel + Constants.AccelerationKey);
        this.shield = PlayerPrefs.GetInt(aModel + Constants.ShieldKey);
    }

    public void SaveShipData() {
        PlayerPrefs.SetInt(this.model + Constants.SpeedKey, this.speed);
        PlayerPrefs.SetInt(this.model + Constants.AccelerationKey, this.acceleration);
        PlayerPrefs.GetInt(this.model + Constants.ShieldKey, this.shield);
    }

    public void UpSpeed() {
        this.speed += 1;
        SaveShipData();
    }
}
