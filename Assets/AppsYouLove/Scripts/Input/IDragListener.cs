using System;
using UnityEngine;

public interface IDragListener
{
  void SubscribeDragEvent(Action<Vector2> dragHandler);
}
