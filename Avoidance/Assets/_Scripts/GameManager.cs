using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject aMeteorite;
	public GameObject GameOverText;
	public float SpawnRate = 0.5f;
	private float lastSpawned = 0.0f;
	private int playerHealth = 1;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > SpawnRate + lastSpawned) {
			lastSpawned = Time.time;
			SpawnMeteorites ();
		}
	}

	public void SpawnMeteorites ()
	{		
		int vDirectie = Mathf.RoundToInt (Random.Range (1, 5));
		switch (vDirectie) {
		case 1:
			SpawnLeft ();
			break;
		case 2:
			SpawnTop ();
			break;
		case 3:
			SpawnRight ();
			break;
		case 4:
			SpawnBottom ();
			break;
		default:
			break;
		}		
	}

	public void SpawnLeft ()
	{
		float vHeight = Random.Range (-15, 15);
		var meteoriteSpawned = (GameObject)Instantiate (
			aMeteorite,
			new Vector3(vHeight, 0.3f, -15.0f),
			aMeteorite.transform.rotation);
		meteoriteSpawned.GetComponent<Rigidbody> ().velocity = aMeteorite.transform.forward * 3;
	}

	public void SpawnTop ()
	{
		float vWidth = Random.Range (-15, 15);
		var meteoriteSpawned = (GameObject)Instantiate (
			aMeteorite,
			new Vector3(-15.0f, 0.3f, vWidth),
			aMeteorite.transform.rotation);
		meteoriteSpawned.GetComponent<Rigidbody> ().velocity = aMeteorite.transform.right * 3;		
	}

	public void SpawnRight ()
	{
		float vHeight = Random.Range (-15, 15);
		var meteoriteSpawned = (GameObject)Instantiate (
			aMeteorite,
			new Vector3(vHeight, 0.3f, 15.0f),
			aMeteorite.transform.rotation);
		meteoriteSpawned.GetComponent<Rigidbody> ().velocity = aMeteorite.transform.forward * -3;
	}

	public void SpawnBottom ()
	{
		float vWidth = Random.Range (-15, 15);
		var meteoriteSpawned = (GameObject)Instantiate (
			aMeteorite,
			new Vector3(15.0f, 0.3f, vWidth),
			aMeteorite.transform.rotation);
		meteoriteSpawned.GetComponent<Rigidbody> ().velocity = aMeteorite.transform.right * -3;
	}

	public void DecreaseHP(){
		playerHealth = playerHealth - 1;

		if (playerHealth == 0) {
			DisplayGameOver ();
		}
	}

	private void DisplayGameOver(){
		GameOverText.SetActive (true);
	}
}
