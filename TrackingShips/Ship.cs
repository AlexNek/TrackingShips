using TrackingShips.Events;

namespace TrackingShips
{
    internal class Ship
    {
        private readonly List<Cargo> _cargo = new List<Cargo>();

        public Ship(string name)
        {
            Name = name;
        }

        public Cargo? FindCargo(string cargoCode)
        {
            return _cargo.Find(x => x.Id == cargoCode);
        }

        public void HandleArrival(ArrivalEvent ev)
        {
            Port = ev.Port;
            foreach (Cargo c in _cargo)
            {
                c.HandleArrival(ev);
            }
            Console.WriteLine($"ship '{Name}' has arrived at the port of {Port?.Name} ({Port?.Country}), date {ev.Occurred.ToShortDateString()}, cargo {_cargo.Count} units");
        }

        public void HandleDeparture(DepartureEvent ev)
        {
            Console.WriteLine($"ship '{Name}' departed from port {Port?.Name} ({Port?.Country}), date {ev.Occurred.ToShortDateString()}, cargo {_cargo.Count} units");

            //Port = Port.AT_SEA;
            Port = null;
        }

        public void HandleLoad(LoadEvent ev)
        {
            if (IsInPort)
            {
                if (ev.Cargo != null)
                {
                    Console.WriteLine($"loaded cargo '{ev.Cargo.Id}'");
                    _cargo.Add(ev.Cargo);
                }
            }
            else
            {
                throw new InvalidOperationException("Ship must be in the port");
            }
        }

        public void HandleUnLoad(UnloadEvent ev)
        {
            if (IsInPort)
            {
                if (ev.Cargo != null)
                {
                    Console.WriteLine($"unloaded cargo '{ev.Cargo.Id}'");
                    _cargo.Remove(ev.Cargo);
                }
            }
            else
            {
                throw new InvalidOperationException("Ship must be in the port");
            }
        }

        public void ReverseLoad(LoadEvent ev)
        {
        }

        public int Id { get; init; }

        public bool IsInPort => Port != null;

        public string Name { get; init; }

        public Port? Port { get; set; }
    }
}
