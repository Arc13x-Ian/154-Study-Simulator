using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    
    public GameObject ControlsMenuUI;
    public bool isPaused;
    private GameAddOns GameAddOns;

    void Start()
    {
        GameAddOns = FindAnyObjectByType<GameAddOns>();
    }

    void Update()
    {
        //pause system
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause state
            isPaused = !isPaused;

            // Call the Pause() or Resume() function based on the pause state
            if (isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    

    public void ContinueGame()
    {
        // Toggle pause state
        isPaused = !isPaused;

        // Resume the game
        Resume();
    }

    public void OpenSettings()
    {
        // Implement your settings menu functionality here
        Debug.Log("Open Settings Menu");
        
    }

    public void QuitGame()
    {
        // Quit the application (works in standalone builds)
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OpenControls()
    {
        // Implement your controls menu functionality here
        Debug.Log("Open Controls Menu");
        ControlsMenuUI.SetActive(true);
    }

    public void MainMenu()
    {
        //loads the Main Menu Scene maybe include somewhere in here a confimation panel? if there is enough time
        Debug.Log("Open Main Menu");
        SceneManager.LoadScene("StartScreen");

    }

    void Pause()
    {
        Time.timeScale = 0f; // Pause the game by setting time scale to 0
        Debug.Log("Game Paused");
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameAddOns.canMove = false;
        // You can also add additional pause functionality here (e.g., show pause menu)
    }

    void Resume()
    {
        Time.timeScale = 1f; // Resume the game by setting time scale to 1
        Debug.Log("Game Resumed");
        pauseMenuUI.SetActive(false);
        ControlsMenuUI.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameAddOns.canMove = true;
        // You can also add additional resume functionality here (e.g., hide pause menu)
    }
}
