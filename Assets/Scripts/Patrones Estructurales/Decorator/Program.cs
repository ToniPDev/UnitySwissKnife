using System;
using UnityEngine;

namespace Patrones_Estructurales.Decorator
{
    public class Program : MonoBehaviour
    {
        private void Start()
        {
            var complex = new PinchosParaMaza( new MazaEnorme(new OrcoBaseSinArma()));
            Debug.Log("Un enemigo complejo " + complex.DamageDealt());
        }
    }
}