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
        //Rigidbody of the projectile
        public Rigidbody ball;
        //Target position where to shoot at
        public Vector3 target;
        //Gravity to use for the shot
        public float gravity = 9.81f;
        //The maximum height the projectile should fly
        public int heightToFly = 10;

        private void Awake()
        {
            health = GetComponent<Health>();
            ball = gameObject.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            ball.useGravity = false;
            Launch();
        }
        
        /// <summary>
        /// This method sets the gravity for the flight and sets the projectile velocity to the calculated velocity for the launch.
        /// </summary>
        private void Launch()
        {
            Physics.gravity = Vector3.up * -gravity;
            ball.useGravity = true;
            ball.velocity = CalculateLaunchVelocity();
        }
        
        /// <summary>
        /// This method calculates everything we need to launch a projectile parabolic with physics.
        /// </summary>
        /// <returns>Velocity to shoot a projectile towards a target.</returns>
        private Vector3 CalculateLaunchVelocity()
        {
            float displacementY = target.y - ball.position.y;
            Vector3 displacementXZ = new Vector3(target.x - ball.position.x, 0, target.z - ball.position.z);

            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * -gravity * heightToFly);
            Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * heightToFly / -gravity) + Mathf.Sqrt(2 * (displacementY - heightToFly) / -gravity));
            
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