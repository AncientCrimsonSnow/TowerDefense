using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Equals("Tower"))
        {
            other.gameObject.GetComponent<TowerController>().hp -= hp;
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            var dmg = other.gameObject.GetComponent<ProjectileController>().durability;
            other.gameObject.GetComponent<ProjectileController>().durability -= hp;
            hp -= dmg;
            if (other.gameObject.GetComponent<ProjectileController>().durability <= 0)
            {
                Destroy(other.gameObject);
            }
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
