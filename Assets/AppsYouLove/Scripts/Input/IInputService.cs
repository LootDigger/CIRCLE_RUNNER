using UniRx;

namespace AUL.PlayerInput
{
    public interface IInputService
    {
        public ReactiveCommand<Path> MovePlayerCommand { get; }
        public ReactiveCommand StopPlayerCommand { get; }
    }
}