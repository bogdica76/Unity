using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class impactTinta : MonoBehaviour {
    public Text textPancarta;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider ObjColiziune)
    {
		if (ObjColiziune.gameObject.CompareTag("bullet"))
        {
            ObjColiziune.gameObject.SetActive(false);
			gameObject.transform.parent.gameObject.transform.parent.GetComponent<ShootingRangeManager> ().checkRaspuns (textPancarta.text);
        }
    }
}
