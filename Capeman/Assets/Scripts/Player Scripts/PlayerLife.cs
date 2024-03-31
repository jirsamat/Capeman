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
    public float Knockback = 5f;
    private bool isDead = false;

    public Animator animator;
    void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateHP();
    }
    private void Update()
    {
        if (CurrentHealth == 0)
        {
            Die();
        }
    }
    //Detects collision with traps
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Trap"))
        {
            //myrigidbody2D.AddForce((myrigidbody2D.transform.position-collision.transform.position)*Knockback);

            CurrentHealth--;
            UpdateHP();
            if (CurrentHealth == 0)
            {
                Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
                Die();
            }
            else
                myrigidbody2D.AddForce((transform.position - collision.transform.position).normalized * Knockback);
        }
    }
    //Death function
    private void Die()
    {
        isDead = true;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerCombat>().enabled = false;
        if (gameObject.GetComponent<PlayerMovement>().isGrounded())
        {
            gameObject.GetComponent<PlayerMovement>().myRigidBody.bodyType = RigidbodyType2D.Static;
        }

        animator.SetBool("isDead", true);
    }
    private void RestartLevel()
    {
        DeathScreen.SetActive(true);
        EnemySpawner.SetActive(false);
        TimeAlive.GetComponent<TimeAlive>().StopAllCoroutines();
        //SceneManager.LoadScene("DeathScreen");
    }
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
    public void UpdateHP()
    {
        HPText.text = "HP: " + CurrentHealth + "/" + MaxHealth;
    }
}

