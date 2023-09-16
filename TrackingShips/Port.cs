using TrackingShips.Events;

namespace TrackingShips
{
    internal class Port
    {
        public Port(string name, string country)
        {
            Name = name;
            Country = country;
        }
        //private const Port AT_SEA = VALUE;

        public void HandleArrival(ArrivalEvent ev)
        {
            ev.Ship.Port = this;
            Registry.CustomsNotificationGateway.Notify(ev.Occurred, ev.Ship, ev.Port);
        }

        public string Name { get; init; }

        public string Country { get; init; }
    }
}
