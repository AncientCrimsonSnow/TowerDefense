using UnityEngine;

namespace InGame
{
    public class InGameManager : Singleton<InGameManager>
    {
        public void Win()
        {
            StateManager.Instance.ChangeState(1);
        }
        public void Lose()
        {
            StateManager.Instance.ChangeState(2);
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
