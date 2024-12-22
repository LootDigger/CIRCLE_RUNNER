using System;
using UniRx;

namespace AUL.Core
{
    public abstract class AbstractModel<T> : IDisposable
    {
        private ReactiveProperty<T> _data = new();
        private CompositeDisposable _disposables = new();
        
        public T Value => _data.Value;

        public void UpdateValue(T newValue)
        {
            _data.Value = newValue;
        }

        public void SubscribeModelChange(Action<T> handler)
        {
            _data.Subscribe(handler).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}