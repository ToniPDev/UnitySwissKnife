namespace Patrones_Estructurales.Proxy
{
    //La interfaz declara funciones comunes para el servicio y el proxy.
    public interface IProxyCommunicator
    {
        public void Request(int damage);
    }
}