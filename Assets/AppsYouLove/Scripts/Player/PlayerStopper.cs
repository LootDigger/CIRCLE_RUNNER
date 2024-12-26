using System;
using UnityEngine;
using UniRx;

namespace AUL.PlayerInput
{
    public class PlayerStopper : MonoBehaviour
    {
        private Subject<Unit> _onPlayerClicked = new();

        private void Awake()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.touchCount > 0)
                .Select(_ => Input.GetTouch(0))
                .Select(touch => GetTappedGameObject(touch.position))
                .Where(tappedObject => tappedObject == gameObject)
                .Subscribe(_ => _onPlayerClicked.OnNext(Unit.Default))
                .AddTo(this);
        }

        private GameObject GetTappedGameObject(Vector2 touchPosition)
        {
            // There should be some injected Camera logic, cause .main is expensive operation,
            // but I have no time to do it xd

            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.collider.gameObject;
            }

            return null;
        }

        public void SubscribePlayerClick(Action handler)
        {
            _onPlayerClicked.Subscribe(_ => handler?.Invoke());
        }

        private void OnDestroy()
        {
            _onPlayerClicked.Dispose();
        }
    }
}