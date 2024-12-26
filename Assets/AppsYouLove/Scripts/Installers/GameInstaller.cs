using AUL.Analytic;
using AUL.Core;
using AUL.Player;
using AUL.Services;
using UnityEngine;
using Zenject;

namespace AUL.Installers.Boot
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] 
        private UIManager _uiManager;
        
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField] 
        private EnemyCollisionTracker _enemyCollisionTracker;
        
        public override void InstallBindings()
        {
            Container.Bind<IDataService>().To<PlayerPrefsDataService>().AsSingle();
            
            Container.Bind<IView>().FromInstance(_uiManager);
            Container.Bind<IGameModel>().To<GameData>().AsSingle();
            Container.Bind<IDistanceAnalyticService>().To<TravelDistanceSpyService>().AsSingle();
            Container.Bind<EnemyCollisionTracker>().FromInstance(_enemyCollisionTracker);
            Container.Bind<GameManager>().FromInstance(_gameManager);
        }
    }
}