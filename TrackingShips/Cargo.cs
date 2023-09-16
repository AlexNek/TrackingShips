using System.Diagnostics;

using TrackingShips.Events;

namespace TrackingShips
{
    internal class Cargo
    {
        private bool _hasBeenInCanada;

        private Ship? _ship;

        private string? _port;

        public Cargo(string id)
        {
            Id = id;
        }

       
        // 1st version
        //public void HandleArrival(ArrivalEvent ev)
        //{
        //    if (Country.CANADA == ev.Port.Country)
        //    {
        //        HasBeenInCanada = true;
        //    }
        //}

        public void HandleArrival(ArrivalEvent ev)
        {
            ev._priorCargoInCanada[this] = _hasBeenInCanada;
            if (Country.CANADA == ev.Port.Country)
            {
                _hasBeenInCanada = true;
            }
        }

        public void ReverseLoad(LoadEvent ev)
        {
            _ship.ReverseLoad(ev);
            _ship = null;
            _port = ev.PriorPort;
        }

        internal void HandleLoad(LoadEvent ev)
        {
            ev.PriorPort = _port;
            _port = null;
            _ship = ev.Ship;
            _ship.HandleLoad(ev);
        }

        public void ReverseArrival(ArrivalEvent ev)
        {
            _hasBeenInCanada = (bool)(ev._priorCargoInCanada[this] ?? false);
        }

        internal void HandleUnload(UnloadEvent ev)
        {
            Debug.Assert(_ship != null, nameof(_ship) + " == null");
            _ship.HandleUnLoad(ev);
            _port = null;
            _ship = null;
        }

        public bool HasBeenInCanada
        {
            get
            {
                return _hasBeenInCanada;
            }
        }

        public string Id { get; init; }
        
    }
}
