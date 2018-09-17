using System.Collections.Generic;
using UnityEngine;

public class ClientHandleData : MonoBehaviour
{
    /*

    public static Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();
    public GameObject playerPref;
    private static GameObject playerpref_;

    public static ByteBuffer playerBuffer;
    private delegate void Packet_(byte[] data);
    private static Dictionary<long, Packet_> packets;
    private static long pLength;

    private void Awake()
    {
        InitializePackets();
        playerpref_ = playerPref;
    }

    private void InitializePackets()
    {
        Debug.Log("Initializing Network Messages...");
        packets = new Dictionary<long, Packet_>
        {
            { (long)ServerPackets.SWelcomeMessage, PACKET_WELCOMEMESSAGE },
            { (long)ServerPackets.SIngame, PACKET_INGAME },
            { (long)ServerPackets.SPlayerData, PACKET_PLAYERDATA },
            { (long)ServerPackets.SPlayerMove, PACKET_PLAYERMOVE },
        };
    }

    public static void HandleData(byte[] data)
    {
        byte[] Buffer;
        Buffer = (byte[])data.Clone();

        if (playerBuffer == null) { playerBuffer = new ByteBuffer(); };

        playerBuffer.WriteBytes(Buffer);

        if (playerBuffer.Count() == 0)
        {
            playerBuffer.Clear();
            return;
        }

        if (playerBuffer.Length() >= 8)
        {
            pLength = playerBuffer.ReadLong(false);
            if (pLength <= 0)
            {
                playerBuffer.Clear();
                return;
            }
        }

        while (pLength > 0 & pLength <= playerBuffer.Length() - 8)
        {
            if (pLength <= playerBuffer.Length() - 8)
            {
                playerBuffer.ReadLong(); //Reads out the Packet Identifier;
                data = playerBuffer.ReadBytes((int)pLength); // Gets the full package Length
                HandleDataPackets(data);
            }

            pLength = 0;

            if (playerBuffer.Length() >= 8)
            {
                pLength = playerBuffer.ReadLong(false);

                if (pLength < 0)
                {
                    playerBuffer.Clear();
                    return;
                }
            }
        }
    }
    private static void HandleDataPackets(byte[] data)
    {
        long packetIdentifier; ByteBuffer buffer;
        Packet_ packet;

        buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        packetIdentifier = buffer.ReadLong();
        buffer.Dispose();

        if (packets.TryGetValue(packetIdentifier, out packet))
        {
            packet.Invoke(data);
        }
    }

    static void PACKET_WELCOMEMESSAGE(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        long packetIdentifier = buffer.ReadLong();
        int connectionID = buffer.ReadInteger();
        string msg = buffer.ReadString();

        Debug.Log(msg);
    }

	static void PACKET_INGAME(byte[] data){
		ByteBuffer buffer = new ByteBuffer();
		buffer.WriteBytes(data);

		long packetIdentifier = buffer.ReadLong();
		int connectionID = buffer.ReadInteger();
        Globals.myConnectionID = connectionID;
	}

    static void PACKET_PLAYERDATA(byte[] data) {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        long packetIdentifier = buffer.ReadLong();
        int connectionID = buffer.ReadInteger();

        Debug.Log(connectionID + " - " + Globals.myConnectionID);

        GameObject temp = playerpref_;
        temp.name = "Player: " + connectionID;
        temp.GetComponent<Player>().connectionID = connectionID;
        var vPlayer = Instantiate(temp);
        Debug.LogError("Just spawned Player: " + connectionID);
        if (connectionID == Globals.myConnectionID)
        {
            Debug.Log("setting camera for: " + connectionID);
            //temp.transform.GetChild(0).gameObject.SetActive(false);

            GameObject.Find("CameraArm").GetComponent<FollowPlayer>().playerToFollow = vPlayer;
        }
    }

    static void PACKET_PLAYERMOVE(byte[] data) {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        long packetIdentifier = buffer.ReadLong();

        int connectionID = buffer.ReadInteger();

        float x = buffer.ReadFloat();
        float y = buffer.ReadFloat();
        float z = buffer.ReadFloat();

        float rotx = buffer.ReadFloat();
        float roty = buffer.ReadFloat();
        float rotz = buffer.ReadFloat();

        Debug.Log("Moving player Player: " + connectionID + "(Clone)");

        GameObject player = GameObject.Find("Player: " + connectionID + "(Clone)");
        player.transform.position = new Vector3(x, y, z);
        player.transform.eulerAngles = new Vector3(
            General.WrapAngle(rotx),
            General.WrapAngle(roty),
            General.WrapAngle(rotz)
            );

        buffer.Dispose();
    }
    */
}
