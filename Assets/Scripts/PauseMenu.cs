using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject deathMenuUI;

    public GameObject controlsUI;

    [SerializeField] private GameObject gameplayUI;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) 
            {
                Resume();
                FindObjectOfType<AudioManager>().Play("Beep");
                gameplayUI.SetActive(true);
            }
            else
            {
                Pause();
                FindObjectOfType<AudioManager>().Play("Beep");
                gameplayUI.SetActive(false);
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
        gameplayUI.SetActive(true);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Controls()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
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

    //void Dead()
    //{
    //    deathMenuUI.SetActive(true);
    //    Time.timeScale = 0f;
        
    //}

    public void Respawn()
    {
        SceneManager.LoadScene(0);
    }
}
