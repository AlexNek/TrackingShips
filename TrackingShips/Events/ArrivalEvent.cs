using System.Collections;

namespace TrackingShips.Events
{
    internal class ArrivalEvent : DomainEvent
    {
        private readonly Port? _port;

        private readonly Ship? _ship;

        internal IDictionary _priorCargoInCanada = new Hashtable();

        internal Port? _priorPort;

        internal ArrivalEvent(DateTime occurred, Port port, Ship ship)
            : base(occurred)
        {
            _port = port;
            _ship = ship;
        }

        internal override void Process()
        {
            Ship.HandleArrival(this);
        }

        internal override void Reverse()
        {
        }

        internal Port Port
        {
            get
            {
                return _port;
            }
        }

        internal Ship Ship
        {
            get
            {
                return _ship;
            }
        }
    }
}
