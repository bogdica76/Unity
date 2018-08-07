using System;

namespace KaymakGames_Server
{
    class General
    {
        public static int GetTickCount()
        {
            return Environment.TickCount;
        }

        public static void InitializeServer()
        {
            int startTime = 0; int endTime = 0;
            startTime = GetTickCount();
            Text.WriteDebug("Initializing Server...");
            //Intializing all game data arrays
            Text.WriteDebug("Initializing Game Arrays...");
            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                ServerTCP.Client[i] = new Clients();
            }

            //Start the Networking
            Text.WriteDebug("Initializing Network...");
            ServerHandleData.InitializePackets();
            ServerTCP.InitializeNetwork();

            endTime = GetTickCount();
            Text.WriteInfo("Initialization complete. Server loaded in " + (endTime - startTime) + " ms.");
        }

        public static void JoinedGame(int connectionID) {
            ServerTCP.SendIngame(connectionID);
            ServerTCP.SendInWorld(connectionID);
        }
    }
}
