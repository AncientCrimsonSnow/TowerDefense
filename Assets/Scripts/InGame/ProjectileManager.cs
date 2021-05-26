using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InGame
{
    public class ProjectileManager : MonoBehaviour
    {
        [SerializeField] private GameObject prefabProjectile;

        [SerializeField] GameObject tower;
        [SerializeField] private GameObject projectileFolder;

        private void Start()
        {
            tower = GameObject.Find("Tower");
            projectileFolder = GameObject.Find("Projectiles");
        }

        private void Awake()
        {
            Debug.Log("Ich bin der Tower");
        }

        public void Shoot()
        {
            if (Time.timeScale == 0)
            {
                return;
            }
            
            Vector3 mousePos = GetWorldMousePosition();
            //Vector3 pos = RandomCircle(new Vector3(0,0.5f,0),Random.Range(5,20) );

            // get the position of the tower
            // the position is the top of the tower
            Vector3 towerPos = tower.transform.position;

            // Spawn projectile at the top of the tower
            GameObject projectile = Instantiate(prefabProjectile, towerPos, Quaternion.identity);
            projectile.transform.parent = projectileFolder.transform;
            projectile.GetComponent<ProjectileController>().target = mousePos;
        }

        private Vector3 GetWorldMousePosition()
        {
            //GameObject ground = GameObject.Find("Ground");
            Plane plane = new Plane(Vector3.up, 0);

            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                Vector3 mousePos = ray.GetPoint(distance);
                mousePos.y = 0.5f;

                return mousePos;
            }

            UnityEngine.Debug.Log("No raycast with plane");
            return new Vector3(0, 0, 0);

        }
    }
}