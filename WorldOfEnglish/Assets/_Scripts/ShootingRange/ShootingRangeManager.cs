using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingRangeManager : MonoBehaviour {

	public Image myImageComponent;
	public Sprite correctAnswerImg;
	public Sprite wrongAnswerImg;
    public Text textProp;
    public Text raspA;
    public Text raspB;
    public Text raspC;
    public Text raspD;
    public GameObject playerShootingRange;
	public GameObject player;    
    private string raspunsCorect = "";

	void Update () {
		if (Input.GetKeyDown (KeyCode.DownArrow) && playerShootingRange.activeSelf) {
			playerShootingRange.SetActive (false);
			player.transform.Translate(0, 0, -2);
			player.SetActive (true);
		}
	}

    IEnumerator incarcareProp() {
		Debug.Log ("incarc prop");
		yield return new WaitForSeconds(1);
		myImageComponent.GetComponent<Image> ().sprite = null;
        WWW apelPHP = new WWW("https://thoe.ro/scrambled/loadSentUnity.php?categ=3");
        yield return apelPHP; // Wait until the download is done
            if (apelPHP.text != "")
            {
                // id # categorie # propozitie # raspA # RaspB # raspC # raspD # raspCorect
                string vRaspuns = apelPHP.text;
                string[] vRaspunsSplit;
                vRaspunsSplit = vRaspuns.Split('#');

                textProp.text = vRaspunsSplit[2];
                raspA.text = vRaspunsSplit[3];
                raspB.text = vRaspunsSplit[4];
                raspC.text = vRaspunsSplit[5];
                raspD.text = vRaspunsSplit[6];

                raspunsCorect = vRaspunsSplit[7];
				gameObject.transform.GetComponentInChildren <Shooting> ().canFire = true;
            }
    }

	IEnumerator TimerFeedback(bool isCorrect){
		yield return new WaitForSeconds(3);
		if (isCorrect == true) {
			myImageComponent.GetComponent<Image> ().sprite = correctAnswerImg;

		} else {
			myImageComponent.GetComponent<Image> ().sprite = wrongAnswerImg;			 
		}
		StartCoroutine(incarcareProp());
	}

	IEnumerator adaugaPuncte() {
		WWW apelPHP = new WWW("https://thoe.ro/scrambled/addPointsInUnity.php?puncte=");
		yield return apelPHP;
	}

    public void checkRaspuns(string unRaspuns)
    {
		gameObject.transform.GetComponentInChildren <Shooting> ().canFire = false;
		bool raspunsulEsteCorect = false;
		//int puncte = 0;
        if (raspunsCorect == unRaspuns)
        {
			raspunsulEsteCorect = true;
			StartCoroutine(adaugaPuncte());
			//puncte = 100; 
        }
        else
        {	
			raspunsulEsteCorect = false;
        }
		textProp.text = textProp.text.Replace (".....", "<color=green><i>" + unRaspuns + "</i></color>");

		StartCoroutine(TimerFeedback(raspunsulEsteCorect));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Confined;
			if (GameObject.Find ("GameManager").GetComponent<BigGameManager> ().checkForAmmo ("bullets")) {
				other.gameObject.SetActive (false);
				playerShootingRange.SetActive (true);
				StartCoroutine(incarcareProp());
			}
			else{
				GameObject.Find ("GameManager").GetComponent<BigGameManager> ().showInfo ("You have no bullets!");
			}
        }		
    }

	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			GameObject.Find ("GameManager").GetComponent<BigGameManager> ().hideInfo ();
		}
	}
}