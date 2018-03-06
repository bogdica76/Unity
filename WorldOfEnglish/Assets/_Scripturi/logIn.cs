using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class logIn : MonoBehaviour {

    public InputField campUser;
    public InputField campParola;
    public void logareSite()
    {
        // string user = GameObject.Find("userField").transform.Find("Error").GetComponent<UnityEngine.UI.Text>().text;
        // string pass = GameObject.Find("passField").transform.Find("Error").GetComponent<UnityEngine.UI.Text>().text;
        string user = campUser.text;
        string pass = campParola.text;


        Debug.Log(user + " - " + pass);
        StartCoroutine(login());
        
    }

    IEnumerator login()
    {


		WWW apelPHP = new WWW("https://thoe.ro/scrambled/logare.php?user="+ campUser.text + "&pass=" + campParola.text);
        yield return apelPHP; // Wait until the download is done

        Debug.Log(apelPHP.error);
        if (apelPHP.error != "")
        {
            Debug.Log("error");
        }
        else
        {
            if (apelPHP.text != "-1")
            {
                SceneManager.LoadScene("levelOne");
            }
            else
            {
                //not logged in
            }
        }
    }
}