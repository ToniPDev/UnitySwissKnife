using System.Collections.Generic;
using System.Linq;
using Systems.ReferenceResolver;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace CardGrid.UI
{
    public class HandCardsWidget : MonoBehaviour,IReferenciable
    {
        #region Private Variables
        
        [Header("Buttons Group")]
        [SerializeField] private Button drawButton;

        [Header("Card Containers")]
        [SerializeField] private GridDropHandler handContainer,discardContainer,deckContainer;
        
        #endregion

        #region Unity Life Cycle

        private void Start()
        {
            FillReferences();
        }

        private void OnEnable()
        {
            drawButton.onClick.AddListener(() => DrawCardsFromDeckInto(handContainer,5));
        }
        private void OnDisable()
        {
            drawButton.onClick.RemoveAllListeners();
        }

        #endregion
        
        #region Utility Methods

        private void FillReferences()
        {
            
        }
        
        private void DrawCardsFromDeckInto(GridDropHandler container,int cardsToDraw)
        {
            if (!CheckIfEnoughCardsIn(deckContainer, cardsToDraw))
            {
                MixCardsFromDiscardContainer();
                return;
            }

            var childs = GetRandomCardsFrom(deckContainer, cardsToDraw);
            
            MoveCardsToContainerFrom(container, deckContainer, childs);
        }

        private void MixCardsFromDiscardContainer()
        {
            var childsSet = new HashSet<int>(Enumerable.Range(0, discardContainer.Cards.Count));
            MoveCardsToContainerFrom(deckContainer,discardContainer,childsSet);
        }

        public void MoveCardsToContainerFrom(GridDropHandler destination, GridDropHandler origin, HashSet<int> childs,
            bool ignoreMaxAmount = false ,bool setInteractable = true, bool updateCards = true)
        {
            var cardsToManage = childs.Select(origin.Cards.ElementAt).ToArray();
            
            foreach (var card in cardsToManage)
            {
                if (!destination.CanDrop(card,ignoreMaxAmount)) continue;
                
                card.SetGridDropHandler(destination,setInteractable);
            }
            
            if(!updateCards) return;
            
            //Al mover cartas al destino, actualizamos la property de Cards del destino y de su nuevo contenedor
            origin.  UpdateCards();
            destination.UpdateCards();
        }

        private bool CheckIfEnoughCardsIn(GridDropHandler container, int cardsToDraw) => 
            container.transform.childCount >= cardsToDraw;

        private HashSet<int> GetRandomCardsFrom(GridDropHandler container, int cardsToDraw)
        {
            var r = new Random();
            var randoms = new HashSet<int>(); // Usamos un HashSet para evitar números duplicados

            while (randoms.Count < cardsToDraw && randoms.Count < container.Cards.Count)
            {
                // Generar un número aleatorio entre 0 y la cantidad de elementos
                randoms.Add(r.Next(0, container.Cards.Count));
            }

            return randoms; // Convertimos el HashSet a una lista antes de devolverlo

        }

        #endregion
    }
}