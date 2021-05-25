using UnityEngine;

namespace InGame
{
    public class InGameManager : Singleton<InGameManager>
    {
        public void Win()
        {
            //WIN
            Debug.Log("WIN");
            StateManager.Instance.ChangeState(4);
        }

        public void Lose()
        {
            //LOSE
            Debug.Log("LOSE");
            StateManager.Instance.ChangeState(5);
        }

        /*
         * Clear all the Projectiles in Scene
         */
        public void ClearAllProjectiles()
        {
            var projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (var projectile in projectiles)
            {
                DestroyImmediate(projectile);
            }
        }
    }
}