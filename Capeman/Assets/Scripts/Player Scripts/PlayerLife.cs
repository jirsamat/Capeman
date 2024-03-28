using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] Rigidbody2D myrigidbody2D;

    public int MaxHealth = 10;
    public int CurrentHealth;
    public float Knockback = 5f;
    private bool isDead = false;

    public Animator animator;
    void Start()
    {
        CurrentHealth = MaxHealth;
    }
    private void Update()
    {
        if(CurrentHealth==0)
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

        animator.SetBool("isDead", true);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
