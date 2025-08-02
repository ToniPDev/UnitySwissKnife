namespace PatronesDeComportamiento.ChainOfResponsabillity
{
    public class OrcDmgReceiver : AbstractDamageHandler
    {
        public override object Handle(object request) => request as string is "fuego" or "hielo" ? _nextHandler.Handle(request) : "Orc received normal dmg";
    }
    
    public class IcedOrcDmgReceiver : AbstractDamageHandler
    {
        public override object Handle(object request) => request as string is "fuego" ? "Orc received fire dmg" : base.Handle(request);
    }
    
    public class FireOrcDmgReceiver : AbstractDamageHandler
    {
        public override object Handle(object request) => request as string is "hielo" ? "Orc received ice dmg" : base.Handle(request);
    }
}