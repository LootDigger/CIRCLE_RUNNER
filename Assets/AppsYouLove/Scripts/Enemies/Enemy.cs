using System;
using AUL.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace AUL.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public void DestroyEnemy()
        {
            Destroy(gameObject);
        }
    }
}