using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AUL.Player
{
    public interface IDriver
    {
        UniTask MoveAlongPath(Path path);
        void StopMovement();
    }
}
