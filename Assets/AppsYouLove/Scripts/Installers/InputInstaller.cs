using Zenject;
using AUL.PlayerInput;
using UnityEngine;

namespace AUL.Installers
{
   public class InputInstaller : MonoInstaller
   {
      [SerializeField]
      private InputManager _inputManager;
      
      [SerializeField]
      private PlayerStopper _playerStopper;
      
      public override void InstallBindings()
      {
         Container.Bind<ITouchListener>().To<TouchInputListener>().AsSingle();
         Container.Bind<IDragListener>().To<DragInputListener>().AsSingle();

         Container.Bind<IInputService>().FromInstance(_inputManager).AsSingle();
         Container.Bind<PlayerStopper>().FromInstance(_playerStopper);
      }
   }
}