using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsSO", menuName = "AUL/ScriptableObjects/GameSettingsSO", order = 1)]
public class GameSettingsSO : ScriptableObject
{
   [Header("Player Settings")] 
   [SerializeField]
   private float _playerSpeed;
   
   [SerializeField]
   private AnimationCurve _speedChangeCurve;

   [SerializeField] 
   private int _enemyCount;
   
   [SerializeField] 
   private float _enemySpawnDelay;
   
   [SerializeField]
   private GameObject _enemyPrefab;

   public int EnemyCount => _enemyCount;
   public float EnemySpawnDelay => _enemySpawnDelay;

   public GameObject EnemyPrefab => _enemyPrefab;
   
   public float PlayerSpeed => _playerSpeed;
   public AnimationCurve SpeedChangeCurve => _speedChangeCurve;
}
