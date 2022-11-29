using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemeyPrefab;
    public int minSpawnNum;
    public int maxSpawnNum;

    private Collider spawnerCollider;
    private Bounds spawnerBounds;

    private void Start()
    {
        spawnerCollider = GetComponent<Collider>();
        spawnerBounds = spawnerCollider.bounds;
        StartCoroutine(SpawnEnemies(minSpawnNum, 0.1f));
    }

    IEnumerator SpawnEnemies(int number, float waitTime)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(enemeyPrefab, GetRandomPosition(), Quaternion.identity);
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
}
