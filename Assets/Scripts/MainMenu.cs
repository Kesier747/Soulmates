using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Debug.Log("Y nos fuimo");
        Application.Quit();
    }

    public void OptionsMenu()
    {
        
    }
}
