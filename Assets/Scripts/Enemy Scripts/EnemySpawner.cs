using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemeyPrefab;

    public int enemiesAlive;

    private Collider spawnerCollider;
    private Bounds spawnerBounds;

    public int enemiesInWave;

    private void Start()
    {
        spawnerCollider = GetComponent<Collider>();
        spawnerBounds = spawnerCollider.bounds;
    }

    public void SpawnEnemies(int enemiesNum)
    {
        enemiesInWave = enemiesNum;
        enemiesAlive = enemiesNum;
        StartCoroutine(SpawnEnemies(enemiesNum, 0.1f));
    }

    IEnumerator SpawnEnemies(int number, float waitTime)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject enemy = Instantiate(enemeyPrefab, GetRandomPosition(), Quaternion.identity);
            enemy.GetComponent<EnemyHealth>().spawner = this;
            yield return new WaitForSeconds(waitTime);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPos;
        randomPos = new Vector3(
            Random.Range(spawnerBounds.min.x, spawnerBounds.max.x),
            Random.Range(spawnerBounds.min.y, spawnerBounds.max.y),
            Random.Range(spawnerBounds.min.z, spawnerBounds.max.z));
        return randomPos;
    }

    public void EnemyCallingForBackup(int amount)
    {
        enemiesAlive += amount;
        StartCoroutine(SpawnEnemies(amount, 0.1f));

    }

    public void EnemyDied()
    {
         enemiesAlive--;
    }
}
