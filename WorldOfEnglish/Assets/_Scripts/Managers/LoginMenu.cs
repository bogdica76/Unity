using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviour {
	public GameObject panelLogin;
	public GameObject panelregister;
	public GameObject panelMessage;
	public Text titluMesaj;
	public Text mesaj;

	// Use this for initialization
	void Start () {
		panelLogin.SetActive (true);
		panelregister.SetActive (false);
		panelMessage.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void goToLogin(){
		panelLogin.SetActive (true);
		panelregister.SetActive (false);
	}

	public void goToRegister(){
		panelLogin.SetActive (false);
		panelregister.SetActive (true);
	}

	public void doLogin(){
		StartCoroutine (Login ());
	}

	IEnumerator Login() {
		string vEmail = GameObject.Find ("LoginUserInput").GetComponent<InputField>().text;
		string vPassword = GameObject.Find("LoginPasswordInput").GetComponent<InputField>().text;

		WWW apelPHP = new WWW("http://unitygame.crazytech.ro/logic/login.php?email="+vEmail+"&pass="+vPassword);
		yield return apelPHP; // Wait until the download is done


		if (apelPHP.error != null)
		{
			showServerMessage ("Atentie", apelPHP.text);
			Debug.Log(apelPHP.error);
		}
		else
		{
			if (apelPHP.text == "1")
			{
				string vRaspuns = apelPHP.text;

				SceneManager.LoadScene ("EnglishIsland");
			}
			else
			{
				showServerMessage ("Atentie", apelPHP.text);
			}
		}

	}

	public void doRegister(){
		StartCoroutine (Register ());
	}

	IEnumerator Register() {
		string vEmail = GameObject.Find ("RegisterUserInput").GetComponent<InputField>().text;
		string vPassword = GameObject.Find("RegisterPasswordInput").GetComponent<InputField>().text;
		string vPassword2 = GameObject.Find("RegisterPassword2Input").GetComponent<InputField>().text;

		WWW apelPHP = new WWW("http://unitygame.crazytech.ro/logic/register.php?email="+vEmail+"&pass="+vPassword);
		yield return apelPHP; // Wait until the download is done


		if (apelPHP.error != "")
		{
			showServerMessage ("Atentie", apelPHP.text);
			Debug.Log(apelPHP.error);
		}
		else
		{
			if (apelPHP.text == "1")
			{
				string vRaspuns = apelPHP.text;

				//SceneManager.LoadScene ("EnglishIsland");
			}
			else
			{
				showServerMessage ("Atentie", apelPHP.text);
			}
		}

	}

	public void showServerMessage(string aTitle, string aMsg){
		panelMessage.SetActive (true);
		titluMesaj.text = aTitle;
		mesaj.text = aMsg;
	}

	public void closeMessageWindow(){
		panelMessage.SetActive (false);
	}
}
