using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;

namespace WOEServer
{
    class WOEServerBase: ApplicationBase
    {
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new WOEPeer(initRequest);
        }
        protected override void Setup()
        {
        }

        protected override void TearDown()
        {
        }
    }
}
