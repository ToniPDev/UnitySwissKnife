using UnityEngine;

namespace PatronesDeComportamiento.Mediator
{
    //Implementa la coordinacion de eventos
    public class EnemyMediator : IMediator
    {
        private Orc _orc;
        private Elf _elf;

        public EnemyMediator(Orc orc, Elf elf)
        {
            _orc = orc;
            _elf = elf;

            _orc.SetMediator(this);
            _elf.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "Attack")
            {
                Debug.Log("Orc attacked");
                _elf.ReceiveDmg();
            }
            if (ev == "Sleep")
            {
                Debug.Log("Elf went to sleep");
                _orc.DoYawn();
            }
        }
    }
}