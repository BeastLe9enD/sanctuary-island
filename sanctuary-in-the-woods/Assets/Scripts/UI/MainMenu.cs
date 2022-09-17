using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject pauseMenu, optionsMenu, creditsMenu;

    public void Play() {
        SceneManager.LoadScene("World");
    }
    
    public void Resume() {
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void OpenOptions() {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void CloseOptions() {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OpenCredits() {
        creditsMenu.SetActive(true);
        pauseMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void CloseCredits() {
        creditsMenu.SetActive(false);
        pauseMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
    }
}