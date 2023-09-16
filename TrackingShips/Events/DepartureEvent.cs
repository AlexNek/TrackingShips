namespace TrackingShips.Events
{
    internal class DepartureEvent : DomainEvent
    {
        private readonly Port _port;

        private readonly Ship _ship;

        internal DepartureEvent(DateTime time, Port port, Ship ship)
            : base(time)
        {
            _port = port;
            _ship = ship;
        }

        internal override void Process()
        {
            Ship.HandleDeparture(this);
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
