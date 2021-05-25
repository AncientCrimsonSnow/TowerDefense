using System;
using UnityEngine;

namespace InGame
{
    public class EnemyController : MonoBehaviour
    {
        public Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name.Equals("Tower"))
            {
                /*
                 * if we hit the Tower, the Tower takes Dmg on the amount of the hp of the Enemy
                 */
                other.gameObject.GetComponent<TowerController>().health.ModifyHealth(-health.CurrentHealth);
                Debug.Log(other.gameObject.GetComponent<TowerController>().health.CurrentHealth);
                if (other.gameObject.GetComponent<TowerController>().health.CurrentHealth <= 0) InGameManager.Instance.Lose();
                Destroy(gameObject);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
            {
                /*
                 * If the opponent comes into contact with a projectile,
                 * they deduct their HP or their durability from each other
                 * and also Destorys them, if they get below 0
                 */
                var dmg = other.gameObject.GetComponent<ProjectileController>().health.CurrentHealth;
                /*
                 * Sometimes Destorys is to late to we get an negative hp Enemy, that would "heal" our Projectile, we dont want that.
                 */
                if (health.CurrentHealth > 0) other.gameObject.GetComponent<ProjectileController>().health.ModifyHealth(-health.CurrentHealth);
                /*
                 * Sometimes Destorys is to late to we get an negative dmg , that would "heal" our Enemy, we dont want that.
                 */
                if (dmg > 0) health.ModifyHealth(-dmg);

                /*
                 * Destorys everything that falls under 0 hp/durability
                 */
                if (other.gameObject.GetComponent<ProjectileController>().health.CurrentHealth <= 0) Destroy(other.gameObject);
                if (health.CurrentHealth <= 0) Destroy(gameObject);
            }
        }
    }
}