using UnityEngine;

namespace InGame
{
    public class EnemyController : MonoBehaviour
    {
        public int hp;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name.Equals("Tower"))
            {
                /*
                 * if we hit the Tower, the Tower takes Dmg on the amount of the hp of the Enemy
                 */
                other.gameObject.GetComponent<TowerController>().hp -= hp;
                if (other.gameObject.GetComponent<TowerController>().hp <= 0) InGameManager.Instance.Lose();
                Destroy(gameObject);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
            {
                /*
                 * If the opponent comes into contact with a projectile,
                 * they deduct their HP or their durability from each other
                 * and also Destorys them, if they get below 0
                 */
                var dmg = other.gameObject.GetComponent<ProjectileController>().durability;
                /*
                 * Sometimes Destorys is to late to we get an negative hp Enemy, that would "heal" our Projectile, we dont want that.
                 */
                if (hp > 0) other.gameObject.GetComponent<ProjectileController>().durability -= hp;
                /*
                 * Sometimes Destorys is to late to we get an negative dmg , that would "heal" our Enemy, we dont want that.
                 */
                if (dmg > 0) hp -= dmg;

                /*
                 * Destorys everything that falls under 0 hp/durability
                 */
                if (other.gameObject.GetComponent<ProjectileController>().durability <= 0) Destroy(other.gameObject);
                if (hp <= 0) Destroy(gameObject);
            }
        }
    }
}