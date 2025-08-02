using System;
using System.Collections;
using CardGrid.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGrid
{
    public partial class Card : IPointerEnterHandler,IPointerClickHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        #region Private Variables

        private bool            _dragging;
        private bool            _pointerEntered;
        private bool            _isRotating;
        private GameObject      _dummy;

        #endregion
        
        #region Events

        public static event EventHandler<bool> OnDraggingCard;
        public static event EventHandler<GridDropHandler> OnCardDropped;

        #endregion
        
        #region Properties
        public Transform       ParentAfterDrag { get; set; }
        public GridDropHandler Container { get; private set; }
        public GridDropHandler OwnContainer { get; private set; }

        #endregion

        #region References

        private void FillHandlerReferences()
        {
            TryGetComponent(out _image);
            Container    = GetComponentInParent<GridDropHandler>();
            OwnContainer = ownDropContainerImage.GetComponentInParent<GridDropHandler>();
        }

        #endregion

        #region Initialization

        void Initialize() => SetInteractable(false);

        #endregion

        #region Ticks

        private void MoveCard()
        {
            if(!_dragging) return;
            transform.position = Vector3.Lerp(transform.position, Input.mousePosition, 8.5f * Time.deltaTime);
        }

        #endregion

        #region Utility Methods

        public void SetInteractable(bool interactable)
        {
            _image.raycastTarget = interactable;
            ownDropContainerImage.raycastTarget = interactable;
        }

        protected IEnumerator RotateCard(Vector3? finalRotation,Action beforeRotate = null,Action afterRotate = null)
        {
            if(_isRotating) yield break;
            
            beforeRotate?.Invoke();
            
            _isRotating = true;
            
            var startRotation = transform.rotation.eulerAngles;
            var endRotation = finalRotation ?? startRotation + new Vector3(0, 0, 90);
            
            var elapsedTime = 0f;
            var duration = 0.25f; // duración de la rotación en segundos

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                var progress = elapsedTime / duration;
        
                // Interpolación suave de la rotación
                transform.rotation = Quaternion.Euler(
                    Vector3.Lerp(startRotation, endRotation, progress)
                );
        
                yield return null;
            }

            // Aseguramos que termine exactamente en la rotación deseada
            transform.rotation = Quaternion.Euler(endRotation);
            
            _isRotating = false;
            
            afterRotate?.Invoke();
        }

        #endregion
        
        #region IHandlers Implementation

        public void OnBeginDrag(PointerEventData eventData)
        {
            CardBeginDragged();
            
            //Al empezar a dragear indico que he hecho un pointer exit para no sustituir el hovering del anterior elemento
            OnPointerExit(null);
            
            //Actualizo cartas del grid
            Container.RemoveCards(this);
            Container.UpdateCards();
            
            //Empiezo a draggear
            _dragging = true;
            OnDraggingCard?.Invoke(this, _dragging);
            
            //Guardo el padre actual
            ParentAfterDrag = transform.parent;

            //Creo un dummy que sirve de clon del objeto a arrastrar y lo meto como padre del grid
            _dummy = new GameObject("DummyCard");
            _dummy.transform.SetParent(ParentAfterDrag);
            
            //Guardo el dummy como objeto arrastrado por el grid
            Container.ObjectBeingDragged = _dummy;
            
            //Lo saco fuera de la jerarquía y lo coloco último
            transform.SetParent(transform.root);
            SetInteractable(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Do nothing
            // Apparently this interface needs to exist in order for BeginDrag and EndDrag to work,
            // but we don't actually have anything to do here
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Dejo de arrastrar
            _dragging = false;
            
            //Seteo su nuevo padre, si se ha dropeado en un GridDropHandler, este se ha modificado si no es el anterior
            //El parent after drag se setea al inicio del drag y en el drop del GridHandler.cs
            transform.SetParent(ParentAfterDrag);
            
            //Si el dummy de este container es el mio lo seteo a null
            if (Container.ObjectBeingDragged == _dummy) Container.ObjectBeingDragged = null;
            
            //Si ha cambiado el parent, actualizo el Container
            var destination = transform.parent.GetComponent<GridDropHandler>();

            if (destination != null && Container != destination)
            {
                Container = destination;
                
                OnCardDropped?.Invoke(this,Container);
            }
            else SetInteractable(true);
            
            //Actualizo mi índice para reemplazar posición de dummy y lo elimino
            transform.SetSiblingIndex(_dummy.transform.GetSiblingIndex());
            Destroy(_dummy);
            
            //Actualizo cartas del grid
            Container.AddCards(this);
            Container.UpdateCards();
            
            CardEndedDragged();
            OnDraggingCard?.Invoke(this, _dragging);
        }
        
        public void SetGridDropHandler(GridDropHandler destination, bool setInteractable)
        {
            CardForcedToChangeContainer();
            SetInteractable(setInteractable);

            //Quito la carta del container actual y la añado al del destino
            Container.RemoveCards(this);
            destination.AddCards(this);
            
            LinkedCard?.SetGridDropHandler(destination, setInteractable);
            
            transform.SetParent(destination.transform);
            Container = destination;
        }
        
        //Al hacer click en la carta la giro
        public void OnPointerClick(PointerEventData eventData)
        {
            //Llamo a cardclicked para que sus hijos overrideen el metodo, y despues llamo a actualizar sus posiciones
            CardClicked();
        }

        //Se llama al pasar el ratón sobre la carta
        public void OnPointerEnter(PointerEventData eventData)
        {
            //Si estoy arrastrando algo al hacer pointer enter
            if (eventData.pointerDrag != null) PointerEnterWhileDragging(eventData);
            else PointerEnterWithoutDragging();
        }
        
        public void OnPointerExit(PointerEventData eventData) => Container.Hovering(false,transform.GetSiblingIndex());

        private void PointerEnterWhileDragging(PointerEventData eventData)
        {
            //Recojo el Dummy de la carta que estoy moviendo
            var dummy = eventData.pointerDrag.GetComponent<Card>()._dummy;

            //Si el padre de ese dummy es diferente al mio lo cambio
            if (dummy != null && dummy.transform.parent != eventData.pointerEnter.transform.parent)
            {
                dummy.transform.SetParent(eventData.pointerEnter.transform.parent);
                Container.ObjectBeingDragged = dummy;
            }
            
            var objectBeingDragged = Container.ObjectBeingDragged;
            
            //Si el dummy no es nulo y tampoco es igual al cacheado cambio su índice
            if (objectBeingDragged != null && objectBeingDragged != _dummy) 
                objectBeingDragged.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }

        private void PointerEnterWithoutDragging() => Container.Hovering(true,transform.GetSiblingIndex());

        #endregion
    }
}