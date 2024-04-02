using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    //general variables for attacks
    public int damage = 1;
    public float attackRate = 2f;
    public float knockBack = 2f;
    public float knockBackdur = 2f;
    float nextAttackTime = 0f;
    public LayerMask enemyLayer;

    //individual frame dependent hitreg colliders
    public PolygonCollider2D[] colliders = new PolygonCollider2D[4];
    //current collider is adecuate to the current attack animation frame
    int colIndex = 0;

    // Update is called once per frame
    void Update()
    {
        //checks if the cooldown has ran out
        if (Time.time >= nextAttackTime)
        {
            //the individual attacks are in the animation as events - better hit reg, allows for higher skill ceiling
            if (Input.GetKeyDown(KeyCode.S))
            {
                //Attack();

                //plays the attack animation
                animator.SetTrigger("Attack");

                //adds attack cooldown
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    //called on four frames in the attack anim.
    public void HitReg()
    {
        //sets up a filter for filtering enemies that should be hit
        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(enemyLayer);

        //gets a list of all enemies overlapping the current collider
        List<Collider2D> hitEnemies = new List<Collider2D>();
        int colcount = Physics2D.OverlapCollider(colliders[colIndex], enemyFilter, hitEnemies);

        //damages the enemies
        foreach (Collider2D enemy in hitEnemies)
        {
           //calculates the direction for the knockback, normalized converts it to a vector of value 1
           Vector2 direction = (enemy.transform.position - transform.position).normalized;
           //Calls the takedamage function from enemy script
           enemy.GetComponent<EnemyScript>().TakeDamage(damage, knockBack, knockBackdur, direction);
        }
        //cycles the colliders
        colIndex++;
        if (colIndex >= colliders.Length)
            colIndex = 0;
        //clears the list of enemies
        hitEnemies.Clear();
    }
}

