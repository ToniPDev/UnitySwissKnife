using System;
using System.Collections.Generic;

namespace Patrones_Estructurales.Composite
{
    /// <summary>
    /// La clase declara metodos para grupos y para objetos.
    /// </summary>
    public abstract class WeaponBranch
    {
        protected WeaponBranch(List<WeaponBranch> child) { }

        protected WeaponType Weapon;
        public enum WeaponType
        {
            Fire,
            Water,
        }
        
        //La clase puede implementar un comportamiento base o no
        public abstract int Damage(WeaponType type);

        //Sirve para administrar a los hijos
        public virtual WeaponType Type()
        {
            throw new NotImplementedException();
        }
        public virtual void Add(WeaponBranch weaponBranch)
        {
            throw new NotImplementedException();
        }
        public virtual void Remove(WeaponBranch weaponBranch)
        {
            throw new NotImplementedException();
        }
        
    }
}