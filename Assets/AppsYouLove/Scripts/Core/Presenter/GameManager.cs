using AUL.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace AUL.Core
{
    public class GameManager : MonoBehaviour
    {
        private EnemyCollisionTracker _enemyCollisionTracker;
        private IGameModel _gameModel;
        private IView _uiManager;
        
        [Inject]
        public void Init(IView uiManager, IGameModel gameModel, EnemyCollisionTracker enemyTracker)
        {
            _uiManager = uiManager;
            _gameModel = gameModel;
            _enemyCollisionTracker = enemyTracker;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _gameModel.SubscribeScoreModelChange(OnScoreChanged);
            _gameModel.SubscribeDistanceModelChange(OnTravelledDistanceChanged);
            _enemyCollisionTracker.EnemyCollisionCommand.Subscribe(OnEnemyCollision);
        }
        
        private void OnEnemyCollision(Unit unit)
        {
            _gameModel.IncrementScore();
        }

        private void OnScoreChanged(int score)
        {
            _uiManager.UpdateScoreView(score);
        }

        private void OnTravelledDistanceChanged(float distance)
        {
            _uiManager.UpdateDistanceTrackerView(distance);
        }
    }
}