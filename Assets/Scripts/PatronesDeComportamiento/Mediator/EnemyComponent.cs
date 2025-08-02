using UnityEngine;

namespace PatronesDeComportamiento.Mediator
{
    public class EnemyComponent : MonoBehaviour
    {
        protected IMediator Mediator;
        protected EnemyComponent(IMediator mediator) => Mediator = mediator;
        public void SetMediator(IMediator mediator) => Mediator = mediator;
    }
}