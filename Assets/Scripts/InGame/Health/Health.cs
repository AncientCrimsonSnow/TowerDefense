using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //https://www.youtube.com/watch?v=kQqqo_9FfsU
    public static event Action<Health> OnHealthAdded = delegate{  };
    public static event Action<Health> OnHealthRemoved = delegate{  };

    [SerializeField] private int maxHealth;
    public int CurrentHealth { get; private set; }
    
    public event Action<float> OnHealthPctChanged = delegate{  };

    [SerializeField] private bool hasHealthBar;
    
    private void Start()
    {
        CurrentHealth = maxHealth;
        if (hasHealthBar)
        {
            Debug.Log(CurrentHealth);
            OnHealthAdded(this);
        }
    }
    
    public void ModifyHealth(int amount)
    {
        CurrentHealth += amount;
        
        if (hasHealthBar)
        {
            var currentHealthPct = (float) CurrentHealth / (float) maxHealth;
            OnHealthPctChanged(currentHealthPct);
        }
        
    }

    private void OnDisable()
    {
        if (hasHealthBar)
        {
            OnHealthRemoved(this);
        }
    }
}
