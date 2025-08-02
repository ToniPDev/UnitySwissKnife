using System.Collections.Generic;
using UnityEngine;

namespace Patrones_Estructurales.Composite
{
    /// <summary>
    /// Clase que contiene todas las armas del jugador.
    /// </summary>
    public class PlayerInventoryWeapons : MonoBehaviour
    {
        private void Start()
        {
            //Armas a dos manos
            FireDagger fDagger = new FireDagger(null);
            FireSword fSword = new FireSword(null);

            //Armas a dos manos y de agua
            LeftWatterDagger lWaterDagger = new LeftWatterDagger(null);
            RightWatterDagger rWaterDagger = new RightWatterDagger(null);
            WaterSword waterSword = new WaterSword(null);
            
            //Ramas de armas a dos manos y de agua
            WaterWeapons wWeapons = new WaterWeapons(new List<WeaponBranch>{lWaterDagger,rWaterDagger,waterSword});
            TwoHandedWeapons twoHandedWeapons = new TwoHandedWeapons(new List<WeaponBranch>{wWeapons,fDagger,fSword});

            Debug.Log("El daño de las armas de fuego es " + twoHandedWeapons.Damage(WeaponBranch.WeaponType.Fire));
            Debug.Log("El daño de las armas de agua es " + twoHandedWeapons.Damage(WeaponBranch.WeaponType.Water));
        }
    }
}