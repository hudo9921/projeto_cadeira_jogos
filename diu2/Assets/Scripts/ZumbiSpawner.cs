using System.Collections;
using UnityEngine;

public class ZumbiSpawner : MonoBehaviour
{
    public GameObject zumbi;
    public Transform[] spawnPoints;
    public float initialSpawnInterval = 2f; 
    public float timeToIncrementZombies = 60f;
    public int initialZombiesToSpawn = 1; 
    public int zombiesIncrementPerMinute = 1;

    private float timer;
    private float incrementTimer;
    private int zombiesToSpawn;

    void Start()
    {
        timer = 0f;
        incrementTimer = 0f;
        zombiesToSpawn = initialZombiesToSpawn;
        StartCoroutine(SpawnZombiesRoutine());
    }

    void Update()
    {
        
        timer += Time.deltaTime;
        incrementTimer += Time.deltaTime;

        
        if (incrementTimer >= timeToIncrementZombies)
        {
            zombiesToSpawn += zombiesIncrementPerMinute; 
            incrementTimer = 0f;
        }
    }

    IEnumerator SpawnZombiesRoutine()
    {
        while (true)
        {
            // Spawnar zombies
            for (int i = 0; i < zombiesToSpawn; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Vector3 randomOffset = Random.insideUnitSphere * Random.Range(0f, 1f);
                Vector3 spawnPosition = spawnPoint.position + randomOffset;

                
                GameObject newZombie = Instantiate(zumbi, spawnPosition, Quaternion.identity);

               
                Quaternion spawnRotation = Quaternion.Euler(spawnPoint.eulerAngles.x, spawnPoint.eulerAngles.y, 0f);

                
                newZombie.transform.rotation = spawnRotation;

                
                newZombie.transform.parent = spawnPoint;
            }

            
            yield return new WaitForSeconds(initialSpawnInterval);
        }
    }
}
