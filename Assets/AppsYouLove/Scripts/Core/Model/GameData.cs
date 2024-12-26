using System;
using AUL.Services;

namespace AUL.Core
{
    public class GameData : IGameModel, IDisposable
    {
        private readonly IDataService _dataService;
        private readonly AbstractModel<int> _scoreModel;
        private readonly AbstractModel<float> _distanceModel;
        private PlayerData _playerData;
        
        public GameData(IDataService dataService)
        {
            _dataService = dataService;
            _scoreModel = new ScoreModel();
            _distanceModel = new TravelDistanceModel();
            LoadPlayerData();
        }

        private void LoadPlayerData()
        {
            _playerData = _dataService.LoadPlayerData();
            _scoreModel.UpdateValue(_playerData.ScoreValue);
            _distanceModel.UpdateValue(_playerData.DistanceValue);
        }
        
        public void SubscribeScoreModelChange(Action<int> handler)
        {
            _scoreModel.SubscribeModelChange(handler);
        }

        public void SubscribeDistanceModelChange(Action<float> handler)
        {
            _distanceModel.SubscribeModelChange(handler);
        }
        
        public int GetScoreModelValue() => _scoreModel.Value;

        public float GetDistanceModelValue() => _distanceModel.Value;

        public void UpdateScoreModel(int newScore)
        {
            _scoreModel.UpdateValue(newScore); 
        }

        public void IncrementScore()
        {
            int score = _scoreModel.Value;
            UpdateScoreModel(++score);
        }

        public void UpdateDistanceModel(float newDistanceDelta)
        {
            float distance = _distanceModel.Value;
            distance += newDistanceDelta;
            _distanceModel.UpdateValue(distance); 
        }

        public void Dispose()
        {
            _dataService.SavePlayerData(_playerData);
        }
    }
}