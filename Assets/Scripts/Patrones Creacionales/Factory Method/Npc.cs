using System;
using UnityEngine;

namespace Patrones_Creacionales.Factory_Method
{
    public class Npc : MonoBehaviour
    {
        public NpcType npcType;
        public enum NpcType
        {
            Info,
            Affinity
        }
        private void Start() => Initializator(CreatorType());

        private NpcCreator CreatorType()
        {
            return npcType switch
            {
                NpcType.Affinity => new NpcAffinityNpcCreator(),
                NpcType.Info => new NpcInfoNpcCreator(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private void Initializator(NpcCreator creator) => creator.NpcInstantiator();
    }
}
