using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InGame
{
    public class ProjectileManager : MonoBehaviour
    {
        
        [SerializeField] private GameObject prefabProjectile;
        [SerializeField] GameObject tower;

        private void Start()
        {
            tower = GameObject.Find("Tower");
        }

        public void Shoot(Vector3 mousePos)
        {
            //Vector3 pos = RandomCircle(new Vector3(0,0.5f,0),Random.Range(5,20) );

            // get the position of the tower
            // the position is the top of the tower
            Vector3 towerPos = tower.transform.position;

            // Spawn projectile at the top of the tower
            GameObject projectile = Instantiate(prefabProjectile, towerPos, Quaternion.identity);
            projectile.GetComponent<ProjectileController>().target = mousePos;
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
