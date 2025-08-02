using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Services.DataPersistant
{
    /// <summary>
    /// Servicio para persistencia de datos en binario con el uso de BinaryWriter.
    /// Actualizado para poder almacenar series de objetos en mismo fichero.
    /// </summary>
    public class DataManagementService : BaseService
    {
        [SerializeField] private StorageData storageData;
        
        //Url del servido donde almacenar los datos
        private const string UpdateUrl = "https://dev.sfs.gamergyworld.es/";
        private string _httpUploadURI;

        //Diccionario de objetos que contienen datos de ficheros que se desean persistir al inicializar el servicio.
        public Dictionary<string, PersistantStorage> PersistantStorage { get; private set; }
        
        protected override void PreInitializeInternal()
        {
            PersistantStorage = new Dictionary<string,PersistantStorage>();
            
            foreach (var storage in storageData.PersistantStorages)
            {
                var saveFile = Path.Combine(Application.persistentDataPath, storage.storageName);
                var persistentStorage = new PersistantStorage(saveFile,storage.storageName,storage.uploadFile,storage.cachedReferences);

                PersistantStorage.Add(storage.storageName, persistentStorage);
                
            }
        }

        protected override void InitializeInternal() { }

        protected override void SubscribeToEventsInternal() { }

        protected override void UnsubscribeToEventsInternal() { }

        protected override void TickInternal(float deltaTime) { }

        protected override void DisposeInternal() { /*Send stored data to server*/ }

        private bool PersistentStorageIsCreated(string dataName) => 
            PersistantStorage.TryGetValue(dataName,out var value) && File.Exists(value.SavePath);

        /// <summary>
        /// Llamada realizada al servicio para guardar un objeto.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="storage"></param>
        /// <param name="version"></param>
        /// <param name="mode"></param>
        public void Save(IPersistable o, string storage ,int version, FileMode mode = FileMode.OpenOrCreate)
        {
            try
            {
                PersistantStorage.TryGetValue(storage, out var storageFile);
               
                storageFile?.Save(o, version, mode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Llamada realizada al servicio para cargar un objeto.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="storage"></param>
        /// <returns>Indica si ha sido posible cargar el archivo </returns>
        public bool TryLoad(IPersistable o, string storage)
        {
            if (!PersistentStorageIsCreated(storage)) return false;

            try
            {
                PersistantStorage.TryGetValue(storage, out var storageFile);

                storageFile?.Load(o);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool TryDelete(string storage)
        {
            if (!PersistentStorageIsCreated(storage)) return false;
            
            try
            {
                PersistantStorage.TryGetValue(storage, out var storageFile);

                storageFile?.Delete();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}