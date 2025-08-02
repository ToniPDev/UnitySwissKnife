using System.Collections.Generic;
using System.IO;

namespace Services.DataPersistant 
{
    /// <summary>
    /// Interfaz que deben implementar aquellos objetos que quieren ser capaces de guardar y cargar sus datos.
    /// </summary>
    public interface IPersistable
    {
        #region Properties

        public int      SaveIndex { get; }
        public string   StorageKey { get; set;}
        public long     BytesToSkip { get; }

        #endregion

        #region Methods

        public void Save(DataWriter writer);
        public void Load(DataReader reader);
        
        /// <summary>
        /// Funcion a overridear si es necesario para poder almacenar el numero de bytes que ocupa el objeto en el
        /// fichero de guardado.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual long FillBytesToSkip(IEnumerable<object> data)
        {
            using var tempStream = new MemoryStream();
            using var tempWriter = new BinaryWriter(tempStream);

            var dataWriter = new DataWriter(tempWriter);
            
            dataWriter.Write(data);
            
            return tempStream.Length;
        }
        
        #endregion
    }
}
