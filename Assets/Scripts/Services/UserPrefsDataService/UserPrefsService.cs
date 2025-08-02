using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Services.UserPrefsDataService
{
    public class UserPrefsService : IUserPrefsService
    {
        // If we were interested in increasing security for whatever reason, we could move part or all of the process to the server.
        private const string Key = "A9OIX1L4XIxcyGbKuirVnXH+NRVJS3cCvwEYDTw40Ec=";
        private const string Iv = "TKFajfxQXZYNRGaNQ5a0jQ==";

        public bool SaveData<T>(string key, T data, bool encrypted = true)
        {
            var path = $"{Application.persistentDataPath}/{key}.json";
            
            try
            {
                if (File.Exists(path)) File.Delete(path);
                using var stream = File.Create(path);
                if (encrypted)
                {
                    WriteEncryptedData(data, stream);
                }
                else
                {
                    stream.Close();
                    File.WriteAllText(path, JsonUtility.ToJson(data));
                }

                return true; 
            }
            catch (Exception e)
            {
                Debug.LogError($"Error while saving data to {path}! {e.Message} {e.StackTrace}");
                return false;
            }
        }

        public T LoadData<T>(string key, bool encrypted = true)
        {
            var path = $"{Application.persistentDataPath}/{key}.json";

            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} does not exist!");

            try
            {
                return encrypted ? ReadEncryptedData<T>(path) : JsonUtility.FromJson<T>(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                Debug.LogError($"Error while loading data from {path}! {e.Message} {e.StackTrace}");
                throw;
            }
        }
        public void DeleteData(string key)
        {
            var path = $"{Application.persistentDataPath}/{key}.json";
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log(GetType().Name + ": '" + key + "' data deleted");
                return;
            }
            Debug.LogWarning(GetType().Name + ": data with key "+"'" +key+ "' does not exists");
        }
        
        public bool ExistsData(string key)
        {
            var path = $"{Application.persistentDataPath}/{key}.json";
            return File.Exists(path);
        }

        private static void WriteEncryptedData<T>(T data, Stream stream)
        {
            using var aesProvider = Aes.Create();
            aesProvider.Key = Convert.FromBase64String(Key);
            aesProvider.IV = Convert.FromBase64String(Iv);
            using var cryptoTransform = aesProvider.CreateEncryptor();
            using var cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write);
            
            cryptoStream.Write(Encoding.ASCII.GetBytes(JsonUtility.ToJson(data)));
        }
        
        private static T ReadEncryptedData<T>(string path)
        {
            var fileBytes = File.ReadAllBytes(path);
            using var aesProvider = Aes.Create();
            aesProvider.Key = Convert.FromBase64String(Key);
            aesProvider.IV = Convert.FromBase64String(Iv);
            
            using var cryptoTransform = aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV);
            using var memoryStream = new MemoryStream(fileBytes);
            using var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            
            return JsonUtility.FromJson<T>(streamReader.ReadToEnd());
        }
    }
}
