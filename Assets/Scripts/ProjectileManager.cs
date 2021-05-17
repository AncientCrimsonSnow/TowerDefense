using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] GameObject tower;
    [SerializeField] [Range(0.0f, 100.0f)] float speed = 0.1f;

    public Vector3 mousePosition;

    private void Start()
    {
        tower = GameObject.Find("Tower");
    }

    [SerializeField] private GameObject prefab_Projectile;
    public void Shoot(Vector3 mousePos)
    {
        //Vector3 pos = RandomCircle(new Vector3(0,0.5f,0),Random.Range(5,20) );

        mousePosition = mousePos;

        // get the position of the tower
        // the posoition is the top of the tower
        Vector3 towerPos = tower.transform.position;

        // Spawn projectile at the top of the tower
        GameObject projectile = Instantiate(prefab_Projectile, towerPos, Quaternion.identity);
        //projectile.GetComponent<ProjectileController>().mousePos = mousePos;
        Vector3 relativePos = projectile.transform.position - mousePos;
        projectile.GetComponent<Rigidbody>().AddForce(speed * relativePos);
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
