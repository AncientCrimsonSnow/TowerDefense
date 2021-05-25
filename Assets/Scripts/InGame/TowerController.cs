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
            hp = Math.Max(1, 100 - Difficulty.Instance.difficulty * 25);
        }

        private void FixedUpdate()
        {
            //If we shoot!
            if (Input.GetMouseButton(0))
            {
                _projectileManager.Shoot(GetWorldMousePosition());
                //_projectileManager.Shoot();
            }
        }

        private Vector3 GetWorldMousePosition()
        {
            //GameObject ground = GameObject.Find("Ground");
            Plane plane = new Plane(Vector3.up, 0);

            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(plane.Raycast(ray, out distance)){
                Vector3 mousePos = ray.GetPoint(distance);
                mousePos.y = 0.5f;
                
                return mousePos;
            }

            UnityEngine.Debug.Log("No raycast with plane");
            return new Vector3(0, 0, 0);
        }
    }
}
