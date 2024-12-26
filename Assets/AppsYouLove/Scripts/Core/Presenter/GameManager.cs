namespace AUL.Core
{
    public class GameManager
    {
        private readonly IGameModel _gameModel;
        private readonly IView _uiManager;
        
        public GameManager(IView uiManager, IGameModel gameModel)
        {
            _uiManager = uiManager;
            _gameModel = gameModel;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _gameModel.SubscribeScoreModelChange(OnScoreChanged);
            _gameModel.SubscribeDistanceModelChange(OnTravelledDistanceChanged);
        }
        
        private void OnEnemyCollision()
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