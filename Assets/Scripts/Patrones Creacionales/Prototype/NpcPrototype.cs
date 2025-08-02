using System;

namespace Patrones_Creacionales.Prototype
{
    public class NpcPrototype
    {
        public string NpcName;
        public NpcIdInfo IdInfo;
        
        public NpcPrototype ShallowCopy()
        {
            return (NpcPrototype) MemberwiseClone();
        }

        public NpcPrototype DeepCopy()
        {
            NpcPrototype clone = (NpcPrototype) MemberwiseClone();
            clone.IdInfo = new NpcIdInfo(IdInfo.IdNumber);
            clone.NpcName = String.Copy(NpcName);
            return clone;
        }
    }
}
