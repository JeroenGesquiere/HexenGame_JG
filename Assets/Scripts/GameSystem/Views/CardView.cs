using HexGameSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class CardView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private CanvasGroup _canvasGroup;
        private BoardView _boardView;

        private GameObject _copy;

        public CardType Type;

        private void Awake()
        {
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
            _boardView = FindObjectOfType<BoardView>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _copy = Instantiate(this.gameObject, _canvasGroup.transform);

            _canvasGroup.blocksRaycasts = false;
            _boardView.DroppedCard = this;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _copy.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            _boardView.DroppedCard = null;
            Destroy(_copy);
        }
    }
}
