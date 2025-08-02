using System;
using System.Collections.Generic;
using System.Linq;
using CardGrid.Types;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGrid.UI
{
    public class GridDropHandler: MonoBehaviour, IDropHandler
    {
        #region Public variables

        [Header("Cards acceptable values")]
        [Tooltip("Width of the elements inside the grid")]
        public float spacing = 150f;
        
        [Header("Cards Space")]
        [Tooltip("Accepted type of cards")]
        public CardType matchingType;
        
        [Tooltip("Make card interactable after drop")]
        public bool interactableAfterDrop = true;

        [Header("Text Container and properties")]
        [SerializeField] [CanBeNull] private TextMeshProUGUI gridTextAmount;

        [Space] [Tooltip("Max Amount of cards in grid")]
        public int maxAmount = 5;
        public int maxCardsDraggedByUser = 5;
        
        [Space]
        public bool checkMaxCardsDraggedByUser;
        
        #endregion  
        
        #region Properties
        public GameObject ObjectBeingDragged { get; set; }

        public HashSet<Card> Cards { get; private set; } = new();

        #endregion

        #region Private Variables

        private readonly Dictionary<int,bool> _hoveredCards = new();
        private int                           _cardsDraggedByUser;

        #endregion
        
        #region Events
        public static event EventHandler<GridDropHandler> OnCardsUpdated;
        
        #endregion
        
        #region Unity Life Cycle

        private void LateUpdate() => UpdateLayout();

        #endregion

        #region Drop Handler

        public void OnDrop(PointerEventData eventData)
        {
            // Verificamos si el objeto arrastrado es una carta
            var card = eventData.pointerDrag.GetComponent<Card>();
            
            if(checkMaxCardsDraggedByUser && _cardsDraggedByUser >= maxCardsDraggedByUser) return;
            
            if(!CanDrop(card)) return;

            card.ParentAfterDrag = transform;
            card.SetInteractable(interactableAfterDrop);

            _cardsDraggedByUser++;
        }

        #endregion

        #region Utility Methods

        public bool CanDrop(Card card,bool ignoreMaxAmount = false)
        {
            if(!card) return false;
            if((matchingType & card.cardDataSo.type) == 0) return false;
            if(ignoreMaxAmount) return true;
            return Cards.Count < maxAmount;
        }
        
        public void Hovering(bool hovering, int siblingIndex)
        {
            if(_hoveredCards.ContainsKey(siblingIndex)) _hoveredCards[siblingIndex] = hovering;
            _hoveredCards.TryAdd(siblingIndex, hovering);
        }

        private void UpdateLayout()
        {
            var count = transform.childCount;
            if (count == 0) return;
            
            var totalWidth = spacing * (count - 1);
            var startX = -totalWidth / 2f;
            
            //Order Childs to be as close as possible
            for (var index = 0; index < transform.childCount; index++)
            {
                var child = transform.GetChild(index);
                var exists = _hoveredCards.TryGetValue(index, out var hovered);
                var nextPos = new Vector3(startX + index * spacing, exists? hovered? 25 : 0 : 0, 0);
                child.localPosition = Vector3.Lerp(child.localPosition, nextPos, Time.deltaTime * 5f);
            }
        }

        public bool AddCards(Card card)
        {
            var couldBeAdded = Cards.Add(card);
            ReorderCards();
            RefreshText();
            return couldBeAdded;
        }

        public bool RemoveCards(Card card)
        {
            var couldBeRemoved = Cards.Remove(card);
            ReorderCards();
            RefreshText();
            return couldBeRemoved;
        }

        private void ReorderCards() => Cards = new HashSet<Card>(Cards.OrderBy(c => c.transform.GetSiblingIndex()));

        public void UpdateCards() => OnCardsUpdated?.Invoke(this, this);

        public void RefreshText()
        {
            if (gridTextAmount == null) return;
            
            gridTextAmount.text = checkMaxCardsDraggedByUser 
                ? $"{Cards.Count}/{maxAmount} - {_cardsDraggedByUser}/{maxCardsDraggedByUser}" 
                : $"{Cards.Count}/{maxAmount}";
        }

        #endregion
    }
}