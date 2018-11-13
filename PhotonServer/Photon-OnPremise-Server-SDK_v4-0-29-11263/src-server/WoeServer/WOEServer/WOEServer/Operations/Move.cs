using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using WOEServer.Common;


namespace WOEServer.Operations
{
    public class Move : BaseOperation
    {
        public Move(IRpcProtocol protocol, OperationRequest request) : base(protocol, request)
        {
        }

        [DataMember(Code = (byte)ParameterCodes.PosX)]
        public float X { get; set; }

        [DataMember(Code = (byte)ParameterCodes.PosY)]
        public float Y { get; set; }

        [DataMember(Code = (byte)ParameterCodes.PosZ)]
        public float Z { get; set; }
    }
}
