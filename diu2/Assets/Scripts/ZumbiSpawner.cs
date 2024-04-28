using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumbiSpawner : MonoBehaviour
{
    public GameObject zumbi;
    public Transform[] spawnPoints;

    public float spawnTime;

    public float SpawnRadius;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnZumbis());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnZumbis()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            Transform spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];

            Vector3 randomPosition = spawnPoint.position+(Random.insideUnitSphere*SpawnRadius);

            randomPosition.z = 0f;

            Instantiate(zumbi,randomPosition,spawnPoint.rotation);
        }
    }

}
