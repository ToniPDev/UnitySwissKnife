using System.Collections.Generic;
using System.Linq;

namespace Patrones_Estructurales.Composite
{
    /// <summary>
    /// Esta seria una rama del arbol tiene un constructor con varias armas que es capaz de sumar el resultado de todas.
    /// </summary>
    class TwoHandedWeapons : WeaponBranch
    {
        private readonly List<WeaponBranch> _children;
        public TwoHandedWeapons(List<WeaponBranch> children) : base(children) => _children = children;
        
        public override void Add(WeaponBranch component) => _children.Add(component);

        public override void Remove(WeaponBranch component) => _children.Remove(component);

        public override int Damage(WeaponType type)
        {
            int sum = 0;
            foreach (var weapon in _children)
            {
                if (weapon.Type() == type) sum += weapon.Damage(type);
            }

            return sum;
        }
    }
    class WaterWeapons : WeaponBranch
    {
        private readonly List<WeaponBranch> _children;
        public WaterWeapons(List<WeaponBranch> children) : base(children) => _children = children;
        public override WeaponType Type() => WeaponType.Water;
        public override void Add(WeaponBranch component) => _children.Add(component);

        public override void Remove(WeaponBranch component) => _children.Remove(component);

        public override int Damage(WeaponType type) => _children.Where(weapon => weapon.Type() == type).Sum(weapon => weapon.Damage(type));
    }
}