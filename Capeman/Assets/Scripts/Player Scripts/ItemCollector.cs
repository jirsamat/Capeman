using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public LayerMask collectibleLayer; 
    public Transform collector;
    public float CollectRange = 1.6f;

    private float SpeedMult;
    private float KBMult;
    private int DMGMult;
    private float ATKMult;
    [SerializeField] private Text collectedText;

    private void Start()
    {
        SpeedMult = gameObject.GetComponent<PlayerMovement>().sprintSpeed;
        KBMult = gameObject.GetComponent<PlayerCombat>().knockBack;
        DMGMult = gameObject.GetComponent<PlayerCombat>().damage;
        ATKMult = gameObject.GetComponent<PlayerCombat>().attackRate;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collectible = Physics2D.OverlapCircle(collector.position, CollectRange, collectibleLayer);

        if (collectible != null)
        {
            Destroy(collectible.gameObject);
            if (collectible.CompareTag("HealthDrop"))
            {
                gameObject.GetComponent<PlayerLife>().Heal(1);
            }
            if (collectible.CompareTag("MaxHealthDrop"))
            {
                gameObject.GetComponent<PlayerLife>().MaxHealth++;
                gameObject.GetComponent<PlayerLife>().UpdateHP();

            }
            if (collectible.CompareTag("SpeedDrop"))
            {
                if (gameObject.GetComponent<PlayerMovement>().sprintSpeed <20)
                {
                    gameObject.GetComponent<PlayerMovement>().sprintSpeed += .1f;
                    SpeedMult++;
                }
            }
            if (collectible.CompareTag("KnockBackDrop"))
            {
                gameObject.GetComponent<PlayerCombat>().knockBack += .1f;
                gameObject.GetComponent<PlayerCombat>().knockBackdur += .1f;
                KBMult += .2f;
            }
            if (collectible.CompareTag("DamageDrop"))
            {
                gameObject.GetComponent<PlayerCombat>().damage++;
                DMGMult++;
            }
            if (collectible.CompareTag("AttackSpeedDrop"))
            {
                gameObject.GetComponent<PlayerCombat>().attackRate += .2f;
                ATKMult++;
            }
        }

        UpdateCollectedText();
    }

    private void UpdateCollectedText()
    {
        collectedText.text = "Speed: " + SpeedMult + "\n" + "Damage: " + DMGMult + "\n" + "Knock Back: " + KBMult + "\n" +  "Attack Speed: " + ATKMult;
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            collected++;
            collectedText.text = "Collected " + collected;
        }
    }
    */
}
