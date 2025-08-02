using UnityEngine;

namespace PatronesDeComportamiento.Mediator
{
    public class Orc : EnemyComponent
    {
        protected Orc(IMediator mediator) : base(mediator) { }
        public void Attack() => Mediator.Notify(this,"Attack");
        public void DoYawn() => Debug.Log("Orc yawned");
    }

    public class Elf : EnemyComponent
    {
        protected Elf(IMediator mediator) : base(mediator) { }
        public void Sleep() => Mediator.Notify(this,"Sleep");
        public void ReceiveDmg() => Debug.Log("Elf get hurt");
    }
}