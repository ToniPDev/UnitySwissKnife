namespace PatronesDeComportamiento.Observer
{
    public interface ISubject
    {
        public int State { get; set; }
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}