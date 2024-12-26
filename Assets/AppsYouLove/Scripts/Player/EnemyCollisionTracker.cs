using AUL.Enemies;
using UnityEngine;
using UniRx;

namespace AUL.Player
{
    public class EnemyCollisionTracker : MonoBehaviour
    {
        public ReactiveCommand EnemyCollisionCommand { get; private set; } = new ReactiveCommand();

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemy>();
            if(enemy == null) return;
            enemy.DestroyEnemy();
            EnemyCollisionCommand.Execute();
        }
    }
}