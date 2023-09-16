using TrackingShips.Events;

namespace TrackingShips
{
    internal class Program
    {
        private static Cargo _cargo1;

        private static EventProcessor _eventProcessor;

        private static Port _port1;

        private static Port _port2;

        private static Port _port3;

        private static Ship _ship1;

        private static Ship _ship2;

        private static void Init()
        {
            _eventProcessor = new EventProcessor();
            _cargo1 = new Cargo("Refactoring");
            _ship1 = new Ship("King Roy") { Id = 1 };
            _ship2 = new Ship("Prince Trevor") { Id = 2 };
            _port1 = new Port("San Francisco", Country.US);
            _port2 = new Port("Los Angeles", Country.US);
            _port3 = new Port("Vancouver", Country.CANADA);
            Registry.Cargos.Add(_cargo1);
            Registry.Ships.Add(_ship1);
            Registry.Ships.Add(_ship2);
        }

        private static void Main(string[] args)
        {
            Init();
            var events = new List<DomainEvent>
                             {
                                               new ArrivalEvent(new DateTime(2005, 11, 2), _port3, _ship1),
                                               new LoadEvent(new DateTime(2005, 11, 1), _cargo1.Id, _ship1.Id),
                                               new DepartureEvent(new DateTime(2005, 11, 3), _port3, _ship1),
                                               new ArrivalEvent(new DateTime(2005, 11, 4), _port2, _ship2),
                                               new ArrivalEvent(new DateTime(2005, 11, 4), _port1, _ship1),
                                               new UnloadEvent(new DateTime(2005, 11, 5), _cargo1, _ship1),
                                               new DepartureEvent(new DateTime(2005, 11, 7), _port1, _ship1),
                                               new ArrivalEvent(new DateTime(2005, 12, 1), _port2, _ship1)
                                           };

            Console.WriteLine("Martin Fowler event sourcing example:");
            
            foreach (var domainEvent in events)
            {
                _eventProcessor.Process(domainEvent);
            }
        }
    }
}
/*
 *https://martinfowler.com/eaaDev/EventSourcing.html
Martin Fowler event sourcing example
   ship 'King Roy' has arrived at the port of Vancouver (CA), date 02.11.2005, cargo 0 units
   loaded cargo 'Refactoring'
   ship 'King Roy' departed from port Vancouver (CA), date 03.11.2005, cargo 1 units
   ship 'Prince Trevor' has arrived at the port of Los Angeles (US), date 04.11.2005, cargo 0 units
   ship 'King Roy' has arrived at the port of San Francisco (US), date 04.11.2005, cargo 1 units
   unloaded cargo 'Refactoring'
   ship 'King Roy' departed from port San Francisco (US), date 07.11.2005, cargo 0 units
   ship 'King Roy' has arrived at the port of Los Angeles (US), date 01.12.2005, cargo 0 units
 */
