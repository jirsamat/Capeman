using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject EnemyParent;
    public GameObject TimeAlive;

    public int DeactivationZone = 25;
    public float SpawnInterval;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(SpawnInterval, Enemy));
    }

    private void Update()
    {
        //increases the frequency of spawning enemies by the time alive
        SpawnInterval = 10f / (TimeAlive.GetComponent<TimeAlive>().Minutes + 1);
    }
    //Enemy spawner
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        while (true)
        {
            //checks if the player is nearby
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

            //if not, spawns a new enemy
            if (!playerNearby)
            {
                GameObject newEnemy = Instantiate(Enemy, transform.position, Quaternion.identity);
                newEnemy.transform.parent = EnemyParent.transform;
            }
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
