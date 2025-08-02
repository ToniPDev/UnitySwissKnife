using UnityEngine;

namespace Patrones_Creacionales.Prototype
{
    public class PrototypeInstanciator : MonoBehaviour
    {
        private NpcPrototype _prototype;
        private NpcPrototype _clone;
        private void Start()
        {
            var npc = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        
            _prototype = new NpcPrototype
            {
                NpcName = "Agustín",
                IdInfo = new NpcIdInfo(1805)
            };
        
            Debug.Log("name = " + _prototype.NpcName + " id = " + _prototype.IdInfo.IdNumber);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _clone == null)
            {
                var npc = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                _clone = _prototype.ShallowCopy();
                npc.transform.position = new Vector3(1, 0, 0);
            
                Debug.Log("name = " + _clone.NpcName + " id = " + _clone.IdInfo.IdNumber);
            }

            if (!Input.GetMouseButtonDown(1)) return;
        
            _prototype.IdInfo.IdNumber = 0250;
            _prototype.NpcName = "Roberto";
            Debug.Log("First Npc Id And Name has changed");
            Debug.Log("name = " + _prototype.NpcName + " id = " + _prototype.IdInfo.IdNumber);
            Debug.Log("name = " + _clone.NpcName + " id = " + _clone.IdInfo.IdNumber);
        }
    }
}
