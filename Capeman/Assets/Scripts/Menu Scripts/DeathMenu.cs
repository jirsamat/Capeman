using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject DeathScreen;
    //on play again button. starts the game
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DeathScreen.SetActive(false);

    }
    //on main menu button, returns to main menu
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");

    }
}
