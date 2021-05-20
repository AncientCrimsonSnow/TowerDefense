using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : Singleton<LoadingManager>
{
    public bool isLoading = false;
    public void LoadScene(int sceneID)
    {
        isLoading = true;
        SceneManager.LoadScene(sceneID, LoadSceneMode.Single);
    }

    public int GetCurrSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
