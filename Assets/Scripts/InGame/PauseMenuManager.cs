using UnityEngine;

namespace InGame
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
    
    
        private bool _pause = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_pause)
                {
                    InitPause();
                }
                else
                {
                    StopPause();
                }
            }
        }
        private void SwitchPause()
        {
            _pause = !_pause;
        }
        private void InitPause()
        {
            SwitchPause();
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        public void StopPause()
        {
            SwitchPause();
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        public void Exit()
        {
            StopPause();
            InGameManager.Instance.Lose();
        }
    }
}
