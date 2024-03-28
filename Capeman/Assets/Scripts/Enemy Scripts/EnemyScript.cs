using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    public AIPath aiPath;
    public float MaxHealth = 2;
    public float CurrentHealth;
    public float KnockBackStun;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        //if the enemy recieves a knockback, it gets stunned for a bit, disabling its movement for the duration
        if (KnockBackStun > 0)
        { 
            GetComponent<AIPath>().enabled = false;
            KnockBackStun -= Time.deltaTime;
        }
        //when the knockback runs out, the ai pathsetter is enabled again
        else
        {
            GetComponent<AIPath>().enabled = true;
        }

        //flips the enemy based of its direction - gfx purposes
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } 
        else if (aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    // function for taking damage, calculates both the health and knockback
    public void TakeDamage(float damage, float knockBack, float knockBackDur, Vector2 direction)
    {
        //applies the damage and sets up the duration of the knockback stun
        CurrentHealth -= damage;
        KnockBackStun = knockBackDur;

        //checks if the enemy dies thanks to the damage caused
        if(CurrentHealth <= 0)
        {
            Die();
            return;
        }

        //resets enemys velocity and then applies the knockback
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockBack, ForceMode2D.Impulse);


    }
    //death function
    void Die()
    {
        //plays the death animation, at the end there is an event marker that calls the deleteEnemy function
        animator.SetBool("isDead", true);

        //disables all its movement
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<AIPath>().enabled = false;
        this.enabled = false;

    }
    //called at the end of the death animation, deletes the game object
    public void DeleteEnemy()
    {
        Destroy(gameObject);
    }
}
