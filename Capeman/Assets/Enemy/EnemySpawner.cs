using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    public float SpawnInterval = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(SpawnInterval, Enemy));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        //instantiate creates new game object (the prefab, the position in which it will spawn, quaternion identity ensures the rotation will be the same as the parent
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-1f, 1f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));       
    }
}
