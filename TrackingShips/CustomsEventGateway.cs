namespace TrackingShips
{
    internal class CustomsEventGateway
    {
        private EventProcessor processor;

        public void Notify(DateTime arrivalDate, Ship ship, Port port)
        {
            if (processor.IsActive)
            {
                SendToCustoms(BuildArrivalMessage(arrivalDate, ship, port));
            }
        }

        private void SendToCustoms(object buildArrivalMessage)
        {
        }

        private object BuildArrivalMessage(DateTime arrivalDate, Ship ship, Port port)
        {
            return null;
        }
    }
}
