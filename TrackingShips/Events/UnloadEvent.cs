namespace TrackingShips.Events;

internal class UnloadEvent : DomainEvent
{
    private readonly Cargo _cargo;

    private readonly Ship _ship;

    public UnloadEvent(DateTime occurred, Cargo cargo, Ship ship)
        : base(occurred)
    {
        _cargo = cargo;
        _ship = ship;
    }

    internal override void Process()
    {
        Cargo?.HandleUnload(this);
    }

    internal override void Reverse()
    {
    }

    public Cargo Cargo => _cargo;

    public Ship Ship => _ship;
}
