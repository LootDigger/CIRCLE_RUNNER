using System;

namespace AUL.Core
{
  public interface IGameModel
  {
    void SubscribeScoreModelChange(Action<int> handler);
    void SubscribeDistanceModelChange(Action<float> handler);
    
    int GetScoreModelValue();
    float GetDistanceModelValue();
    
    void UpdateScoreModel(int newScore);
    void IncrementScore();
    void UpdateDistanceModel(float newDistanceDelta);
  }
}