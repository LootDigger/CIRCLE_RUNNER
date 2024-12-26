using System.Reflection;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstallerSO", menuName = "Installers/GameSettingsSO")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] 
    private GameSettingsSO _gameSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_gameSettings);
    }
}
