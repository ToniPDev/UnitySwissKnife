using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace Mediator
{
    /// <summary>
    /// Clase de la cual ha de heredar el jugador para funcionar como puente mediante sus módulos y resto de
    /// servicios de Zenject (Optimización con assemblies de Unity). Esta clase se encarga de Injectar, inicializar y
    /// actualizar módulos del jugador.
    /// </summary>
    public abstract class PlayerMediator : MonoBehaviour
    {
        #region Property
        
        public int    ID { get; set; }
        public string UserName { get; set; }
        public T      CastToPlayer<T>() where T : PlayerMediator => this as T;

        #endregion

        #region Protected Variables

        [ReadOnly] protected List<IModule> Modules = new();
        [ReadOnly] protected bool          ModulesInitialized;

        #endregion

        #region Loop Cycle

        protected void InitModules()
        {
            foreach (var module in Modules.Where(module => module != null)) module.Init(this);
            ModulesInitialized = true;
        }
        
        protected void DisposeModules()
        {
            foreach (var module in Modules.Where(module => module != null)) module.Dispose();
        }

        protected void UpdateModules()
        {
            /*for(int i = 0; i < Modules.Count; ++i)
            {
                var module = Modules[i];
                if (Modules[i] == null) continue;
                Modules[i].Tick();
            }*/
        }

        #endregion

        #region Getters

        public virtual T GetModule<T>() where T : class, IModule => 
            Modules.Where(module => module != null).FirstOrDefault(module => module.GetType() == typeof(T) 
                                   || module.GetType().IsSubclassOf(typeof(T)) 
                                   || module.GetType().GetInterfaces().Any(i => i == typeof(T))) as T;

        #endregion
    }
}

