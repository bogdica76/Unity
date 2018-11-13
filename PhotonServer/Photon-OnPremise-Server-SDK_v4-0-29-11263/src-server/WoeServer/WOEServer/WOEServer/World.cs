using System.Collections.Generic;
using ExitGames.Threading;
using System.Threading;

namespace WOEServer
{
    class World
    {
        public static readonly World Instance = new World();

        public List<WOEPeer> Clients { get; private set; }

        private readonly ReaderWriterLockSlim readWriteLock;

        public World()
        {
            Clients = new List<WOEPeer>();
            readWriteLock = new ReaderWriterLockSlim();
        }

        public WOEPeer TryGetByName(string name)
        {
            using (ReadLock.TryEnter(this.readWriteLock, 1000))
            {
                return Clients.Find(n => n.name.Equals(name));
            }
        }

        public bool IsContain(string name)
        {
            using (ReadLock.TryEnter(this.readWriteLock, 1000))
            {
                return Clients.Exists(n => n.name.Equals(name));
            }
        }

        public void AddClient(WOEPeer client)
        {
            using (WriteLock.TryEnter(this.readWriteLock, 1000))
            {
                Clients.Add(client);
            }
        }

        public void RemoveClient(WOEPeer client)
        {
            using (WriteLock.TryEnter(this.readWriteLock, 1000))
            {
                Clients.Remove(client);
            }
        }

        ~World()
        {
            readWriteLock.Dispose();
        }
    }
}

