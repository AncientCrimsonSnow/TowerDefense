using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //[SerializeField] [Range(0.0f, 100.0f)] float speed = 100.0f;

    public int durability;
    public float step;
    public Vector3 mousePos;
    private Rigidbody rigidBody;

    public float firingAngle = 45.0f;
    public float gravity = 9.81f; // g

    public Transform Projectile;
    public Transform Target;
    private Transform myTransform;

    private void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        myTransform = transform;
    }

    private void Start()
    {
        StartCoroutine(SimulateProjectile());
    }

    public void Update()
    {
        /*if(Vector3.Distance(transform.position, mousePos) > 0.001f)
        {
            step = speed * Time.deltaTime; ;
            transform.position = Vector3.MoveTowards(transform.position, mousePos, step);
        }*/

        //Vector3 relativePos = mousePos - transform.position;
        //rigidBody.AddForce(speed * relativePos);
    }

    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }

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
        if (other.gameObject.name.Equals("Ground"))
        {
            Freeze(true);
        }
    }
}
