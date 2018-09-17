using WOEServer.Common;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using WOEServer.Operations;

namespace WOEServer
{
    class WOEPeer: ClientPeer
    {
        private string working = "Maybe";

        public WOEPeer(InitRequest initRequest)
        : base(initRequest)
        {
        }

        protected override void OnDisconnect(DisconnectReason disconnectCode, string reasonDetail)
        {
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            if ((OperationCodes)operationRequest.OperationCode == OperationCodes.Login) {
                Login loginRequest = new Login(Protocol, operationRequest);
                string user = loginRequest.CharacterName;
                OperationResponse response = new OperationResponse();
                response.OperationCode = (byte)OperationCodes.Login;

                if (user == "mama")
                {
                    response.ReturnCode = 0;
                }
                else {
                    response.ReturnCode = 1;
                }
                SendOperationResponse(response, sendParameters);
            }
        }
    }
}
