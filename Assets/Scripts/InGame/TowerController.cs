using System;
using UnityEngine;

namespace InGame
{
    public class TowerController : MonoBehaviour
    {

        private ProjectileManager _projectileManager;
        public Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
            _projectileManager = GetComponent<ProjectileManager>();
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