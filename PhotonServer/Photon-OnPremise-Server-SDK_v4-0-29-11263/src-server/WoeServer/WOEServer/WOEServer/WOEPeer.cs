using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if ((OperationCode)operationRequest.OperationCode == OperationCode.Login) {
                working = "ok";                
            }
        }
    }
}
