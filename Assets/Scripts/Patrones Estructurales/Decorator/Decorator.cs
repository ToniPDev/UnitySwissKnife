namespace Patrones_Estructurales.Decorator
{
    //Sigue la estructura base del componente, su función es definir sus envoltorios con un campo para guardarlos
    public abstract class Decorator : Component
    {
        protected Component Component;
        protected Decorator(Component component) => Component = component;

        public void SetComponent(Component component) => Component = component;

        //Este delega el trabajo al componente que envuelve
        public override int DamageDealt() => Component?.DamageDealt() ?? 0;
    }
}
