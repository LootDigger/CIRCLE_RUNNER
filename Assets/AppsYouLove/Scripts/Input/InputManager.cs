using UniRx;
using UnityEngine;
using Zenject;

namespace AUL.PlayerInput
{
    public class InputManager : MonoBehaviour, IInputService
    {
        private ITouchListener _touchListener;
        private IDragListener _dragListener;
        private PlayerStopper _playerStopper;

        private readonly PathBuilder _pathBuilder = new();
        private readonly ReactiveCommand<Path> _movePlayerCommand = new();
        private readonly ReactiveCommand _stopPlayerCommand = new();
        
        // TODO: Make "Executer" class with queue and stopping current commands logic 
        public ReactiveCommand<Path> MovePlayerCommand => _movePlayerCommand;
        public ReactiveCommand StopPlayerCommand => _stopPlayerCommand;
        
        [Inject]
        public void Init(ITouchListener touchListener, IDragListener dragListener, PlayerStopper playerStopper)
        {
            _touchListener = touchListener;
            _dragListener = dragListener;
            _playerStopper = playerStopper;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _touchListener.SubscribeTouchBegan(OnTouchBeganHandler);
            _touchListener.SubscribeTouchEnd(OnTouchEndedHandler);
            _dragListener.SubscribeDragEvent(OnDragEventHandler);
            _playerStopper.SubscribePlayerClick(OnPlayerClickEventHandler);
        }

        #region Input Handling

        private void OnTouchBeganHandler(Vector2 screenCoords)
        {
            _pathBuilder.CreateNewPath();
        }

        private void OnTouchEndedHandler(Unit obj)
        {
            _movePlayerCommand.Execute(_pathBuilder.GetPath());
        }

        private void OnDragEventHandler(Vector2 screenCoord)
        {
            _pathBuilder.CreatePathPoint(screenCoord);
        }
        
        private void OnPlayerClickEventHandler()
        {
            _stopPlayerCommand.Execute();
        }

        #endregion
    }
}
