using BoardSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class TileView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private BoardView _boardView;

        [SerializeField]
        private UnityEvent OnActivate;

        [SerializeField]
        private UnityEvent OnDeactivate;


        public Position GridPosition => PositionHelper.GridPosition(transform.position);

        private void Start()
        {
            _boardView = FindObjectOfType<BoardView>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                _boardView.OnTileViewEnter(this);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            _boardView.OnPositionViewDrop(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                _boardView.OnTileViewExit(this);
            }
        }

        private void Awake()
        {
            var pos = PositionHelper.GridPosition(transform.position);
            transform.position = PositionHelper.WorldPosition(pos);
        }

        internal void Deactivate()
               => OnDeactivate?.Invoke();

        internal void Activate()
            => OnActivate?.Invoke();
    }
}
