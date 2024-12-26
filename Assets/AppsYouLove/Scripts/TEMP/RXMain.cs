using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RXMain : MonoBehaviour
{
   private readonly Subject<Unit> onMouseClick = new();
   private void Awake()
   {
     // DoubleClickObservable();
     // ContinuesClickObservable();
     DraggableMouseObservable();
   }
   
   private void ClickObservable()
   {
      Observable.EveryUpdate()
         .Where(_=> Input.GetMouseButtonDown(0))
         .Subscribe(_=>
         {
            Debug.Log("Mouse clicked");
         });
   }

   private void DoubleClickObservable()
   {
      var clickStream = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0));
      clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250))).Where(x => x.Count == 2).Subscribe(_ =>
      {
         Debug.Log("Double Click Observed");
      });
   }
   
   private void ContinuesClickObservable()
   {
      var clickStream = Observable.EveryUpdate()
         .Where(_ => Input.GetMouseButton(0))
         .Select(_ => Input.mousePosition)
         .DistinctUntilChanged();
      clickStream.Subscribe(mousePos =>
      {
         Debug.Log("Continues Click at: " + mousePos);
      });
   }
   
   private void DraggableMouseObservable()
   {
      Vector2? previousPosition = null;

      var StartDragging = Observable.EveryUpdate()
         .Where(_ => Input.touchCount > 0)
         .Select(_ => Input.GetTouch(0).position)
         .Where(currentPosition =>
         {
            if (previousPosition.HasValue && Vector2.Distance(previousPosition.Value, currentPosition) < 50f)
            {
               return false;
            }

            previousPosition = currentPosition;
            return true;
         })
         .Subscribe(clickPos =>
         {
            Debug.Log("DraggableMouse Moved " + clickPos);
         });
   }
   
}
