using System;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class HealthBarManager : MonoBehaviour
    {
        [SerializeField] private HeathBar healthBarPrefab;
        
        private Dictionary<Health, HeathBar> healthBars = new Dictionary<Health, HeathBar>();

        private void Awake()
        {
            Debug.Log("Ich bin der HealthBarManager");
            Health.OnHealthAdded += AddHealthBar;
            Health.OnHealthRemoved += RemoveHealthBar;
        }

        private void AddHealthBar(Health health)
        {
            Debug.Log(health.CurrentHealth);
            if (!healthBars.ContainsKey(health))
            {
                var healthBar = Instantiate(healthBarPrefab, transform);
                healthBars.Add(health, healthBar);
                healthBar.SetHealth(health);
            }
        }

        private void RemoveHealthBar(Health health)
        {
            if (healthBars.ContainsKey(health))
            {
                Destroy(healthBars[health].gameObject);
                healthBars.Remove(health);
            }
        }
    }
}
