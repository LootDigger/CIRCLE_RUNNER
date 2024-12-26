using System.Collections.Generic;
using UnityEngine;

public struct Path
{
   public List<Vector3> _points;
   
   public void AddPoint(Vector3 point)
   {
      if(_points == null)
         _points = new List<Vector3>();
      
      _points.Add(point);
      
#if UNITY_EDITOR
      DrawPath();
#endif
   }

   public float GetRelativeLength(Vector3 playerPosition)
   {
      if (_points == null || _points.Count == 0) return 0f;
      float initDistance = Vector3.Distance(playerPosition, _points[0]);
      float pathLenth = GetLength();
      return initDistance + pathLenth;
   }
   
   private float GetLength()
   {
      float distance = 0f;
      if (_points == null || _points.Count <= 1) return distance;
      
      for (int i = 0; i < _points.Count - 1; i++)
      {
         distance += Vector3.Distance(_points[i], _points[i + 1]);
      }
      return distance;
   }
   
#if UNITY_EDITOR

   private void DrawPath()
   {
      for (int i = 0; i < _points.Count - 1; i++)
      {
         Debug.DrawLine(_points[i], _points[i + 1], Color.red, 10f);
      }
   }
#endif
}
