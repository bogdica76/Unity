using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {

	private ArrayList Fruits;
	private ArrayList FruitsSpawned;

	public string fruitSpawned = "";
	public GameObject fruit;
	public AudioClip clip;
	private bool soundPlayed = false;

	// Use this for initialization
	void Start () {
//		Fruits.Add("apple", "pear", "banana");
	}
	
	// Update is called once per frame
	void Update () {
		if (fruitSpawned == "") {
			soundPlayed = false;
			SpawnFruit ();
		} else {
			//play sound and wait for pick
			if (!soundPlayed) {
				fruit = GameObject.Find (fruitSpawned);
				fruit.GetComponent<AudioSource> ().Play ();
				soundPlayed = true;
			}
		}
	}

	void SpawnFruit(){
		var number = Random.Range (1, 3);
		//fruitSpawned = "";
		switch (number) {
		case 1:
			fruitSpawned = "apple";
			break;
		case 2:
			fruitSpawned = "banana";
			break;
		case 3:
			fruitSpawned = "pear";
			break;
		}
	}
}
