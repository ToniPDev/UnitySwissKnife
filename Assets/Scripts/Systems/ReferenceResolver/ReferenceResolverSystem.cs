using System.Linq;
using System.Threading.Tasks;
using Extensions.EnumerableExtensions;
using UnityEngine;

namespace Systems.ReferenceResolver
{
    /// <summary>
    /// Singleton that contains all the IReferenciables on the scene cached when initialized.
    /// </summary>
    public sealed class ReferenceResolverSystem
    {
        #region Private References

        private IReferenciable[] _references; 

        #endregion
        
        #region Singleton

        private static ReferenceResolverSystem _instance;

        private static bool _isInitialized;

        public static async Task GetInstanceAsync()
        {
            if (_instance != null) return;

            _instance = new ReferenceResolverSystem();
            
            await _instance.FillReferences(); // Esperamos a que termine
            
            _isInitialized = true;
        }

        public static ReferenceResolverSystem Instance
        {
            get
            {
                if (!_isInitialized)
                    Debug.LogWarning("ReferenceResolverSingleton was accessed before being initialized!");
                return _instance;
            }
        }

        #endregion

        #region Initialization
        
        /// <summary>
        /// Loops over all elements in scene and caches all the IReferenciables
        /// </summary>
        private async Task FillReferences()
        {
            // Ahora encontramos todos los objetos que implementan IReferenciable
            _references = Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include,FindObjectsSortMode.InstanceID)
                .Where(x => x is IReferenciable)  // Filtramos por la interfaz IReferenciable
                .Cast<IReferenciable>()           // Convertimos el resultado a IReferenciable
                .ToArray();

            await Task.Yield();
        }

        #endregion

        #region Getters


        /// <summary>
        /// Method used for obtain the reference from the cached array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetReference<T>() where T : class, IReferenciable =>  _references.ReferenciableBinarySearch<T>(0, _references.Length - 1);

        #endregion
       
    }
}
