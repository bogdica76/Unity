using WOEServer.Common;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using WOEServer.Operations;
using System.Collections.Generic;

namespace WOEServer
{
    class WOEPeer : ClientPeer
    {
        public string name;
        public Vector3Net Position { get; private set; }

        public WOEPeer(InitRequest initRequest)
        : base(initRequest)
        {
        }

        protected override void OnDisconnect(DisconnectReason disconnectCode, string reasonDetail)
        {
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            switch (operationRequest.OperationCode)
            {
                case (byte)OperationCodes.Login:
                    // ((OperationCodes)operationRequest.OperationCode == OperationCodes.Login) {
                    Login loginRequest = new Login(Protocol, operationRequest);
                    string user = loginRequest.User;
                    string pass = loginRequest.Pass;

                    //verific daca e deja logat contul curent
                    if (World.Instance.IsContain(user))
                    {
                        SendOperationResponse(loginRequest.GetResponse(ErrorCodes.NameIsExist), sendParameters);
                        return;
                    }                    

                    OperationResponse response = new OperationResponse();
                    response.OperationCode = (byte)OperationCodes.Login;
                    DatabaseManager dbManager = new DatabaseManager();
                    if (dbManager.LoginToGame(user, pass))
                    {
                        response.ReturnCode = 0;
                        name = user;
                        Position = new Vector3Net(0, 0, 0);
                        World.Instance.AddClient(this);
                    }
                    else
                    {
                        response.ReturnCode = 1;
                    }

                    SendOperationResponse(response, sendParameters);
                    break;
                case (byte)OperationCodes.Move:
                    {
                        var moveRequest = new Move(Protocol, operationRequest);

                        if (!moveRequest.IsValid)
                        {
                            SendOperationResponse(moveRequest.GetResponse(ErrorCodes.InvalidParameters), sendParameters);
                            return;
                        }

                        Position = new Vector3Net(moveRequest.X, moveRequest.Y, moveRequest.Z);

                        var eventData = new EventData((byte)EventCodes.Move);
                        eventData.Parameters = new Dictionary<byte, object>
                            {
                                {(byte) ParameterCodes.PosX, Position.X},
                                {(byte) ParameterCodes.PosY, Position.Y},
                                {(byte) ParameterCodes.PosZ, Position.Z},
                                {(byte)ParameterCodes.User, name}
                            };
                        //SendEvent(eventData, sendParameters);
                        eventData.SendTo(World.Instance.Clients, sendParameters);
                    }
                    break;

                case (byte)OperationCodes.WorldEnter:
                    {
                        EventData eventData = new EventData((byte)EventCodes.WorldEnter);
                        
                        eventData.Parameters = new Dictionary<byte, object>
                            {
                                {(byte) ParameterCodes.PosX, Position.X},
                                {(byte) ParameterCodes.PosY, Position.Y},
                                {(byte) ParameterCodes.PosZ, Position.Z},
                                {(byte)ParameterCodes.User, name}
                            };
                        //SendEvent(eventData, sendParameters);
                        eventData.SendTo(World.Instance.Clients, sendParameters);
                        //eventData.SendTo(World.Instance.Clients, sendParameters);
                        //Log.Info("Operation WorldEnter:" + name);
                    }
                    break;

                case (byte)OperationCodes.ListPlayers:
                    {
                        ListPlayersHandler(sendParameters);
                    }
                    break;
            }


        }


        private void ListPlayersHandler(SendParameters sendParameters)
        {
            OperationResponse response = new OperationResponse((byte)OperationCodes.ListPlayers);

            var players = World.Instance.Clients;

            var dicPlayers = new Dictionary<string, object[]>();

            foreach (var p in players)
            {
                if (!p.name.Equals(name))
                    dicPlayers.Add(p.name, new object[] { p.Position.X, p.Position.Y, p.Position.Z });
            }


            response.Parameters = new Dictionary<byte, object> { { (byte)ParameterCodes.ListPlayers, dicPlayers } };
            SendOperationResponse(response, sendParameters);

            //Log.Debug("ListPlayersHandler:" + CharacterName);
        }
    }
}
