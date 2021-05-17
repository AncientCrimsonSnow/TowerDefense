using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 100.0f)] float speed = 100.0f;

    public int durability;
    public float step;
    public Vector3 mousePos;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
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
