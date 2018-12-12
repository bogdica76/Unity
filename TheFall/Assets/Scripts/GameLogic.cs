using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    public GameObject noObstacleObject;
    public GameObject[] obstacles;
    public int pauseBetweenObstacle = 3;
    public float playerSpeed = 3.0f;
    private Rigidbody rb;

    private int pausesRemaining = 3;
    private float startConstructionPoint = -4;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        movePlayer();
	}
	
	// Update is called once per frame
	void Update () {
        //movePlayer();
	}

    private void movePlayer() {
        Vector3 movement = new Vector3(0.0f, -1.0f, 0.0f);

        rb.AddForce(movement * playerSpeed);
    }

    void OnTriggerExit(Collider other)
    {
        //distrugem nivelul din urma
        if (other.gameObject.tag == "LevelPiece")
        {
            Destroy(other.gameObject);
            Vector3 newComponentPosition = new Vector3(0.0f, startConstructionPoint, 0.0f);
            startConstructionPoint -= 1;

            if (pausesRemaining > 0)
            {
                pausesRemaining -= 1;
                Instantiate(noObstacleObject, newComponentPosition, Quaternion.identity);
            }
            else
            {
                pausesRemaining = pauseBetweenObstacle;
                int randomObstacle = Random.Range(0, obstacles.Length);
                Instantiate(obstacles[randomObstacle], newComponentPosition, Quaternion.identity);
            }
        }
    }
}
