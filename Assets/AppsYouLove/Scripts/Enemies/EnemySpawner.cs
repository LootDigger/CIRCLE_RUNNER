using System;
using System.Threading;
using AUL.Player;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettings;
    [Inject] private EnemyCollisionTracker _enemyCollisionTracker;

    private Camera _mainCamera;
    private int _currentEnemyCount = 0;
    private CancellationTokenSource _mainCancelTokenSource = new CancellationTokenSource();

    async UniTaskVoid Start()
    {
        _mainCamera = Camera.main;
        SubscribeEvents();
        await SpawnRoutine();
    }

    private void OnDestroy()
    {
        _mainCancelTokenSource.Cancel();
    }

    private void SubscribeEvents()
    {
        _enemyCollisionTracker.EnemyCollisionCommand.Subscribe(_=>
        {
            _currentEnemyCount--;
        });
    }

    private async UniTask SpawnRoutine()
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_gameSettings.EnemySpawnDelay), cancellationToken: _mainCancelTokenSource.Token);
            SpawnEnemyWithDelay();
        }
    }

    private void SpawnEnemyWithDelay()
    {
        if (_currentEnemyCount < _gameSettings.EnemyCount)
        {
            Vector3 spawnPosition = GetRandomScreenPosition();
            Instantiate(_gameSettings.EnemyPrefab, spawnPosition, Quaternion.identity);
            _currentEnemyCount++;
        }
    }

    private Vector3 GetRandomScreenPosition()
    {
        Vector3 screenBottomLeft = _mainCamera.ScreenToWorldPoint(new Vector3(0, 0, _mainCamera.nearClipPlane));
        Vector3 screenTopRight = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.nearClipPlane));

        float randomX = Random.Range(screenBottomLeft.x, screenTopRight.x);
        float randomZ = Random.Range(screenBottomLeft.z, screenTopRight.z);

        return new Vector3(randomX, 0f, randomZ);
    }
}
