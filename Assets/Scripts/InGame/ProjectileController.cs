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
        public int durability;

        public Transform projectile;
        public Rigidbody ball;
        public Vector3 target;
        public float gravity = 9.81f;
        public int h = 10;

        public float firingAngle = 45.0f;

        private void Awake()
        {
            ball = gameObject.GetComponent<Rigidbody>();
            projectile = transform;
        }

        private void Start()
        {
            //StartCoroutine(SimulateProjectile());
            //StartCoroutine(ParabolicMotion(projectile.transform, target));
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
        
        private IEnumerator ParabolicMotion(Transform projectileTransform, Vector3 target)
        {
            // Calculate the range from the projectile to the target by zero-ing each out in their y-axis
            Vector3 zeroedOrigin = new Vector3(projectileTransform.position.x, 0, projectileTransform.position.z);
            Vector3 zeroedTarget = new Vector3(target.x, 0, target.z);
            Vector3 zeroedDirection = (zeroedTarget - zeroedOrigin).normalized;
 
            float angleRad = firingAngle * Mathf.Deg2Rad;
            float heightDifference = projectileTransform.position.y - target.y;
            float targetDistance = Vector3.Distance(projectileTransform.position, target);
            float targetRange = Vector3.Distance(zeroedOrigin, zeroedTarget);
 
            // Calculate the velocity needed to throw the object to the target at specified angle.
            // Velocity can be solved by re-arranging the general equation for parabolic range:
            // https://en.wikipedia.org/wiki/Range_of_a_projectile
            float projectile_Velocity
                = (Mathf.Sqrt(2) * targetRange * Mathf.Sqrt(-gravity) * Mathf.Sqrt(1 / (Mathf.Sin(2 * angleRad)))) /
                  (Mathf.Sqrt((2 * targetRange) + (heightDifference * Mathf.Sin(2 * angleRad) * (1 / Mathf.Sin(angleRad)) * (1 / Mathf.Sin(angleRad)))));
 
            // Extract the X  Y componenent of the velocity
            float Vx = projectile_Velocity * Mathf.Cos(angleRad);
            float Vy = projectile_Velocity * Mathf.Sin(angleRad);
 
            // Calculate flight time.
            float flightDuration = targetRange / Vx;
 
            // Rotate projectile to face the target.
            projectileTransform.rotation = Quaternion.LookRotation(zeroedDirection);
 
            float elapse_time = 0;
 
            while (elapse_time < flightDuration)
            {
                transform.Translate(Vx * Time.deltaTime, 0, (Vy - (-gravity * elapse_time)) * Time.deltaTime);
 
                elapse_time += Time.deltaTime;
 
                yield return null;
            }
        }

        private IEnumerator SimulateProjectile()
        {
            // Short delay added before Projectile is thrown
            yield return new WaitForSeconds(1.5f);

            // Calculate distance to target
            float targetDistance = Vector3.Distance(projectile.position, target);
            //Debug.Log("Distance: " + target_Distance);

            // Calculate the velocity needed to throw the object to the target at specified angle.
            float projectileVelocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / -gravity);

            // Extract the X  Y componenent of the velocity
            float vx = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float vy = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

            // Calculate flight time.
            float flightDuration = targetDistance / vx;

            // Rotate projectile to face the target.
            projectile.rotation = Quaternion.LookRotation(target - projectile.position);

            float elapseTime = 0;

            while (elapseTime < flightDuration)
                //while (transform.position.y > 0.5f)           
            {
                projectile.Translate((vy - (-gravity * elapseTime)) * Time.deltaTime, 0, vx * Time.deltaTime);

                elapseTime += Time.deltaTime;

                yield return null;
            }
        }
        
        private IEnumerator SimulateProjectile2()
        {
            // Short delay added before Projectile is thrown
            yield return new WaitForSeconds(1.5f);

            // Calculate distance to target
            float throwRange = Vector3.Distance(projectile.position, target);

            // Calculate the launch velocity
            float launchVelocity = (float) Math.Sqrt(throwRange / Math.Sin(firingAngle));

            // Get x and y components of velocity
            float vx = (float) (launchVelocity * Math.Cos(firingAngle));
            float vy = (float) (launchVelocity * Math.Sin(firingAngle));

            // Calculate time of flight
            float flightTime = (float) ((vy + Math.Sqrt((vy * vy) * 2 * -gravity * projectile.position.y)) / -gravity);
            
            float elapseTime = 0;

            while (elapseTime < flightTime)
                //while (transform.position.y > 0.5f)           
            {
                projectile.Translate((vy - (-gravity * elapseTime)) * Time.deltaTime, 0, vx * Time.deltaTime);
                //projectile.Translate();
                
                elapseTime += Time.deltaTime;

                yield return null;
            }
        }

        /*
         * if we want to freeze it:
         */
        public void Freeze(bool freeze)
        {
            if (freeze)
            {
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; 
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            //Freeze the Projectile, if we hit the Ground
            if (other.gameObject.name.Equals("Ground"))
            {
                Freeze(true);
            }
        }
    }
}
