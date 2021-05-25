using UnityEngine;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject DifficultyMenu;
        [SerializeField] private GameObject OptionsMenu;

        private StateManager _stateManager;

        private GameObject[] AllMenus;

        private void Awake()
        {
            _stateManager = StateManager.Instance;

            AllMenus = new[]
            {
                MainMenu,
                DifficultyMenu,
                OptionsMenu
            };
        }


        public void ToDifficultyMenu()
        {
            switchToMenu(1);
            _stateManager.ChangeState(3);
        }

        public void ToOptionsMenu()
        {
            switchToMenu(2);
            _stateManager.ChangeState(4);
        }

        private void switchToMenu(int menuIndex)
        {
            foreach (var menu in AllMenus) menu.SetActive(false);
            AllMenus[menuIndex].SetActive(true);
        }

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

        private void StartGame()
        {
            _stateManager.ChangeState(3);
        }

        public void Back()
        {
            switchToMenu(0);
            _stateManager.ChangeState(2);
        }

        private void SetDifficulty(int difficulty)
        {
            Difficulty.Instance.difficulty = difficulty;
        }
    }
}