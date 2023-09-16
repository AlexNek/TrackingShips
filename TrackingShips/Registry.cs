namespace TrackingShips
{
    internal static class Registry
    {
        private static readonly CustomsEventGateway _CustomsNotificationGateway = new CustomsEventGateway();

        private static readonly Ships _ships = new Ships();

        private static readonly Cargos _cargos = new Cargos();

        public static CustomsEventGateway CustomsNotificationGateway
        {
            get
            {
                return _CustomsNotificationGateway;
            }
        }

        public static Ships Ships
        {
            get
            {
                return _ships;
            }
        }

        public static Cargos Cargos
        {
            get
            {
                return _cargos;
            }
        }
    }

    internal class Ships : List<Ship>
    {

    }

    internal class Cargos : List<Cargo>
    {

    }
}
