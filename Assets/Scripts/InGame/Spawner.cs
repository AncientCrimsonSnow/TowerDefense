using System.Collections;
using UnityEngine;

namespace InGame
{
    public class Spawner : MonoBehaviour
    {
        private const int MAX_WAVES = 10;

        [SerializeField] private GameObject unitToSpawn;
        private int currWave;

        private readonly int distance = 100;

        private int hp = 1;
        private int speed = 1000;

        [SerializeField] private GameObject enemyFolder;

        private void Awake()
        {
            StartCoroutine(Break(1));
            enemyFolder = GameObject.Find("Enemies");
        }

        private void SetDifficulty(float difficulty)
        {
            var speedHelper = 500;
            hp = (int) (1 * difficulty);
            speed = (int) (speedHelper + difficulty * speedHelper * 0.75f);
        }

        private IEnumerator SpawnUnits(int time)
        {
            //CheckSPawn
            if (StateManager.Instance.GetState(2) == 2)
            {
                StartCoroutine(Break(time));
            }
            else if (StateManager.Instance.GetState(2) == 3)
            {
                //Spawn
                Spawn();
                yield return new WaitForSeconds(time);
                StartCoroutine(SpawnUnits(time));
            }
        }

        private IEnumerator Break(int spawnTime)
        {
            //Break
            if (CheckforFinalWave()) InGameManager.Instance.Win();
            SetDifficulty(Difficulty.Instance.difficulty);
            Difficulty.Instance.difficulty += Difficulty.Instance.difficulty * 0.1f;
            yield return new WaitUntil(() => StateManager.Instance.GetState(2) == 3);
            InGameManager.Instance.ClearAllProjectiles();
            StartCoroutine(SpawnUnits(spawnTime));
        }

        public bool CheckforFinalWave()
        {
            currWave++;
            if (currWave > MAX_WAVES) return true;
            return false;
        }

        /*
         * Spawns the Enemy in a random circle in "distance" range which move towards the center.
         */
        public void Spawn()
        {
            var pos = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f) * new Vector3(distance, 0.5f, 0);
            var newEnemey = Instantiate(unitToSpawn, pos, Quaternion.identity);
            newEnemey.transform.parent = enemyFolder.transform;
            newEnemey.transform.LookAt(new Vector3(0, 0.5f, 0));
            newEnemey.GetComponent<Rigidbody>().AddForce(speed * newEnemey.transform.forward);
        }
    }
}