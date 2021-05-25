using System;
using UnityEngine;

namespace InGame
{
    public class TowerController : MonoBehaviour
    {

        [Tooltip("In Seconds")] [Range(1.0f, 10f)] [SerializeField] float fireRate = 1.5f;
        private float nextFire = 0.0f;
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
            if (Input.GetMouseButton(0) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                _projectileManager.Shoot();
            }
        }
    }
}