using AUL.Analytic;
using AUL.Core;
using AUL.Player;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

public class PlayerDriver : MonoBehaviour, IDriver
{
    [Inject]
    private IInputService _inputManager;
    [Inject]
    private IDistanceAnalyticService _distanceAnalytic;
    [Inject]
    private IGameModel _gameModel;
    [Inject] 
    private GameSettingsSO _gameSettings;
    
    private bool _isStopped;

    private void Awake()
    {
        _inputManager.MovePlayerCommand
            .Subscribe(MovePlayerCommandHandler);
    }

    private void MovePlayerCommandHandler(Path path)
    {
        Debug.Log("MovePlayerCommandHandler");
        MoveAlongPath(path);
    }

    public async UniTask MoveAlongPath(Path path)
    {
        _isStopped = false;
        float totalPathDistance = path.GetRelativeLength(transform.position);
        float travelledDistance = 0f;
        float dynamicSpeed = _gameSettings.PlayerSpeed;
        
        foreach (var targetPoint in path._points)
        {
            while (Vector3.Distance(transform.position, targetPoint) > 0.01f)
            {
                if (_isStopped) return;

                dynamicSpeed = _gameSettings.PlayerSpeed * RecalculateSpeedMultiplier(CalculateProgress(travelledDistance, totalPathDistance));

                Vector3 direction = (targetPoint - transform.position).normalized;
                float distance = dynamicSpeed * Time.deltaTime;
                travelledDistance += distance;
                _distanceAnalytic.RegisterNewDeltaDistance(distance);

                Vector3 delta = direction * distance;
                transform.position += delta;

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }
    }

    public void StopMovement()
    {
        _isStopped = true;
    }

    private float CalculateProgress(float progress, float totalDistance) => progress/totalDistance;

    private float RecalculateSpeedMultiplier(float progress) => _gameSettings.SpeedChangeCurve.Evaluate(progress);
}
