using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    
    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID, LoadSceneMode.Single);
    }

    public int GetCurrSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
