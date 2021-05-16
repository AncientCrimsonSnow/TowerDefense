using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public Boolean win = false;
    public Boolean lose = false;

    private void Update()
    {
        if (win)
        {
            win = false;
            Win();
            
        }
        if (lose)
        {
            lose = false;
            Lose();
        }
    }


    public void Win()
    {
        StateManager.Instance.ChangeState(1);
    }
    public void Lose()
    {
        StateManager.Instance.ChangeState(2);
    }
}
