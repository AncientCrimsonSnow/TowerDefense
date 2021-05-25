using System;
using System.Diagnostics;
using UnityEngine;

namespace InGame
{
    public class TowerController : MonoBehaviour
    {
        public int hp;
    
        [SerializeField] private ProjectileManager _projectileManager;

        private void Awake()
        {
            hp = Math.Max(1, 100 - (int) Difficulty.Instance.difficulty * 25);
        }

        private void FixedUpdate()
        {
            //If we shoot!
            if (Input.GetMouseButton(0))
            {
                //_projectileManager.Shoot(GetWorldMousePosition());
                _projectileManager.Shoot();
            }
        }
    }
}
