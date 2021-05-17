using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject unitToSpawn;

    public Boolean temp = true;


    private int distance = 100;

    private void FixedUpdate()
    {
        //Spawn(50000);
    }
    
    public void Spawn(int speed)
    {
        Vector3 pos = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f) * new Vector3(distance,0.5f, 0 );
        GameObject newEnemey = Instantiate(unitToSpawn, pos, Quaternion.identity);
        newEnemey.transform.LookAt(new Vector3(0, 0.5f, 0));
        newEnemey.GetComponent<Rigidbody>().AddForce(speed * Time.deltaTime * newEnemey.transform.forward);
    }
}
