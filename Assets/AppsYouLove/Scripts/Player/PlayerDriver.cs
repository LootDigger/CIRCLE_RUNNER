using System.Threading;
using AUL.Analytic;
using AUL.Core;
using AUL.Player;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace AUL.PlayerInput
{
    public class PlayerDriver : MonoBehaviour, IDriver
    {
        [Inject] private IInputService _inputManager;
        [Inject] private IDistanceAnalyticService _distanceAnalytic;
        [Inject] private IGameModel _gameModel;
        [Inject] private GameSettingsSO _gameSettings;
        
        private CancellationTokenSource _cts;

        private void Awake()
        {
            _inputManager.MovePlayerCommand
                .Subscribe(MovePlayerCommandHandler);

            _inputManager.StopPlayerCommand
                .Subscribe(StopPlayerCommandHandler);
        }

        private void MovePlayerCommandHandler(Path path)
        {
            if(_cts != null)
                _cts.Cancel();

            _cts = new CancellationTokenSource();
            MoveAlongPath(path);
        }

        private void StopPlayerCommandHandler(Unit unit)
        {
            StopMovement();
        }

        public async UniTask MoveAlongPath(Path path)
        {
            float totalPathDistance = path.GetRelativeLength(transform.position);
            float travelledDistance = 0f;
            float dynamicSpeed = _gameSettings.PlayerSpeed;
            
            if(path._points == null) {return;}

            foreach (var targetPoint in path._points)
            {
                while (Vector3.Distance(transform.position, targetPoint) > 0.01f)
                {
                    dynamicSpeed = _gameSettings.PlayerSpeed *
                                   RecalculateSpeedMultiplier(
                                       CalculateProgress(travelledDistance, totalPathDistance));

                    Vector3 direction = (targetPoint - transform.position).normalized;
                    float distance = dynamicSpeed * Time.deltaTime;
                    travelledDistance += distance;
                    _distanceAnalytic.RegisterNewDeltaDistance(distance);

                    Vector3 delta = direction * distance;
                    transform.position += delta;

                    await UniTask.Yield(PlayerLoopTiming.Update,_cts.Token,true);
                }
            }
        }

        public void StopMovement()
        {
            _cts.Cancel();
        }

        private float CalculateProgress(float progress, float totalDistance) => progress / totalDistance;

        private float RecalculateSpeedMultiplier(float progress) => _gameSettings.SpeedChangeCurve.Evaluate(progress);

        private void OnDestroy()
        {
            if(_cts == null) return;
            _cts.Cancel();
        }
    }
}