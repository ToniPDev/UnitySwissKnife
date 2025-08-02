using System;
using System.Threading;
using UnityEngine;

namespace Threads
{
    /// <summary>
    /// TODO: Fill class description
    /// </summary>  
    public class StartThread : MonoBehaviour
    {
        #region Dependency Injection
        
        #endregion
        
        #region Private Variables

        private Thread _thread;

        #endregion
        
        #region Public Variables
        
        #endregion
        
        #region Events

        Action DoThings => DoStuff;

        #endregion
        
        #region Unity LifeCycle

        private void Start() => _thread.Start();

        #endregion

        #region Utility Methods

        public void SetThread()
        {
            _thread = ThreadStructure.SetThread(AfterFinish, DoThings);
        }

        private void AfterFinish() => print("AFTER FINISH");

        private void DoStuff() => print("DO THINGS");

        #endregion
    }
}
