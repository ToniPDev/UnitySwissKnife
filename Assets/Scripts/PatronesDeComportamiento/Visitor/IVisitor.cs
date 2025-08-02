namespace PatronesDeComportamiento.Visitor
{
    //Declara metodos que corresponden con los diferentes tipos de componentes.
    public interface IVisitor
    {
        void VisitConcreteComponentA(ConcreteComponentA element);

        void VisitConcreteComponentB(ConcreteComponentB element);
    }
}