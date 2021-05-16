using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public int hp;
    
    [SerializeField] private ProjectileManager _projectileManager;
    
    public ref int getRefToTowerHP()
    {
        return ref hp;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _projectileManager.Shoot();
        }
    }
}
