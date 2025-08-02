using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardGrid.UI.Extensions;
using CardGrid.UI.Utilities;
using Systems.ReferenceResolver;
using UnityEngine;
using UnityEngine.UI;

namespace CardGrid.UI
{
    public class PlayCardsWidget : MonoBehaviour
    {
        #region Public Variables

        [Header("Buttons Group")]
        [SerializeField] private HoldClickableButton visibilityButton;
        [SerializeField] private Button              playCardsButton;
        
        [Header("Card Containers")]
        [SerializeField] private GridDropHandler actionsContainer,artfulnessContainer,discardContainer;

        #endregion

        #region Private Variables

        [ResolveReference]private HandCardsWidget        _handCardsWidget;

        private CanvasGroup   _canvasGroup;
        
        #endregion
        
        #region Unity Life Cycle

        private void Awake()
        {
            FillReferences();
            PreInitialize();
        }

        private void OnEnable()
        {
            Card.OnDraggingCard            += ManagePlayerWidgetVisibility;
            GridDropHandler.OnCardsUpdated += UpdateCards;
            visibilityButton.OnHoldClicked += HidePlayerWidget;
            visibilityButton.OnStopHolding += ShowPlayerWidget;
            
            playCardsButton .onClick.AddListener(PlayCards);
        }

        private void OnDisable()
        {
            Card.OnDraggingCard            -= ManagePlayerWidgetVisibility;
            GridDropHandler.OnCardsUpdated -= UpdateCards;
            visibilityButton.OnHoldClicked -= HidePlayerWidget;
            visibilityButton.OnStopHolding -= ShowPlayerWidget;
            
            playCardsButton .onClick.RemoveListener(PlayCards);
        }

        #endregion

        #region Utility Methods

        private void FillReferences() => TryGetComponent(out _canvasGroup);

        private void PreInitialize() => _canvasGroup.alpha = 0;

        private void ManagePlayerWidgetVisibility(object sender, bool isDragging)
        {
            if(!isDragging && !IsPlayingCards()) HidePlayerWidget();
            else ShowPlayerWidget();
        }

        private void ShowPlayerWidget()
        {
            StopAllCoroutines();
            StartCoroutine(_canvasGroup.ManageCanvasVisibility(true));
        }

        private void HidePlayerWidget()
        {
            StopAllCoroutines();
            StartCoroutine(_canvasGroup.ManageCanvasVisibility(false));
        }
        
        private async void PlayCards() 
        {
            throw new NotImplementedException();
		}
        private void UpdateCards(object sender, GridDropHandler e)
        {
            throw new NotImplementedException();
        }
        private bool IsPlayingCards() => actionsContainer.Cards.Count > 0 || artfulnessContainer.Cards.Count > 0;

        #endregion
    }
}
