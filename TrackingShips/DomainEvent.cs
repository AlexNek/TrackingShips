namespace TrackingShips
{
    internal abstract class DomainEvent
    {
        private readonly DateTime _occurred;

        private readonly DateTime _recorded;

        protected DomainEvent(DateTime occurred)
        {
            _occurred = occurred;
            _recorded = DateTime.Now;
        }

        internal abstract void Process();

        internal abstract void Reverse();

        public DateTime Occurred => _occurred;

        public DateTime Recorded => _recorded;
    }
}
