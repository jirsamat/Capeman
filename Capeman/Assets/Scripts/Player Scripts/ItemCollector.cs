using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public LayerMask collectibleLayer; 
    public Transform collector;
    public float CollectRange = 1.6f;

    private int collected = 0;
    [SerializeField] private Text collectedText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collectible = Physics2D.OverlapCircle(collector.position, CollectRange, collectibleLayer);

        if (collectible != null)
        {
            Destroy(collectible.gameObject);
            if (collectible.CompareTag("HealthDrop"))
            {
                gameObject.GetComponent<PlayerLife>().CurrentHealth++;
                gameObject.GetComponent<PlayerLife>().UpdateHP();
            }
            collected++;
        }

        UpdateCollectedText();
    }

    private void UpdateCollectedText()
    {
        collectedText.text = "Collected " + collected;
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
