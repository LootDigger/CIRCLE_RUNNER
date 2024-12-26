using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsSO", menuName = "AUL/ScriptableObjects/GameSettingsSO", order = 1)]
public class GameSettingsSO : ScriptableObject
{
   [Header("Player Settings")] 
   [SerializeField]
   private float _playerSpeed;
   
   [SerializeField]
   private AnimationCurve _speedChangeCurve;


   public float PlayerSpeed => _playerSpeed;
   public AnimationCurve SpeedChangeCurve => _speedChangeCurve;
}
