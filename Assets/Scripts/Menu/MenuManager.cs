using UnityEngine;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {

        public void StartGameDiff1()
        {
            SetDifficulty(0);
            StartGame();
        }
        public void StartGameDiff2()
        {
            SetDifficulty(1);
            StartGame();
        }
        public void StartGameDiff3()
        {
            SetDifficulty(2);
            StartGame();
        }

        private void SetDifficulty(int difficulty)
        {
            Difficulty.Instance.difficulty = difficulty;
        }
        private void StartGame()
        {
            StateManager.Instance.ChangeState(1);
        }
    }
}
