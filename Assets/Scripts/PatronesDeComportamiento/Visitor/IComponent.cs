namespace PatronesDeComportamiento.Visitor
{
    //declara el metodo accept que debe llevar el visitante como argumento
    public interface IComponent
    {
        void Accept(IVisitor visitor);
    }
}