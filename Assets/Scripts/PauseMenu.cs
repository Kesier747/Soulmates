using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject deathMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) 
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        //if (GetComponent<PlayerInput>().deadState == false)
        //{
        //    Dead();
        //}
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Dead()
    {
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void Respawn()
    {
        SceneManager.LoadScene(2);
    }
}
