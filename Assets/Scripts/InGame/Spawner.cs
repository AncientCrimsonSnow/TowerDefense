using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


namespace InGame
{
    public class Spawner : MonoBehaviour
    {

        const int MAX_WAVES = 10;
        const int WAVE_LENGTH = 60;
        
        [SerializeField] private GameObject unitToSpawn;

        private float difficulty;
        
        private int hp = 1;
        private int speed = 1000;
        
        private int distance = 100;
        private int currWave = 1;
        

        private void Awake()
        {
            
            difficulty = Difficulty.Instance.difficulty;
            StartCoroutine(Spawning());
            
        }

        private void SetDifficulty(float difficulty)
        {
            var speedHelper = 500;
            hp = (int) (1 * difficulty);
            speed = (int) (speedHelper + difficulty * speedHelper * 0.75f);
        }

        /*
         * Spawning all Waves
         */
        private IEnumerator Spawning()
        {
            //till max Waves
            while (currWave <= MAX_WAVES)
            {
                //Each Wave the enemys getting stronger, so we set the difficulty each time a bit higher
                SetDifficulty(difficulty);
                difficulty += difficulty*0.2f;
                
                //Each Wave is also an IEnumerator, each sec. we spawn one Enemy
                var spawnWaveCoroutine = SpawnWave();
                StartCoroutine(spawnWaveCoroutine);
                
                //We spawn them WAVE_LENGTH sec long till we stop them
                yield return new WaitForSeconds(WAVE_LENGTH);
                StopCoroutine(spawnWaveCoroutine);
                
                
                //Give some Space, that the last spawned Enemys can arrive till the next Wave is spawned
                Debug.Log("Auslauf/Pause");
                yield return new WaitForSeconds(20);
                
                //Clear all Projectiles
                InGameManager.Instance.ClearAllProjectiles();
                
                currWave++;
                StartCoroutine(Spawning());
            }
            
            //If we Survive all Waves, we win.
            InGameManager.Instance.Win();
        }

        /*
         * Spawning every sec. one Enemy, till it Stops.
         */
        private IEnumerator SpawnWave()
        {
            Debug.Log("NEXT WAVE");
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(1);
            }
        }
        /*
         * Spawns the Enemy in a random circle in "distance" range which move towards the center.
         */
        public void Spawn()
        {
            Vector3 pos = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f) * new Vector3(distance,0.5f, 0 );
            GameObject newEnemey = Instantiate(unitToSpawn, pos, Quaternion.identity);
            newEnemey.transform.LookAt(new Vector3(0, 0.5f, 0));
            newEnemey.GetComponent<Rigidbody>().AddForce(speed * newEnemey.transform.forward);
            newEnemey.GetComponent<EnemyController>().hp = hp;
        }
    }
}
