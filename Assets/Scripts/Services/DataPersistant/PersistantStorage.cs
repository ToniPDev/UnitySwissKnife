using System.Collections.Generic;
using System.IO;
using Object = UnityEngine.Object;

namespace Services.DataPersistant
{
    /// <summary>
    /// Clase que contenga la logica de gestion del objeto a persistir.
    /// </summary>
    public class PersistantStorage
    {
        public string SavePath { get; private set; }
        public string FileName { get; private set; }

        public bool UploadOnServer { get; private set; }

        public PersistantStorage(string savePath, string fileName, bool uploadOnServer,List<Object> cachedReferences)
        {
            SavePath          = savePath;
            FileName          = fileName;
            UploadOnServer    = uploadOnServer;
            
        }

        /// <summary>
        /// Función que escribe los datos implementados por el objeto persistable mediante un BinaryWriter.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="version"></param>
        /// <param name="mode"></param>
        public void Save(IPersistable o, int version, FileMode mode)
        {
            //TODO El Create hace un nuevo archivo del writer, entonces si persistimos datos desde una clase y queremos
            //concatenar otros de otra clase diferente no podemos vovler a abrir el archivo, tendríamos que utilizar el mismo writer. 
 
            using var writer = new BinaryWriter(File.Open(SavePath, mode, FileAccess.ReadWrite));
            
            //Escribimos siempre la version del writer por si en un futuro se modifican datos tener esto controlado y
            //poder modificarlo sin provocar errores.
            
            writer.Write(-version);
            
            var versionLength = o.FillBytesToSkip(new object[]{-version});

            //Salto en el writer el numero de bytes que quiero escribir Se debería ir con cuidado con no repetir save
            //index para una misma key
            
            writer.BaseStream.Seek((o.SaveIndex * o.BytesToSkip) + versionLength, SeekOrigin.Begin);
            
            o.Save(new DataWriter(writer));
        }

        /// <summary>
        /// Función que escribe los datos implementados por el objeto persistable mediante un BinaryReader.
        /// </summary>
        /// <param name="o"></param>
        public void Load(IPersistable o)
        {
            var data = File.ReadAllBytes(SavePath);
            var reader = new BinaryReader(new MemoryStream(data));

            var version = -reader.ReadInt32();
            
            var versionLength = o.FillBytesToSkip(new object[]{-version});
            
            if(reader.BaseStream.Length <= o.SaveIndex * o.BytesToSkip + versionLength) return;
            
            //Salto en el reader el numero de bytes que quiero leer
            reader.BaseStream.Seek(o.SaveIndex * o.BytesToSkip + versionLength, SeekOrigin.Begin);
            
            o.Load(new DataReader(reader, version));
        }

        public void Delete() => File.Delete(SavePath);
    }
}