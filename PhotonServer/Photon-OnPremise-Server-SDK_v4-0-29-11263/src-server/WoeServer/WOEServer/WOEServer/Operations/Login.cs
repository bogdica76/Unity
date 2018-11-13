using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using WOEServer.Common;

namespace WOEServer.Operations
{
    public class Login : BaseOperation
    {
        public Login(IRpcProtocol protocol, OperationRequest request)
            : base(protocol, request)
        {
        }

        [DataMember(Code = (byte)ParameterCodes.User)]
        public string User { get; set; }

        [DataMember(Code = (byte)ParameterCodes.Pass)]
        public string Pass { get; set; }
    }
}