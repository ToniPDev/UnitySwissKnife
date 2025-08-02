using System.IO;
using ObjectManagement.Scripts;
using UnityEngine;

namespace Services.SaveManagerService
{
    public class SaveManagerService : MonoBehaviour
    {
        private string _savePath;

        private void Awake () 
        {
            _savePath = Path.Combine(Application.persistentDataPath, "levelsData");
        }

        public void Save (ISaver o)
        {
            using var writer = new BinaryWriter(File.Open(_savePath, FileMode.Create));
            o.Save(new GameDataWriter(writer));
        }

        public void Load (ISaver o)
        {
            using var reader = new BinaryReader(File.Open(_savePath, FileMode.Open));
            o.Load(new GameDataReader(reader));
        }
    }
}
