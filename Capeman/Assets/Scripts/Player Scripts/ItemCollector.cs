using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public LayerMask playerLayerMask;
    public LayerMask attackLayerMask;

    private int collected = 0;
    [SerializeField] private Text collectedText;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayerMask == (playerLayerMask | (1 << collision.gameObject.layer)))
        {
            // This collider belongs to the player, so collect the item
            Destroy(collision.gameObject);
            collected++;
            collectedText.text = "Collected " + collected;
        }
        else if (attackLayerMask == (attackLayerMask | (1 << collision.gameObject.layer)))
        {
            // This collider belongs to an attack, do not collect the item
        }
    }
}
