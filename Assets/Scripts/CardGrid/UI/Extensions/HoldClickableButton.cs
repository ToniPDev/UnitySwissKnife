using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGrid.UI.Extensions
{
    public class HoldClickableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float holdDuration;

        public event Action OnStopHolding;
        public event Action OnHoldClicked;

        private bool _isHoldingButton;
        private float _elapsedTime;

        public void OnPointerDown(PointerEventData eventData) => ToggleHoldingButton(true);

        private void ToggleHoldingButton(bool isPointerDown)
        {
            _isHoldingButton = isPointerDown;

            if (isPointerDown)
                _elapsedTime = 0;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ManageButtonInteraction(true);
            ToggleHoldingButton(false);
        }

        private void ManageButtonInteraction(bool isPointerUp = false)
        {
            if (isPointerUp)
            {
                Click();
                return;
            }
            
            if (!_isHoldingButton)
                return;

            _elapsedTime += Time.deltaTime;
            var isHoldClickDurationReached = _elapsedTime > holdDuration;

            if (isHoldClickDurationReached)
                HoldClick();
        }

        private void Click() => OnStopHolding?.Invoke();

        private void HoldClick()
        {
            ToggleHoldingButton(false);
            OnHoldClicked?.Invoke();
        }

        private void Update() => ManageButtonInteraction();
    }
}