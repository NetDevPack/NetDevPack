using NetDevPack.SimpleMediator.Core.Interfaces;
using System;

namespace NetDevPack.Messaging
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}