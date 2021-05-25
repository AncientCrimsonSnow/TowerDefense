using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Scripting;

namespace InGame
{
    public class ProjectileController : MonoBehaviour
    {
        //HP of the Projectile
        public Health health;

        public Transform projectile;
        public Rigidbody ball;
        public Vector3 target;
        public float gravity = 9.81f;
        public int h = 10;

        private void Awake()
        {
            health = GetComponent<Health>();
            ball = gameObject.GetComponent<Rigidbody>();
            projectile = transform;
        }

        private void Start()
        {
            ball.useGravity = false;
            Launch();
        }
        
        private void Launch()
        {
            Physics.gravity = Vector3.up * -gravity;
            ball.useGravity = true;
            ball.velocity = CalculateLaunchVelocity();
        }
        
        private Vector3 CalculateLaunchVelocity()
        {
            float displacementY = target.y - ball.position.y;
            Vector3 displacementXZ = new Vector3(target.x - ball.position.x, 0, target.z - ball.position.z);

            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * -gravity * h);
            Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * h / -gravity) + Mathf.Sqrt(2 * (displacementY - h) / -gravity));
            
            return velocityXZ + velocityY;
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