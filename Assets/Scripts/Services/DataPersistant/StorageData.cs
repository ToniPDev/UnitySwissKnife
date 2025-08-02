using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.DataPersistant
{
    /// <summary>
    /// Scriptable object que almacena los datos del objeto a persistir.
    /// </summary>
    [CreateAssetMenu(fileName = "StorageData", menuName = "ScriptableObjects/StorageData")]
    public class StorageData : ScriptableObject
    {
        public List<Storage> PersistantStorages;
        
        [Serializable]
        public class Storage
        {
            public string                 storageName;
            public bool                   uploadFile;
            
            //Lista de referencias para cachear dada la imposibilidad de unity para guardar objetos complejos.
            public List<Object>           cachedReferences = new();
        }
    }
}