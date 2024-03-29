using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAlive : MonoBehaviour
{
    private int Seconds = 0;
    private int Minutes = 0;
    [SerializeField] private Text timeAliveText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(count());
    }

    IEnumerator count()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Seconds++;
            if (Seconds == 60)
            {
                Seconds = 0;
                Minutes++;
            }
            timeAliveText.text = "TimeAlive: " + Minutes + " min " + Seconds + " s";

        }
    }
}
