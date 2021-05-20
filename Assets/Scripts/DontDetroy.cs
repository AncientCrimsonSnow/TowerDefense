using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDetroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
