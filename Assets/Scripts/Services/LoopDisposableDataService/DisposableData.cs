using System;

namespace Services.LoopDisposableDataService
{
    [Serializable]
    public class DisposableData<T1> where T1 : struct
    {
        protected T1 Parameters;
        public virtual void SetParameters(T1 parameters) => Parameters = parameters;
        public virtual ref T1 GetParams() => ref Parameters;
    }
}