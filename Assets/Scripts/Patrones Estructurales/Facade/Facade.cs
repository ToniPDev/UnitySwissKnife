using System;
using UnityEngine;

namespace Patrones_Estructurales.Facade
{
    //La fachada se va a encargar de manejar la lógica de los subsystemas de manera simple, delegando las peticiones a cada subsistema.
    public class Facade : MonoBehaviour
    {
        protected Subsystem1 _subsystem1;
        protected Subsystem2 _subsystem2;

        public Facade(Subsystem1 subsystem1, Subsystem2 subsystem2)
        {
            _subsystem1 = subsystem1;
            _subsystem2 = subsystem2;
        }

        //Este metodo seria como un atajo a los métodos más complicados de cada subsistema.
        public string Operation()
        {
            string result = "Combinacion de subsistemas 1 y 2 :";
            result += _subsystem1.Operation1();
            result += _subsystem2.Operation2();
            return result;
        }

        //Esto realmente se haria desde el constructor, es decir creariamos los subsistemas en otro sitio y construiriamos una fachada injectandolos
        private void Start()
        {
            _subsystem1 = new Subsystem1();
            _subsystem2 = new Subsystem2();

            Debug.Log(Operation());
        }
    }
}
