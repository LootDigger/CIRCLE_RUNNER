using UniRx;
using UnityEngine;
using Zenject;

namespace AUL.PlayerInput
{
    public class InputManager : MonoBehaviour, IInputService
    {
        private ITouchListener _touchListener;
        private IDragListener _dragListener;

        private readonly PathBuilder _pathBuilder = new();
        private readonly ReactiveCommand<Path> _movePlayerCommand = new();

        // TODO: Make Wrapper with queue 
        public ReactiveCommand<Path> MovePlayerCommand => _movePlayerCommand;

        [Inject]
        public void Init(ITouchListener touchListener, IDragListener dragListener)
        {
            _touchListener = touchListener;
            _dragListener = dragListener;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _touchListener.SubscribeTouchBegan(OnTouchBeganHandler);
            _touchListener.SubscribeTouchEnd(OnTouchEndedHandler);
            _dragListener.SubscribeDragEvent(OnDragEventHandler);
        }

        #region Input Handling

        private void OnTouchBeganHandler(Vector2 position)
        {
            _pathBuilder.CreateNewPath();
        }

        private void OnTouchEndedHandler(Unit obj)
        {
            Debug.Log("_movePlayerCommand.Execute");
            _movePlayerCommand.Execute(_pathBuilder.GetPath());
        }

        private void OnDragEventHandler(Vector2 screenCoord)
        {
            _pathBuilder.CreatePathPoint(screenCoord);
        }

        #endregion
    }
}
