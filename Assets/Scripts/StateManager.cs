using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
   private readonly int[] _state = {0, 0, 0};
   
   public int GetState(int depth)
   {
      if (depth < 0 || depth > _state.Length - 1)
      {
         Debug.Log("no valid depth");
         return -1;
      }
      return _state[depth];
   }
   private void Awake()
   {
      ChangeState(0,0);
   }
   public void ChangeState(int newState, int depth)
   {
      PrintState();
      //Checks if param are valid
      if (depth < 0 || depth > _state.Length - 1)
      {
         Debug.Log("no valid depth");
         depth = 0;
      }
      if (newState < 0)
      {
         Debug.Log("no valid state");
         newState = 0;
      }
      
      //set new State
      _state[depth] = newState;

      //When there are subStates, we set them to 0
      if (depth < _state.Length - 1)
      {
         for (int i = depth + 1; i < _state.Length; i++)
         {
            _state[i] = 0;
         }
      }
      PerformStateBehavior();
   }

   public void PrintState()
   {
      Debug.Log(_state[0] + "." + _state[1] + "." + _state[2]);
   }

   private void PerformStateBehavior()
   {
      switch (_state[0])
      {
         case 0:
            //Do some loadingStuff

            ChangeState(1,0);
            break;
         case 1:
            //Menu
            switch (_state[1])
            {
               case 0:
                  //loading MainMenu

                  ChangeState(1,1);
                  break;
               case 1:
                  //Showing MainMenu
                  break;
               case 2:
                  //MenuPoints
                  switch (_state[2])
                  {
                     case 0:
                        //loading firstMenuPoint

                        break;
                     case 1:
                        //loading secMenuPoint...

                        break;
                  }

                  break;
            }

            break;
         case 2:
            //ingame
            switch (_state[1])
            {
               case 0:
                  //Loading IngameStuff
                  
                  ChangeState(1,1);
                  break;
               case 1:
                  //start Spawning
                  
                  break;
               case 2:
                  //spawning
                  
                  break;
               case 3:
                  //Do winningStuff
                  
                  ChangeState(3,0);
                  break;
               case 4:
                  //Do losingStuff
                  
                  ChangeState(3,0);
                  break;
            }
            break;
         case 3:
            //statistics (endScreen)
            switch (_state[1])
            {
               case 0:
                  //load Statistics
                  
                  ChangeState(1,1);
                  break;
               case 1:
                  //show Statistics
                  
                  break;
               case 2:
                  //goBacktoMainMenu
                  
                  ChangeState(1,0);
                  break;
            }
            break;
      }
   }
}
