using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InGame
{
    public class ProjectileManager : MonoBehaviour
    {
        
        [SerializeField] private GameObject prefabProjectile;
        public void Shoot()
        {
            Vector3 pos = RandomCircle(new Vector3(0,0.5f,0),Random.Range(5,20) );
            GameObject projectile = Instantiate(prefabProjectile, pos, Quaternion.identity);
            projectile.GetComponent<ProjectileController>().durability = Math.Max(1, 5 - (int)Difficulty.Instance.difficulty);
            //Todo Projectile hier in Richtung fliegen lassen.
        }
    
        //temp
        private Vector3 RandomCircle ( Vector3 center ,   float radius  ){
            float ang = Random.value * 360;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y;
            pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            return pos;
        }
    }
}
