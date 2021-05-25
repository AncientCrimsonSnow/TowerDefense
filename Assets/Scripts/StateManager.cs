using System;
using System.Collections;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    private readonly int[] _state = {0, 0, 0};
    private LoadingManager _loadingManager;

    private void Awake()
    {
        _loadingManager = LoadingManager.Instance;
        ChangeState(0, 0);
    }

    public int GetState(int depth)
    {
        if (depth < 0 || depth > _state.Length - 1)
        {
            Debug.Log("no valid depth");
            return -1;
        }

        return _state[depth];
    }

    public void ChangeState(int newState)
    {
        ChangeState(newState, _state.Length - 1);
    }

    private void ChangeStateAfterTime(int newState, int depth, int time)
    {
        StartCoroutine(DoSomethingAfterTime(() => ChangeState(newState, depth), time));
    }

    public IEnumerator DoSomethingAfterTime(Action function, int time)
    {
        yield return new WaitForSeconds(time);
        function();
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
            for (var i = depth + 1; i < _state.Length; i++)
                _state[i] = 0;
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
                ChangeState(1, 0);
                break;
            case 1:
                //Menu
                CheckScene(1);
                switch (_state[1])
                {
                    case 0:
                        ChangeState(1, 1);
                        break;
                    case 1:
                        //Wait(Showing Main Menu)
                        switch (_state[2])
                        {
                            case 0:
                                ChangeState(1, 2);
                                break;
                            case 1:
                                //Wait
                                break;
                            case 2:
                                //Exit Game
                                ChangeState(4, 0);
                                break;
                            case 3:
                                //Difficulty Button Pressed
                                ChangeState(2, 1);
                                break;
                            case 4:
                                //Options Button Pressed
                                ChangeState(3, 1);
                                break;
                        }

                        break;
                    case 2:
                        //Diffucilty Menu
                        switch (_state[2])
                        {
                            case 0:
                                ChangeState(1, 2);
                                break;
                            case 1:
                                //Wait (Showing Difficulty Buttons which starts the Game)
                                Debug.Log(_loadingManager.GetCurrSceneIndex());
                                break;
                            case 2:
                                //Back
                                ChangeState(1, 0);
                                break;
                            case 3:
                                //Start the Game
                                ChangeState(2, 0);
                                break;
                        }

                        break;
                    case 3:
                        //Options Menu
                        //In Progress
                        switch (_state[2])
                        {
                            case 0:
                                ChangeState(1, 2);
                                break;
                            case 1:
                                //Wait
                                break;
                            case 2:
                                //Back
                                ChangeState(1, 0);
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
                                ChangeState(1, 2);
                                break;
                            case 1:
                                //Startstuff
                                ChangeState(2, 2);
                                break;
                            case 2:
                                //Break
                                ChangeStateAfterTime(3, 2, 20);
                                break;
                            case 3:
                                //Spawning
                                ChangeStateAfterTime(2, 2, 60);
                                break;
                            case 4:
                                //Do winningStuff
                                ChangeState(3, 0);
                                break;
                            case 5:
                                //Do losingStuff
                                ChangeState(3, 0);
                                break;
                        }

                        break;
                }

                break;
            case 3:
                //reload Menu
                Debug.Log("reload");
                ChangeState(1, 0);
                break;
            case 4:
                //Close Game
                Application.Quit();
                break;
        }
    }

    private void CheckScene(int SceneID)
    {
        if (_loadingManager.GetCurrSceneIndex() != SceneID && !_loadingManager.isLoading)
        {
            Debug.Log(SceneID);
            _loadingManager.LoadScene(SceneID);
        }
        else if (_loadingManager.GetCurrSceneIndex() == SceneID)
        {
            _loadingManager.isLoading = false;
        }
    }
}