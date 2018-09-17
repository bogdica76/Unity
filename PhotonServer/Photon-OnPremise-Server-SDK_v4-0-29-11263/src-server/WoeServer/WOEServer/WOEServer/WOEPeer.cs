using WOEServer.Common;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

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
                working = "ok";                
            }
        }
    }
}
