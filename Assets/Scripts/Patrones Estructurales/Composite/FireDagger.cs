using System.Collections.Generic;


namespace Patrones_Estructurales.Composite
{
    /// <summary>
    /// La daga en este caso seria una hoja del arbol, nos devuelve los valores que queramos.
    /// </summary>
    public class FireDagger : WeaponBranch
    {
        public override WeaponType Type() => WeaponType.Fire;
        public override int Damage(WeaponType type) => 5;
        public FireDagger(List<WeaponBranch> child) : base(child) {}
    }

    public class FireSword : WeaponBranch
    {
        public override WeaponType Type() => WeaponType.Fire;
        public override int Damage(WeaponType type) => 8;
        public FireSword(List<WeaponBranch> child) : base(child) { }
    }
    
    public class LeftWatterDagger : WeaponBranch
    {
        public override WeaponType Type() => WeaponType.Water;
        public override int Damage(WeaponType type) => 5;
        public LeftWatterDagger(List<WeaponBranch> child) : base(child) { }
    }
    public class RightWatterDagger : WeaponBranch
    {
        public override WeaponType Type() => WeaponType.Water;
        public override int Damage(WeaponType type) => 5;
        public RightWatterDagger(List<WeaponBranch> child) : base(child) { }
    }
    
    public class WaterSword : WeaponBranch
    {
        public override WeaponType Type() => WeaponType.Water;
        public override int Damage(WeaponType type) => 20;
        public WaterSword(List<WeaponBranch> child) : base(child) { }
    }
}