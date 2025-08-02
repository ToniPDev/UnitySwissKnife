namespace Services.LoopDisposableDataService
{
   public class LoopDisposableDataService<TDisposableData,TParameters> 
      : BaseService where TDisposableData : DisposableData<TParameters>, new() where TParameters : struct
   {
      #region Private Variables

      protected TDisposableData DataManager;

      #endregion

      #region Utility Methods

      public void ChangeData(TParameters data) => DataManager.SetParameters(data);
      public TDisposableData GetDataManager() => DataManager;
      public ref TParameters GetData() => ref DataManager.GetParams();

      #endregion
   
      #region ZenjectLifecycle
   
      protected override void PreInitializeInternal() { }

      protected override void InitializeInternal()
      {
         DataManager = new TDisposableData();
         ChangeData(new TParameters());
      }

      protected override void SubscribeToEventsInternal() { }

      protected override void UnsubscribeToEventsInternal() { }

      protected override void TickInternal(float deltaTime) { }

      protected override void DisposeInternal() => DataManager = null;

      #endregion
   }
}