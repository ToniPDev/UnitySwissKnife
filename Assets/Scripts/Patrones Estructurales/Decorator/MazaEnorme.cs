using UnityEngine;

namespace Patrones_Estructurales.Decorator
{
    public class MazaEnorme : Decorator
    {
        //Llaman al objeto envuelto y alteran su resultado
        public MazaEnorme(Component comp) : base(comp) => Component = comp;

        //Pueden implementar o no esta funcion, permitiendo su extension
        public override int DamageDealt() => Component.DamageDealt() + 20;
    }
}