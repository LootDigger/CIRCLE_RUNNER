using UniRx;

public interface IInputService 
{
    public ReactiveCommand<Path> MovePlayerCommand { get; }
}
