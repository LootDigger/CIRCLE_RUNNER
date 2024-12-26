using System;
using UniRx;
using UnityEngine;

namespace AUL.PlayerInput
{
    public class DragInputListener : IDragListener, IDisposable
    {
        private Subject<Vector2> _onTouchDragged = new();
        private CompositeDisposable _disposable = new();

        public DragInputListener()
        {
            DragStreamInit();
        }

        private void DragStreamInit()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButton(0))
                .Select(_ => Input.mousePosition)
                .DistinctUntilChanged()
                .Subscribe(mousePos => { _onTouchDragged.OnNext(mousePos); }).AddTo(_disposable);
        }

        public void SubscribeDragEvent(Action<Vector2> dragHandler)
        {
            _onTouchDragged.Subscribe(dragHandler);
        }

        public void Dispose()
        {
            _onTouchDragged?.Dispose();
            _disposable?.Dispose();
        }
    }
}