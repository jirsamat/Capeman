using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject EnemyParent;

    public int DeactivationZone = 25;
    public float SpawnInterval = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(SpawnInterval, Enemy));
    }

    //Enemy spawner
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DeactivationZone);
            bool playerNearby = false;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    playerNearby = true;
                    break;
                }
            }

            if (!playerNearby)
            {
                Debug.Log("Player not in proximity");
                GameObject newEnemy = Instantiate(Enemy, transform.position, Quaternion.identity);
                newEnemy.transform.parent = EnemyParent.transform;
            }
            else
            {
                Debug.Log("Player in proximity" + this.name);
            }

            yield return new WaitForSeconds(SpawnInterval);
        }
        /*
        //checks if player is near, then pauses the spawning
        while (Physics2D.OverlapCircle(transform.position, DeactivationZone).CompareTag("Player"))
        {
            Debug.Log("Player in proximity");
            yield return null;
        }
        //if player is not near, the spawning continues
        Debug.Log("Player not in proximity");
        //waits for the interval, then proceeds
        yield return new WaitForSeconds(interval);
        //instantiate creates new game object (the prefab, the position in which it will spawn, quaternion identity ensures the rotation will be the same as the parent
        GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        //gives the parents position to the enemy
        newEnemy.transform.parent = EnemyParent.transform;
        //starts over again
        StartCoroutine(spawnEnemy(interval, enemy));
        */
    }
}
