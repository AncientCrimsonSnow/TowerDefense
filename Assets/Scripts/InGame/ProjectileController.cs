using UnityEngine;

namespace InGame
{
    public class ProjectileController : MonoBehaviour
    {
        //HP of the Projectile
        public Health health;
        
        private void Awake()
        {
            health = GetComponent<Health>();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            //Freeze the Projectile, if we hit the Ground
            if (other.gameObject.name.Equals("Ground")) Freeze(true);
        }
        /*
         * if we want to freeze it:
         */
        public void Freeze(bool freeze)
        {
            if (freeze)
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            else
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}