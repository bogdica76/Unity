using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FruitSelector : MonoBehaviour {

	public FruitSpawner fruitSpawner;
	private int score = 0;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		fruitSpawner = gameObject.GetComponent<FruitSpawner> ();
		//scoreText = .Find ("scoreText");
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Mouse is down");

			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) 
			{
				if (fruitSpawner.fruitSpawned == hitInfo.transform.gameObject.name) {
					Debug.Log ("OK");
					score = score + 1;
					scoreText.text = "Score: " + score.ToString ();
					fruitSpawner.fruitSpawned = "";
				} else {
					new WaitForSeconds (3);
					SceneManager.LoadScene("Village");
				}
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.tag == "Construction")
				{
					Debug.Log ("It's working!");
				} else {
					Debug.Log ("nopz");
				}
			} else {
				Debug.Log("No hit");
			}
			Debug.Log("Mouse is down");
		} 
	}
}
