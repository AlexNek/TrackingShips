using System.Collections;

namespace TrackingShips
{
    internal class EventProcessor
    {
        private readonly IList log = new ArrayList();
        //public void Process(DomainEvent e)
        //{
        //    e.Process();
        //    log.Add(e);
        //}

        public void Process(DomainEvent e)
        {
            IsActive = true;
            e.Process();
            IsActive = false;
            log.Add(e);
        }

        public bool IsActive { get; private set; }
    }
}
