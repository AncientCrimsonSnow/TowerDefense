using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
   [SerializeField] private LoadingManager loadingManager;
   
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

   public void ChangeState(int newState)
   {
      ChangeState(newState,_state.Length - 1);
   }
   private void ChangeState(int newState, int depth)
   {
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
      PrintState();
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
            //Default Start Scene
            ChangeState(1,0);
            break;
         case 1:
            //Menu
            CheckScene(1);
            switch (_state[1])
            {
               case 0:
                  //loading MainMenu
                  ChangeState(1,1);
                  break;
               case 1:
                  //MenuPoints
                  switch (_state[2])
                  {
                     case 0:
                        //Wait
                        break;
                     case 1:
                        //loading IngameScene
                        ChangeState(2,0);
                        break;
                  }
                  break;
            }
            break;
         case 2:
            CheckScene(2);
            //ingame
            switch (_state[1])
            {
               case 0:
                  //Loading IngameStuff
                  ChangeState(1, 1);
                  break;
               case 1:
                  //Playing
                  switch (_state[2])
                  {
                     case 0:
                        //spawning

                        break;
                     case 1:
                        //Do winningStuff
                        Debug.Log("win");
                        ChangeState(3, 0);
                        break;
                     case 2:
                        //Do losingStuff
                        Debug.Log("lose");
                        ChangeState(3, 0);
                        break;
                  }
                  break;
            }
            break;
         case 3:
            //reload Menu
            Debug.Log("reload");
            ChangeState(1,0);
            break;
      }
   }

   private void CheckScene(int SceneID)
   {
      if (loadingManager.GetCurrSceneIndex() != SceneID)
      {
         loadingManager.LoadScene(SceneID);
      }
   }
}
