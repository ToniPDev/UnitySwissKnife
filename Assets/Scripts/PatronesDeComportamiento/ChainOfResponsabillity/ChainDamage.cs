using System;
using System.Collections.Generic;
using UnityEngine;

namespace PatronesDeComportamiento.ChainOfResponsabillity
{
    public class ChainDamage : MonoBehaviour
    {
        public TipodeAtaque attackType;
        public enum TipodeAtaque
        {
            Fuego,
            Hielo,
            Normal
        }
        private void Start()
        {
            var orc = new OrcDmgReceiver();
            var fireOrc = new FireOrcDmgReceiver();
            var iceOrc = new IcedOrcDmgReceiver();

            orc.SetNext(fireOrc).SetNext(iceOrc);

            Debug.Log(orc.Handle(GetAttack()));
        }

        private string GetAttack()
        {
            return attackType switch
            {
                TipodeAtaque.Fuego => "fuego",
                TipodeAtaque.Hielo => "hielo",
                TipodeAtaque.Normal => "normal",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}