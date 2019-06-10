using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Scene_Manager : MonoBehaviour {


    public static bool gameIsPaused = false;
    public GameObject pauseMenu;

    //Handles audio
    public AudioSource pauseAudio;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        GameObject.Find("LevelMusic").GetComponent<AudioSource>().Play();
        pauseAudio.Stop();
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        GameObject.Find("LevelMusic").GetComponent<AudioSource>().Stop();
        pauseAudio.Play();
    }

    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
        gameIsPaused = false;
    }

    public void playGame ()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame ()
    {
        UnityEditor.EditorApplication.isPlaying = false; 
    }
}


