using System;
using UniRx;
using UnityEngine;

public interface ITouchListener
{
    void SubscribeTouchBegan(Action<Vector2> touchBeganHandler);
    void SubscribeTouchEnd(Action<Unit> touchEndHandler);
}
