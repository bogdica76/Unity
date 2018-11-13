using System.Linq;
using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    private Player localPlayer { get; set; }
    private float Step = 3f;

    private Vector3 oldPosition { get; set; }
    private const float SendRate = 0.05f;
    private float lastSendTime = 0;
	// Use this for initialization
	void Start () {
	    PhotonServer.Instance.WorldEnterOperation();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    InputUpdate();

       try
       {
           MoveLogic();
       }catch{}

	    
	}

    void FixedUpdate()
    {
        //Debug.Log(localPlayer);
        if (localPlayer == null)
        {
            //Debug.Log(PhotonServer.Instance.Players[0].CharacterName);
            //Debug.Log(PhotonServer.Instance.Players[0].name);
            //Debug.Log("Character name is: " + PhotonServer.Instance.CharacterName);
            var p = PhotonServer.Instance.Players.FirstOrDefault(
                n => n.CharacterName.Equals(PhotonServer.Instance.CharacterName));
            if (p != null)
            {
                localPlayer = p;
                PhotonServer.Instance.ListPlayersOperation();
            }
            return;
        }

        TrySend();
    }

    private void MoveLogic()
    {
        for (int i = 0; i < PhotonServer.Instance.Players.Count; i++)
        {
            var player = PhotonServer.Instance.Players[i];
            if (player != localPlayer)
            {
                //player.Position = player.NewPosition;
                Debug.Log("Moving player: " + player.CharacterName);
                player.Position = Vector3.Lerp(player.Position, player.NewPosition, Time.fixedDeltaTime*15f);
            } 
        }
    }

    private void InputUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Pressed A");
            Move(-Step, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Pressed D");
            Move(Step, 0, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("Pressed W");
            Move(0, 0, Step);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //Debug.Log("Pressed S");
            Move(0, 0, -Step);
        }
    }

    private void Move(float x,float y,float z)
    {
        if (localPlayer != null)
            localPlayer.Position += new Vector3(x, y, z)*Time.fixedDeltaTime;
    }

    private void TrySend()
    {
        //Debug.Log("Try send is called");
        if (localPlayer.Position != oldPosition && lastSendTime < Time.time) { 

            //Debug.Log("trying to send move coord");
            oldPosition = localPlayer.Position;
            lastSendTime = Time.time + SendRate;

            PhotonServer.Instance.MoveOperation(oldPosition.x, oldPosition.y, oldPosition.z);
        }
    }
}
