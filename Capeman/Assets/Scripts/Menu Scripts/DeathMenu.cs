using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject DeathScreen;
    private void Start()
    {
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DeathScreen.SetActive(false);

    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");

    }
}
