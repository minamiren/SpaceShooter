using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject asteroidParent;
    private int spawnCount = 8;
    public float spawnWait;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(spawnWait);
        while(true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                yield return new WaitForSeconds(.5f);
                int x = Random.Range(1, 32) - 16;
                int z = Random.Range(15, 23);
                Vector3 spawnPosition = new Vector3(x, 0, z);
                Transform asteroidTransform = asteroid.transform;
                asteroidTransform.position = spawnPosition;
                GameObject spawned = Instantiate(asteroid, asteroidTransform);
                spawned.transform.parent = asteroidParent.transform;
            }
            yield return new WaitForSeconds(spawnWait * 6);
        }
    }
}
