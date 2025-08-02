using UnityEngine;
using UnityEngine.UI;

namespace CardGrid
{
    public abstract partial class Card : MonoBehaviour
    {
        #region Private Variables

        protected abstract Card LinkedCard { get; }

        private Image _image;
        [SerializeField] private Image ownDropContainerImage;

        #endregion

        #region Public Variables

        public  CardDataSo cardDataSo;

        #endregion
        
        #region Unity Life Cycle
        private void Awake() => FillHandlerReferences();

        private void Start() => Initialize();

        private void LateUpdate() => MoveCard();

        #endregion

        #region Utility Methods

        protected abstract void CardClicked();

        protected abstract void CardBeginDragged();
        
        protected abstract void CardEndedDragged();
        
        protected abstract void CardForcedToChangeContainer();

        #endregion
    }
}