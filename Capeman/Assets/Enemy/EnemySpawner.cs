using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject EnemyParent;

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
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), Random.Range(transform.position.y - 2, transform.position.y + 2), 0), Quaternion.identity);
        newEnemy.transform.parent = EnemyParent.transform;
        StartCoroutine(spawnEnemy(interval, enemy));       
    }
}
