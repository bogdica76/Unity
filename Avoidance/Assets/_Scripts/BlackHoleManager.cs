using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleManager : MonoBehaviour {
	public GameObject BlackHole;
	public GameObject BlackHoleMiniMap;
    public bool blackHoleIsSpawned = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnBlackHole(){
		float x, y;
		x = Random.Range (-15, 15);
		y = Random.Range (-15, 15);

		var blackHoleSpawned = (GameObject)Instantiate (
			BlackHole,
			new Vector3(x, 0.3f, y),
			BlackHole.transform.rotation);

		var blackHoleMiniMapSpawned = (GameObject)Instantiate (
			BlackHoleMiniMap,
			new Vector3(x + 100.0f, 0.3f, y),
			BlackHoleMiniMap.transform.rotation);
        blackHoleIsSpawned = true;
	}
}
