using System;
using UniRx;
using UnityEngine;

namespace AUL.PlayerInput
{
    public class TouchInputListener : ITouchListener, IDisposable
    {
        private Subject<Vector2> _onTouchBegan = new();
        private Subject<Unit> _onTouchEnded = new();
        private CompositeDisposable _disposable = new();

        public TouchInputListener()
        {
            TouchStreamInit();
        }

        private void TouchStreamInit()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.touchCount > 0)
                .Select(_ => Input.GetTouch(0))
                .Subscribe(touch =>
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            _onTouchBegan.OnNext(touch.position);
                            break;
                        case TouchPhase.Ended:
                            _onTouchEnded.OnNext(Unit.Default);
                            break;
                        case TouchPhase.Canceled:
                            _onTouchEnded.OnNext(Unit.Default);
                            break;

                    }
                }).AddTo(_disposable);
        }

        public void SubscribeTouchBegan(Action<Vector2> touchBeganHandler)
        {
            _onTouchBegan.Subscribe(touchBeganHandler);
        }

        public void SubscribeTouchEnd(Action<Unit> touchEndHandler)
        {
            _onTouchEnded.Subscribe(touchEndHandler);
        }

        public void Dispose()
        {
            _onTouchBegan?.Dispose();
            _onTouchEnded?.Dispose();
            _disposable?.Dispose();
        }
    }
}