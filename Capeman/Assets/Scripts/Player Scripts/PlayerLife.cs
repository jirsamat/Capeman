using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject EnemySpawner;
    public GameObject TimeAlive;

    [SerializeField] Rigidbody2D myrigidbody2D;
    [SerializeField] private Text HPText;

    public int MaxHealth = 10;
    public int CurrentHealth;

    //used for player knockback
    public float Knockback = 5f;
    public bool isKnocked = false;

    private bool isDead = false;

    public Animator animator;
    void Start()
    {
        //sets up the HP
        CurrentHealth = MaxHealth;
        UpdateHP();
    }
    private void Update()
    {
        //checks if player dies
        if (CurrentHealth == 0)
        {
            Die();
        }
    }
    //Detects collision with traps - anything that damages the player -> enemies are also counted as traps
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //after player dies the game is still active, this negates any enemy interaction with the player - safety measure
        if (isDead) return;

        if (collision.gameObject.CompareTag("Trap"))
        {
            //updated HP
            CurrentHealth--;
            UpdateHP();

            //checks if the player dies
            if (CurrentHealth == 0)
            {
                Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
                Die();
            }
            //if not - applies knockback
            else
            {
                ApplyKnockBack(collision);
            }
        }
    }
    //Death function
    private void Die()
    {
        isDead = true;
        //disables player input, when the body hits the floor, freezes its position
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerCombat>().enabled = false;
        if (gameObject.GetComponent<PlayerMovement>().isGrounded())
        {
            gameObject.GetComponent<PlayerMovement>().myRigidBody.bodyType = RigidbodyType2D.Static;
        }

        animator.SetBool("isDead", true);
    }
    //restarts the level. Is called on the deathscreen menu button PlayAgain
    private void RestartLevel()
    {
        DeathScreen.SetActive(true);
        EnemySpawner.SetActive(false);
        TimeAlive.GetComponent<TimeAlive>().StopAllCoroutines();
    }
    //heals the player a specified amount of hp
    public void Heal(int HP)
    {
        int remainingHP = HP;
        while (remainingHP > 0 && CurrentHealth < MaxHealth)
        {
            CurrentHealth++;
            remainingHP--;
        }
        UpdateHP();
    }
    //updates the HP text. Called with Heal, but also with Health Increase
    public void UpdateHP()
    {
        HPText.text = "HP: " + CurrentHealth + "/" + MaxHealth;
    }
    //knock back function for the player
    public void ApplyKnockBack(Collision2D collision)
    {
        isKnocked = true;

        //determines the direction of the knockback
        Vector2 direction = (transform.position - collision.transform.position).normalized;
        //plays the knock back animation
        animator.SetTrigger("KnockBack");

        //resets players velocity and then apllies the knockback
        myrigidbody2D.velocity = Vector2.zero;
        if (direction.x > 0)
        {
            myrigidbody2D.velocity = new Vector2(Knockback, Knockback);
        }
        else
        {
            myrigidbody2D.velocity = new Vector2(-Knockback, Knockback);
        }
    }
    //removes the knockback stun - on knockback animation
    public void StopKnockBack()
    {
        isKnocked = false;
    }
}

