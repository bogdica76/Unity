using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
	public GameObject aMeteorite;
	public GameObject GameOverText;
    public GameObject countdownText;
    public GameObject WinningText;
	public float SpawnRate = 0.5f;
	private float lastSpawned = 0.0f;
    private float gameStart = 0.0f;
	private int playerHealth = 1;
    private int diamondsEarned = 0;
    private bool isGameOver = false;

	// Use this for initialization
	void Start ()
	{
		InitGameScene ();
        gameStart = Time.time;
        diamondsEarned = 0;
        isGameOver = false;	
	}
	
	// Update is called once per frame
	void Update ()
	{
        float timeLeft = 60 - (Time.time - gameStart);
        countdownText.GetComponent<Text>().text = timeLeft.ToString("F2");
		if (Time.time > SpawnRate + lastSpawned) {
			lastSpawned = Time.time;
			SpawnMeteorites ();
		}
        if (Time.time > gameStart + 60) {
            if (gameObject.GetComponent<BlackHoleManager>().blackHoleIsSpawned)
                return;

            gameObject.GetComponent<BlackHoleManager>().SpawnBlackHole();
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
			EndGame ();
		}
	}

	private void EndGame(){
        if (!isGameOver)
        {
            isGameOver = true;
            GameOverText.SetActive(true);
            float seconds = Time.time - gameStart;
            diamondsEarned = Mathf.FloorToInt(seconds);
            StartCoroutine(GoToMenu());
        }
    }

    public void FinishLevel() {
        //display winning message
        if (!isGameOver)
        {
            isGameOver = true;
            float seconds = Time.time - gameStart;
            diamondsEarned = Mathf.FloorToInt(seconds);
            WinningText.SetActive(true);
            WinningText.GetComponent<Text>().text = "Congratulations! \n You have escaped the asteroid field earning <color=cyan>" + diamondsEarned + "</color> diamons.";
            StartCoroutine(GoToMenu());
        }
    }

    IEnumerator GoToMenu() {
        GameObject.Find("PlayerManager").GetComponent<PlayerData>().RewardByTime(diamondsEarned);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }

	public void InitGameScene (){
		
	}

	public void StartGame(){
		SceneManager.LoadScene ("TestLevel");
	}
}
