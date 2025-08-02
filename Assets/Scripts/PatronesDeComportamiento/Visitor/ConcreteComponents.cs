namespace PatronesDeComportamiento.Visitor
{
    //Implementamos la interfaz componente y visitamos el componente con el mismo nombre que la clase
    public class ConcreteComponentA : IComponent
    {
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteComponentA(this);
        }

        public string ExclusiveMethodOfComponentA() => "A";
    }
    
    public class ConcreteComponentB : IComponent
    {
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteComponentB(this);
        }
        
        public string ExclusiveMethodOfComponentB() => "B";
    }
}