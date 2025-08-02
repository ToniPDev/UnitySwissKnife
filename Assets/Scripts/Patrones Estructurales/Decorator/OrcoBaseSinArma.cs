using UnityEngine;

namespace Patrones_Estructurales.Decorator
{
    public class OrcoBaseSinArma : Component
    {
        //Otorgan implementaciones por defecto
        //Pueden haber varios concrete components => OrcosBase, ElfosBase
        public override int DamageDealt() => 10;
    }
}