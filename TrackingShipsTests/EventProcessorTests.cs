using FluentAssertions;

using TrackingShips;
using TrackingShips.Events;

namespace TrackingShips.Tests
{
    public class EventProcessorTests
    {
        private readonly Ship _ship1;

        private readonly Port _port1;

        private readonly Port _port2;

        private readonly Port _port3;

        private readonly Cargo _cargo1;

        private readonly EventProcessor _eventProcessor;

        public EventProcessorTests()
        {
            _eventProcessor = new EventProcessor();
            _cargo1 = new Cargo("Refactoring");
            _ship1 = new Ship("King Roy") { Id = 1 };
            _port1 = new Port("San Francisco", Country.US);
            _port2 = new Port("Los Angeles", Country.US);
            _port3 = new Port("Vancouver", Country.CANADA);
            Registry.Cargos.Add(_cargo1);
            Registry.Ships.Add(_ship1);
        }


        [Fact]
        public void ArrivalSetsShipsLocation()
        {
            ArrivalEvent ev = new ArrivalEvent(new DateTime(2005, 11, 1), _port1, _ship1);
            _eventProcessor.Process(ev);
            _ship1.Port.Should().Be(_port1);
        }

        [Fact]
        public void DeparturePutsShipOutToSea()
        {
            _eventProcessor.Process(new ArrivalEvent(new DateTime(2005, 10, 1), _port2, _ship1));
            _eventProcessor.Process(new ArrivalEvent(new DateTime(2005, 11, 1), _port1, _ship1));
            _eventProcessor.Process(new DepartureEvent(new DateTime(2005, 11, 1), _port1, _ship1));

            _ship1.Port.Should().BeNull();
        }

        [Fact]
        public void VisitingCanadaMarksCargo()
        {
            _eventProcessor.Process(new ArrivalEvent(new DateTime(2005, 11, 2), _port3, _ship1));
            _eventProcessor.Process(new LoadEvent(new DateTime(2005, 11, 1), _cargo1.Id, _ship1.Id));
            _eventProcessor.Process(new DepartureEvent(new DateTime(2005, 11, 3), _port3, _ship1));
            _eventProcessor.Process(new ArrivalEvent(new DateTime(2005, 11, 4), _port1, _ship1));
            _eventProcessor.Process(new UnloadEvent(new DateTime(2005, 11, 5), _cargo1, _ship1));
            _cargo1.HasBeenInCanada.Should().BeTrue();
        }

        [Fact(Skip = "Not implemented for this example")]
        public void DepartureWithoutArrivalThrowsException()
        {
            var departureEvent = new DepartureEvent(new DateTime(2005, 11, 1), _port1, _ship1);
            // Fluent Assertions provides the Invoking() method to assert exceptions
            Action act = () => _eventProcessor.Process(departureEvent);
            // Replace Exception with the specific type of exception you expect to be thrown, if any.
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void LoadWhileOutToSeaThrowsException()
        {
            var loadEvent = new LoadEvent(new DateTime(2005, 11, 1), _cargo1.Id, _ship1.Id);
            // Fluent Assertions provides the Invoking() method to assert exceptions
            Action act = () => _eventProcessor.Process(loadEvent);
            // Replace Exception with the specific type of exception you expect to be thrown, if any.
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void UnloadWhileOutToSeaThrowsException()
        {
            var unloadEvent = new UnloadEvent(new DateTime(2005, 11, 1), _cargo1, _ship1);
            // Fluent Assertions provides the Invoking() method to assert exceptions
            Action act = () => _eventProcessor.Process(unloadEvent);
            // Replace Exception with the specific type of exception you expect to be thrown, if any.
            act.Should().Throw<Exception>();
        }
    }

    
}
