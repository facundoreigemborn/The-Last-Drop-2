using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject panelCredits;
    public GameObject panelControls;

    // MAIN MENU
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // MainGame
    }

    public void OpenCredits()
    {
        panelCredits.SetActive(true);
    }

    public void CloseCredits()
    {
        panelCredits.SetActive(false);
    }

    public void OpenControls()
    {
        panelControls.SetActive(true);
    }

    public void CloseControls()
    {
        panelControls.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }

    // GAME OVER / VICTORY
    public void Retry()
    {
        SceneManager.LoadScene(1); // MainGame
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); // MainMenu
    }
}