namespace TrackingShips.Events
{
    internal class LoadEvent : DomainEvent
    {
        private readonly string _cargoCode;

        private readonly int _shipCode;

        private string? _priorPort;

        internal LoadEvent(DateTime occurred, string cargoCode, int shipId)
            : base(occurred)
        {
            _shipCode = shipId;
            _cargoCode = cargoCode;
        }

        internal override void Process()
        {
            if (Ship.IsInPort)
            {
                Cargo?.HandleLoad(this);
            }
            else
            {
                throw new InvalidOperationException("Ship must be in the port");
            }
        }

        internal override void Reverse()
        {
            Cargo?.ReverseLoad(this);
        }

        internal Cargo? Cargo
        {
            get
            {
                //return Cargo.Find(_cargoCode);
                return Registry.Cargos.Find((x) => x.Id == _cargoCode);
            }
        }

        internal Ship? Ship
        {
            get
            {
                return Registry.Ships.Find((x) => x.Id == _shipCode);
            }
        }

        public string? PriorPort
        {
            get
            {
                return _priorPort;
            }
            set
            {
                _priorPort = value;
            }
        }
    }
}
